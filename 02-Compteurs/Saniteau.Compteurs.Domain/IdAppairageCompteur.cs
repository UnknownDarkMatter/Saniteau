using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public class IdAppairageCompteur : IEquatable<IdAppairageCompteur>
    {
        public static explicit operator int(IdAppairageCompteur id)
        {
            return id._value;
        }

        public static bool operator ==(IdAppairageCompteur value1, IdAppairageCompteur value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdAppairageCompteur value1, IdAppairageCompteur value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdAppairageCompteur Parse(int id)
        {
            return new IdAppairageCompteur(id);
        }

        private int _value;

        private IdAppairageCompteur(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdAppairageCompteur)) { return false; }

            return ((IdAppairageCompteur)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdAppairageCompteur other)
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
