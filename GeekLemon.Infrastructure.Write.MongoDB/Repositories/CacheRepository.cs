using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Contracts;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Caching;

namespace GeekLemonConference.Infrastructure.EventStoreAndBus.Repositories.EventStore
{
    public class CacheRepository : IEventRepository
    {
        private readonly IEventRepository _repository;
        private readonly IEventStore _eventStore;
        private readonly MemoryCache _cache;
        private readonly Func<CacheItemPolicy> _policyFactory;
        private static readonly ConcurrentDictionary<string, object> _locks =
            new ConcurrentDictionary<string, object>();

        public CacheRepository(IEventRepository repository, IEventStore eventStore)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");

            _repository = repository;
            _eventStore = eventStore;
            _cache = MemoryCache.Default;
            _policyFactory = () => new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, 0, 15, 0),
                RemovedCallback = x =>
                {
                    object o;
                    _locks.TryRemove(x.CacheItem.Key, out o);
                }
            };
        }

        public void Save<T>(T aggregate, int? expectedVersion = null)
            where T : AggregateRoot
        {
            var idstring = aggregate.Key.ToString();
            try
            {
                lock (_locks.GetOrAdd(idstring, _ => new object()))
                {
                    if (aggregate.Key != AggregateKey.Empty && !IsTracked(aggregate.Key))
                        _cache.Add(idstring, aggregate, _policyFactory.Invoke());
                    _repository.Save(aggregate, expectedVersion);
                }
            }
            catch (Exception)
            {
                _cache.Remove(idstring);
                throw;
            }
        }

        public T Get<T>(AggregateKey aggregateId) where T : AggregateRoot
        {
            var idstring = aggregateId.ToString();
            try
            {
                lock (_locks.GetOrAdd(idstring, _ => new object()))
                {
                    T aggregate;
                    if (IsTracked(aggregateId))
                    {
                        aggregate = (T)_cache.Get(idstring);
                        var events = _eventStore.Get(aggregateId, aggregate.Version);
                        if (events.Any() && events.First().Version != aggregate.Version + 1)
                        {
                            _cache.Remove(idstring);
                        }
                        else
                        {
                            aggregate.LoadFromHistory(events);
                            return aggregate;
                        }
                    }
                    //jeśli nie ma go w Cache to poszukać w repozytorium prawdziwym
                    aggregate = _repository.Get<T>(aggregateId);
                    _cache.Add(
                        aggregateId.ToString(),
                        aggregate,
                        _policyFactory.Invoke());
                    return aggregate;
                }
            }
            catch (Exception)
            {
                _cache.Remove(idstring);
                throw;
            }
        }

        private bool IsTracked(AggregateKey id)
        {
            return _cache.Contains(id.ToString());
        }
    }
}
