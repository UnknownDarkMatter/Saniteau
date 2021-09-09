using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class IdAdresse : IEquatable<IdAdresse>
    {
        public static explicit operator int(IdAdresse id)
        {
            return id._value;
        }

        public static bool operator ==(IdAdresse value1, IdAdresse value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdAdresse value1, IdAdresse value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdAdresse Parse(int id)
        {
            return new IdAdresse(id);
        }

        private int _value;

        private IdAdresse(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdAdresse)) { return false; }

            return ((IdAdresse)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdAdresse other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
