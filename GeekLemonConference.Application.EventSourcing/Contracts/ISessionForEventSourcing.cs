using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    //public interface ISessionForEventSourcing
    //{
    //    void Add<T>(T aggregate) where T : AggregateRoot;
    //    T Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot;
    //    void Commit();
    //}

    public interface ISessionForEventSourcing
    {
        ExecutionStatus Add<T>(T aggregate) where T : AggregateRoot;
        ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot;
        ExecutionStatus Commit();
    }
}
