using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain
{

    public class ExecutionStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MessageForDeveloper { get; set; }
        public Exception Exception { get; init; }
        public WhereExecuted Where { get; init; }
        public Reason Reason { get; set; }

        internal ExecutionStatus()
        {
            Reason = Reason.None;
        }

        public static ExecutionStatus LogicOk()
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DomainLogic,
                Success = true,
            };
        }

        public static ExecutionStatus EventStoreOk()
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = true,
            };
        }

        public static ExecutionStatus LogicError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DomainLogic,
                Success = false,
                Message = message,
                Reason = Reason.Error
            };
        }

        public static ExecutionStatus EventStoreError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.Error
            };
        }

        public static ExecutionStatus EventStoreConcurrencyError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore
            };
        }

        public static ExecutionStatus EventStoreEventsOutOfOrderError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.EventsOutOfOrderInEventStore
            };
        }

        public static ExecutionStatus EventStoreAggregateOrEventMissingIdError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.AggregateNotFoundInEventStore
            };
        }

        public static ExecutionStatus LogicError(Exception ex)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DomainLogic,
                Success = false,
                Reason = Reason.NotControledException,
                Exception = ex,
                MessageForDeveloper = ex.Message
            };
        }

        public static ExecutionStatus EventStoreError(Exception ex)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Reason = Reason.NotControledException,
                Exception = ex,
                MessageForDeveloper = ex.Message
            };
        }

        public static ExecutionStatus DbOk()
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DataBase,
                Success = true,
            };
        }

        public static ExecutionStatus DbError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DataBase,
                Reason = Reason.Error,
                Success = false,
                Message = message
            };
        }

        public static ExecutionStatus DbError(Exception ex)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.DataBase,
                Success = false,
                Exception = ex,
                Message = ex.Message,
                Reason = Reason.Error
            };
        }
    }

    public class ExecutionStatus<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public string MessageForDeveloper { get; set; }
        public Exception Exception { get; init; }
        public WhereExecuted Where { get; init; }

        public T Value { get; init; }
        public Reason Reason { get; init; }

        protected ExecutionStatus()
        {
            Reason = Reason.None;
        }

        public ExecutionStatus RemoveGeneric()
        {
            return new ExecutionStatus()
            {
                Success = this.Success,
                Exception = this.Exception,
                Message = this.Message,
                MessageForDeveloper = this.MessageForDeveloper,
                Reason = this.Reason,
                Where = this.Where
            };
        }

        public static ExecutionStatus<T> DbOk(T value)
        {
            return new ExecutionStatus<T>()
            {
                Success = true,
                Value = value,
                Where = WhereExecuted.DataBase
            };
        }

        public static ExecutionStatus<T> LogicOk(T value)
        {
            return new ExecutionStatus<T>()
            {
                Success = true,
                Value = value,
                Where = WhereExecuted.DomainLogic
            };
        }

        public static ExecutionStatus<T> EventStoreOk(T value)
        {
            return new ExecutionStatus<T>()
            {
                Success = true,
                Value = value,
                Where = WhereExecuted.EventStore
            };
        }

        public static ExecutionStatus<T> LogicError(string message)
        {
            return new ExecutionStatus<T>()
            {
                Success = false,
                Message = message,
                Reason = Reason.Error,
                Where = WhereExecuted.DomainLogic
            };
        }

        public static ExecutionStatus<T> EventStoreError(string message)
        {
            return new ExecutionStatus<T>()
            {
                Success = false,
                Message = message,
                Reason = Reason.Error,
                Where = WhereExecuted.EventStore
            };
        }

        public static ExecutionStatus<T> LogicError(Exception ex)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.DomainLogic,
                Success = false,
                Reason = Reason.NotControledException,
                Exception = ex,
                MessageForDeveloper = ex.Message
            };
        }

        public static ExecutionStatus<T> EventStoreError(Exception ex)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Reason = Reason.NotControledException,
                Exception = ex,
                MessageForDeveloper = ex.Message
            };
        }


        public static ExecutionStatus<T> EventStoreConcurrencyError(string message)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore
            };
        }

        public static ExecutionStatus EventStoreAggregateOrEventMissingIdError(string message)
        {
            return new ExecutionStatus()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.AggregateOrEventMissingIdInEventStore
            };
        }

        public static ExecutionStatus<T> EventStoreAggregateNotFoundError(string message)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.EventStore,
                Success = false,
                Message = message,
                Reason = Reason.AggregateNotFoundInEventStore
            };
        }



        public static ExecutionStatus<T> LogicIfDefaultThenError(T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                return new ExecutionStatus<T>()
                {
                    Where = WhereExecuted.DomainLogic,
                    Success = false,
                    Message = "NotFound",
                    Reason = Reason.ReturnedNull
                };
            }

            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.DataBase,
                Success = true,
            };
        }

        public static ExecutionStatus<T> DbError(string message)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.DataBase,
                Success = false,
                Message = message
            };
        }

        public static ExecutionStatus<T> DbError(Exception ex)
        {
            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.DataBase,
                Success = false,
                Reason = Reason.NotControledException,
                Exception = ex,
                MessageForDeveloper = ex.Message
            };
        }

        public static ExecutionStatus<T> DbIfDefaultThenError(T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                return new ExecutionStatus<T>()
                {
                    Where = WhereExecuted.DataBase,
                    Success = false,
                    Message = "NotFound",
                    Reason = Reason.ReturnedNull
                };
            }

            return new ExecutionStatus<T>()
            {
                Where = WhereExecuted.DataBase,
                Success = true,
                Value = value
            };
        }

        public static ExecutionStatus<T> From(ExecutionStatus addstatus)
        {
            return new ExecutionStatus<T>()
            {
                Where = addstatus.Where,
                Exception = addstatus.Exception,
                Message = addstatus.Message,
                Success = true,
            };
        }
    }
}



