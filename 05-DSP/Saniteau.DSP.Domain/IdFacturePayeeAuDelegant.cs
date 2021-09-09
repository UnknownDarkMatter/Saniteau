using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdFacturePayeeAuDelegant : IEquatable<IdFacturePayeeAuDelegant>
    {
        public static explicit operator int(IdFacturePayeeAuDelegant id)
        {
            return id._value;
        }

        public static bool operator ==(IdFacturePayeeAuDelegant value1, IdFacturePayeeAuDelegant value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdFacturePayeeAuDelegant value1, IdFacturePayeeAuDelegant value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdFacturePayeeAuDelegant Parse(int id)
        {
            return new IdFacturePayeeAuDelegant(id);
        }

        private int _value;

        private IdFacturePayeeAuDelegant(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdFacturePayeeAuDelegant)) { return false; }

            return ((IdFacturePayeeAuDelegant)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdFacturePayeeAuDelegant other)
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
