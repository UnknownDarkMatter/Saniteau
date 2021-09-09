using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Common
{
    public class Montant : IEquatable<Montant>, IComparable<Montant>
    {
        public static Montant FromDecimal(decimal value)
        {
            return new Montant(value);
        }

        public static bool operator ==(Montant value1, Montant value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(Montant value1, Montant value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static explicit operator decimal (Montant montant)
        {
            if (montant is null) { return 0; }

            return montant._value;
        }


        private readonly decimal _value;

        private Montant(decimal value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Montant)) { return false; }

            return base.Equals((Montant) obj);
        }
        public bool Equals([AllowNull] Montant other)
        {
            if(other is null) { return false; }

            return other._value == this._value;
        }
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        public override string ToString()
        {
            return _value.ToString();
        }

        public int CompareTo([AllowNull] Montant other)
        {
            if (other is null) { return 1; }
            if (this._value < other._value) return -1;
            if (this._value == other._value) return 0;
            return 1;
        }
    }
}
