﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IdFacturation : IEquatable<IdFacturation>
    {
        public static explicit operator int(IdFacturation id)
        {
            return id._value;
        }

        public static bool operator ==(IdFacturation value1, IdFacturation value2)
        {
            if (value1 is null && value2 is null) { return true; }
            if (value1 is null) { return false; }
            return value1.Equals(value2);
        }
        public static bool operator !=(IdFacturation value1, IdFacturation value2)
        {
            if (value1 is null) { return false; }
            return !value1.Equals(value2);
        }

        public static IdFacturation Parse(int id)
        {
            return new IdFacturation(id);
        }
        
        private int _value;
        
        private IdFacturation(int id)
        {
            _value = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is null) { return false; }
            if(obj.GetType() != typeof(IdFacturation)) { return false; }

            return ((IdFacturation)obj)._value == _value;
        }

        public bool Equals([AllowNull] IdFacturation other)
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
