namespace GeekLemonConference.Domain
{
    public enum Reason
    {
        None,
        Error,
        NotControledException,
        ReturnedNull,
        ConcurrencyOlderVersionSendedWhenNewerIsInEventStore,
        AggregateOrEventMissingIdInEventStore,
        AggregateNotFoundInEventStore,

        EventsOutOfOrderInEventStore

    }
}



