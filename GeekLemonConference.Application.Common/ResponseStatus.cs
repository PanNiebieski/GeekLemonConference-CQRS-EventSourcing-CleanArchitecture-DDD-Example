namespace GeekLemonConference.Application.Common
{
    public enum ResponseStatus
    {
        Success = 0,
        NotFoundInDataBase = 1,
        BadQuery = 2,
        ValidationError = 3,
        DataBaseError = 4,
        BussinesLogicError = 5,
        EventStoreError = 6,
        ConcurrencyOlderVersionSendedWhenNewerIsInEventStore = 7,
        AggregateOrEventMissingIdInEventStore = 8,
        AggregateNotFoundInEventStore = 9
    }
}
