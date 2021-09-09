using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class IdAdressePDL : IEquatable<IdAdressePDL>
    {
        public static explicit operator int(IdAdressePDL id)
        {
            return id._value;
        }

        public static bool operator ==(IdAdressePDL value1, IdAdressePDL value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdAdressePDL value1, IdAdressePDL value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdAdressePDL Parse(int id)
        {
            return new IdAdressePDL(id);
        }

        private int _value;

        private IdAdressePDL(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdAdressePDL)) { return false; }

            return ((IdAdressePDL)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdAdressePDL other)
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
