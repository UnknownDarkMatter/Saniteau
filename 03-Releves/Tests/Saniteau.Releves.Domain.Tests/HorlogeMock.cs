using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain.Tests
{
    public class HorlogeMock : IHorloge
    {
        private readonly DateTime _now;
        public HorlogeMock(DateTime now)
        {
            _now = now;
        }

        public Date GetDate()
        {
            return Date.FromDateTime(_now);
        }

        public DateTime GetDateTime()
        {
            return _now;
        }
    }
}
