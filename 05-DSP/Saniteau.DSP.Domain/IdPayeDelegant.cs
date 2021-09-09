using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdPayeDelegant : IEquatable<IdPayeDelegant>
    {
        public static explicit operator int(IdPayeDelegant id)
        {
            return id._value;
        }

        public static bool operator ==(IdPayeDelegant value1, IdPayeDelegant value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdPayeDelegant value1, IdPayeDelegant value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdPayeDelegant Parse(int id)
        {
            return new IdPayeDelegant(id);
        }
        
        private int _value;
        
        private IdPayeDelegant(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdPayeDelegant)) { return false; }

            return ((IdPayeDelegant)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdPayeDelegant other)
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
