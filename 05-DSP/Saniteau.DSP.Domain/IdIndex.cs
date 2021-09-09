using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdIndex : IEquatable<IdIndex>, IComparable<IdIndex>
    {
        public static explicit operator int(IdIndex id)
        {
            return id._value;
        }

        public static bool operator ==(IdIndex value1, IdIndex value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdIndex value1, IdIndex value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdIndex Parse(int id)
        {
            return new IdIndex(id);
        }

        private int _value;

        private IdIndex(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdIndex)) { return false; }

            return ((IdIndex)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdIndex other)
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

        public int CompareTo([AllowNull] IdIndex other)
        {
            if (this._value < other._value) return -1;
            if (this._value == other._value) return 0;
            return 1;
        }
    }
}
