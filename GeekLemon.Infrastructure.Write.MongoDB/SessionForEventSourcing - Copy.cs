//using GeekLemon.Infrastructure.Write.MongoDB;
//using GeekLemonConference.Application.Contracts;
//using GeekLemonConference.Application.EventSourcing;
//using GeekLemonConference.Application.EventSourcing.Contracts;
//using GeekLemonConference.Application.EventSourcing.Exceptions;
//using GeekLemonConference.Domain;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace GeekLemonConference.Infrastructure.EventStoreAndBus
//{
//    public class SessionForEventSourcing : ISessionForEventSourcing
//    {
//        private readonly IEventRepository _repository;
//        private readonly Dictionary<AggregateKey, AggregateDescriptor> _trackedAggregates;

//        public SessionForEventSourcing(IEventRepository repository)
//        {
//            if (repository == null)
//                throw new ArgumentNullException("repository");

//            _repository = repository;
//            _trackedAggregates = new Dictionary<AggregateKey, AggregateDescriptor>();
//        }

//        public ExecutionStatus Add<T>(T aggregate) where T : AggregateRoot
//        {
//            if (!IsTracked(aggregate.Key))
//                _trackedAggregates.Add(aggregate.Key,
//                    new AggregateDescriptor
//                    {
//                        Aggregate = aggregate,
//                        Version = aggregate.Version
//                    });
//            else if (_trackedAggregates[aggregate.Key].Aggregate != aggregate)
//            {
//                string message = string.Format("A different version than expected was found in aggregate {0}");
//                return ExecutionStatus.EventStoreConcurrencyError(message);
//            }

//            return ExecutionStatus.EventStoreOk();
//        }

//        public ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
//        {
//            if (IsTracked(id))
//            {
//                var trackedAggregate = (T)_trackedAggregates[id].Aggregate;
//                if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
//                {
//                    string message = string.Format("A different version than expected was found in aggregate {0}", id);
//                    return ExecutionStatus<T>.EventStoreConcurrencyError(message);
//                }

//                return ExecutionStatus<T>.EventStoreOk(trackedAggregate);
//            }

//            ExecutionStatus<T> aggregateStatus;

//            aggregateStatus = _repository.Get<T>(id);

//            if (!aggregateStatus.Success)
//                return aggregateStatus;

//            if (expectedVersion != null && aggregateStatus.Value.Version != expectedVersion)
//            {
//                string message = string.Format("A different version than expected was found in aggregate {0}", id);
//                return ExecutionStatus<T>.EventStoreConcurrencyError(message);
//            }

//            var addstatus = Add(aggregateStatus.Value);

//            if (!addstatus.Success)
//                return ExecutionStatus<T>.From(addstatus);

//            return ExecutionStatus<T>.EventStoreOk(aggregateStatus.Value);
//        }

//        private bool IsTracked(AggregateKey id)
//        {
//            return _trackedAggregates.ContainsKey(id);
//        }

//        public ExecutionStatus Commit()
//        {
//            foreach (var descriptor in _trackedAggregates.Values)
//            {
//                try
//                {
//                    _repository.Save(descriptor.Aggregate, descriptor.Version);
//                }
//                catch (Exception ex)
//                {
//                    return ExecutionStatus.EventStoreError(ex);
//                }
//            }
//            _trackedAggregates.Clear();

//            return ExecutionStatus.EventStoreOk();
//        }

//        private class AggregateDescriptor
//        {
//            public AggregateRoot Aggregate { get; set; }
//            public int Version { get; set; }
//        }
//    }
//}
