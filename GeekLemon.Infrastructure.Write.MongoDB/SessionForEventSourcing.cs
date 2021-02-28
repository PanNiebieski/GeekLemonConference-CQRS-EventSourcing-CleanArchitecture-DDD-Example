using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Contracts;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Application.EventSourcing.Exceptions;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Infrastructure.EventStoreAndBus
{
    public class SessionForEventSourcing : ISessionForEventSourcing
    {
        private readonly IEventRepository _repository;
        private readonly Dictionary<AggregateKey, AggregateDescriptor> _trackedAggregates;

        public SessionForEventSourcing(IEventRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
            _trackedAggregates = new Dictionary<AggregateKey, AggregateDescriptor>();
        }

        public ExecutionStatus Add<T>(T aggregate) where T : AggregateRoot
        {
            try
            {
                if (!IsTracked(aggregate.Key))
                    _trackedAggregates.Add(aggregate.Key,
                        new AggregateDescriptor
                        {
                            Aggregate = aggregate,
                            Version = aggregate.Version
                        });
                else if (_trackedAggregates[aggregate.Key].Aggregate != aggregate)
                    throw new ConcurrencyException(aggregate.Key);
            }
            catch (Exception ex)
            {
                //if (ExecutionFlow.Options.ThrowExceptions)
                throw;

                return ExecutionStatus.EventStoreError(ex);
            }

            return ExecutionStatus.EventStoreOk();
        }

        public ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            try
            {
                //w pamięci sprawdzamy czy nie próbujemy ze złą wersją dodać zdarzenie
                if (IsTracked(id))
                {
                    var trackedAggregate = (T)_trackedAggregates[id].Aggregate;
                    if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
                        throw new ConcurrencyException(trackedAggregate.Key);
                    return ExecutionStatus<T>.EventStoreOk(trackedAggregate);
                }

                //jeśli nie mamy w pamieci to odpytujemy repozytorium
                var aggregate = _repository.Get<T>(id);
                if (expectedVersion != null && aggregate.Version != expectedVersion)
                    throw new ConcurrencyException(id);
                Add(aggregate); //dodaj do tej warstwy pamięci

                return ExecutionStatus<T>.EventStoreOk(aggregate);
            }
            catch (Exception ex)
            {
                //if (ExecutionFlow.Options.ThrowExceptions)
                throw;

                return ExecutionStatus<T>.EventStoreError(ex);
            }


        }

        private bool IsTracked(AggregateKey id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public ExecutionStatus Commit()
        {
            try
            {
                foreach (var descriptor in _trackedAggregates.Values)
                {
                    _repository.Save(descriptor.Aggregate, descriptor.Version);
                }
                _trackedAggregates.Clear();
            }
            catch (Exception ex)
            {
                //if (ExecutionFlow.Options.ThrowExceptions)
                throw;

                return ExecutionStatus.EventStoreError(ex);
            }
            return ExecutionStatus.EventStoreOk();
        }

        private class AggregateDescriptor
        {
            public AggregateRoot Aggregate { get; set; }
            public int Version { get; set; }
        }
    }
}
