﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdPayeDelegantLigne : IEquatable<IdPayeDelegantLigne>
    {
        public static explicit operator int(IdPayeDelegantLigne id)
        {
            return id._value;
        }

        public static bool operator ==(IdPayeDelegantLigne value1, IdPayeDelegantLigne value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdPayeDelegantLigne value1, IdPayeDelegantLigne value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdPayeDelegantLigne Parse(int id)
        {
            return new IdPayeDelegantLigne(id);
        }
        
        private int _value;
        
        private IdPayeDelegantLigne(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdPayeDelegantLigne)) { return false; }

            return ((IdPayeDelegantLigne)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdPayeDelegantLigne other)
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
