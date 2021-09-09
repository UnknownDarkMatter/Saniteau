using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common
{
    public static class DateTimeExtentions
    {
        public static Date ToDate(this DateTime date)
        {
            return new Date(date.Year, date.Month, date.Day);
        }
        public static Date? ToDate(this DateTime? date)
        {
            if(date == null) { return null; }
            return new Date(date.Value.Year, date.Value.Month, date.Value.Day);
        }
    }
}
