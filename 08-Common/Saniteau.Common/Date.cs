using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Common
{
    public class Date : IEquatable<Date>, IComparable<Date>
    {
        public static Date FromDateTime(DateTime dateTime)
        {
            return new Date(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static Date MinValue { get { return DateTime.MinValue.ToDate(); } }
        public static Date MaxValue { get { return DateTime.MaxValue.ToDate(); } }

        public static bool operator ==(Date value1, Date value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(Date value1, Date value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }


        private DateTime _date;
        public Date(int year, int month, int day)
        {
            _date = new DateTime(year, month, day);
        }

        public int Year { get { return _date.Year; } }
        public int Month { get { return _date.Month; } }
        public int Day { get { return _date.Day; } }
        public override bool Equals(object obj)
        {
            if(obj == null) { return false; }
            if(obj.GetType() != typeof(Date)) { return false; }
            var other = (Date)obj;
            return _date.Year == other._date.Year && _date.Month == other._date.Month && _date.Day == other._date.Day;
        }

        public bool Equals([AllowNull] Date other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode();
        }

        public override string ToString()
        {
            return _date.ToString("dd/MM/yyyy");
        }

        public DateTime ToDateTime(int hour, int minutes, int seconds)
        {
            return new DateTime(_date.Year, _date.Month, _date.Day, hour, minutes, seconds);
        }

        public Date AddYears(int value)
        {
            _date = _date.AddYears(value);
            return FromDateTime(_date);
        }

        public Date AddMonths(int value)
        {
            _date = _date.AddMonths(value);
            return FromDateTime(_date);
        }

        public Date AddDays(int nbDays)
        {
            _date = _date.AddDays(nbDays);
            return FromDateTime(_date);
        }

        public int CompareTo([AllowNull] Date other)
        {
            if(other is null) { return 1; }
            if (this._date < other._date) return -1;
            if (this._date == other._date) return 0;
            return 1;
        }

        public Date LastDayOfYear()
        {
            return new Date(Year, 12, 31);
        }

        
    }
}
