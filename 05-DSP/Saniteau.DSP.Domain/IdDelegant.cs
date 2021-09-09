using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdDelegant : IEquatable<IdDelegant>
    {
        public static explicit operator int(IdDelegant id)
        {
            return id._value;
        }

        public static bool operator ==(IdDelegant value1, IdDelegant value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdDelegant value1, IdDelegant value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdDelegant Parse(int id)
        {
            return new IdDelegant(id);
        }
        
        private int _value;
        
        private IdDelegant(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdDelegant)) { return false; }

            return ((IdDelegant)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdDelegant other)
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
