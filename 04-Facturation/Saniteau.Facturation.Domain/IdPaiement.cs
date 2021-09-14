using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class IdPaiement : IEquatable<IdPaiement>
    {
        public static explicit operator int(IdPaiement id)
        {
            return id._value;
        }

        public static bool operator ==(IdPaiement value1, IdPaiement value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdPaiement value1, IdPaiement value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdPaiement Parse(int id)
        {
            return new IdPaiement(id);
        }
        
        private int _value;
        
        private IdPaiement(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdPaiement)) { return false; }

            return ((IdPaiement)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdPaiement other)
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
