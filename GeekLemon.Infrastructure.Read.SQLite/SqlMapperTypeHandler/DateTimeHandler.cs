using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.SqlMapperTypeHandler
{
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public static readonly DateTimeHandler Default = new DateTimeHandler();
        private readonly TimeZoneInfo databaseTimeZone = TimeZoneInfo.Local;
        public DateTimeHandler()
        {

        }

        public override DateTimeOffset Parse(object value)
        {
            DateTime storedDateTime;
            if (value == null)
                storedDateTime = DateTime.MinValue;
            else
                storedDateTime = (DateTime)value;

            if (storedDateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime)
                return DateTimeOffset.MinValue;
            else
                return new DateTimeOffset(storedDateTime, databaseTimeZone.BaseUtcOffset);
        }

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            DateTime paramVal = value.ToOffset(databaseTimeZone.BaseUtcOffset).DateTime;
            parameter.Value = paramVal;
        }
    }
}
