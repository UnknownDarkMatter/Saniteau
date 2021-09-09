using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class ChampLibre : IEquatable<ChampLibre>
    {
        public static bool operator ==(ChampLibre value1, ChampLibre value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(ChampLibre value1, ChampLibre value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        private readonly string _value;

        public ChampLibre(string value)
        {
            _value = value;
        }

        public bool Equals([AllowNull] ChampLibre other)
        {
            return Equals((object)other);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType() != typeof(ChampLibre)) { return false; }

            return ((ChampLibre)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
