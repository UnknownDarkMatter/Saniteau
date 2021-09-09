using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdAbonné : IEquatable<IdAbonné>
    {
        public static explicit operator int(IdAbonné id)
        {
            return id._value;
        }

        public static bool operator ==(IdAbonné value1, IdAbonné value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdAbonné value1, IdAbonné value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdAbonné Parse(int id)
        {
            return new IdAbonné(id);
        }
        
        private int _value;
        
        private IdAbonné(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdAbonné)) { return false; }

            return ((IdAbonné)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdAbonné other)
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
