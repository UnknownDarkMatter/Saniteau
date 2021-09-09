using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public class IdCompteur : IEquatable<IdCompteur>
    {
        public static explicit operator int(IdCompteur id)
        {
            return id._value;
        }

        public static bool operator ==(IdCompteur value1, IdCompteur value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdCompteur value1, IdCompteur value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdCompteur Parse(int id)
        {
            return new IdCompteur(id);
        }

        private int _value;

        private IdCompteur(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdCompteur)) { return false; }

            return ((IdCompteur)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdCompteur other)
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
