using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class IdPDL : IEquatable<IdPDL>
    {
        public static explicit operator int(IdPDL id)
        {
            return id._value;
        }

        public static bool operator ==(IdPDL value1, IdPDL value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdPDL value1, IdPDL value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdPDL Parse(int id)
        {
            return new IdPDL(id);
        }

        private int _value;

        private IdPDL(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdPDL)) { return false; }

            return ((IdPDL)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdPDL other)
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
