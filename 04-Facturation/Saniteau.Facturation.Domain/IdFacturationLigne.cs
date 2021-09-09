using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class IdFacturationLigne : IEquatable<IdFacturationLigne>
    {
        public static explicit operator int(IdFacturationLigne id)
        {
            return id._value;
        }

        public static bool operator ==(IdFacturationLigne value1, IdFacturationLigne value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdFacturationLigne value1, IdFacturationLigne value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdFacturationLigne Parse(int id)
        {
            return new IdFacturationLigne(id);
        }
        
        private int _value;
        
        private IdFacturationLigne(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdFacturationLigne)) { return false; }

            return ((IdFacturationLigne)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdFacturationLigne other)
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
