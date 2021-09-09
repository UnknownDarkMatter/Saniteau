using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdIndexPayéParDelegant : IEquatable<IdIndexPayéParDelegant>
    {
        public static explicit operator int(IdIndexPayéParDelegant id)
        {
            return id._value;
        }

        public static bool operator ==(IdIndexPayéParDelegant value1, IdIndexPayéParDelegant value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdIndexPayéParDelegant value1, IdIndexPayéParDelegant value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdIndexPayéParDelegant Parse(int id)
        {
            return new IdIndexPayéParDelegant(id);
        }

        private int _value;

        private IdIndexPayéParDelegant(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdIndexPayéParDelegant)) { return false; }

            return ((IdIndexPayéParDelegant)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdIndexPayéParDelegant other)
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
