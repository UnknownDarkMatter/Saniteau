using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdPompe : IEquatable<IdPompe>
    {
        public static explicit operator int(IdPompe id)
        {
            return id._value;
        }

        public static bool operator ==(IdPompe value1, IdPompe value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdPompe value1, IdPompe value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdPompe Parse(int id)
        {
            return new IdPompe(id);
        }

        private int _value;

        private IdPompe(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(IdPompe)) { return false; }

            return ((IdPompe)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdPompe other)
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
