using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common
{
    public class Horloge : IHorloge
    {
        private static IHorloge _instance = null;
        public static IHorloge Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Horloge();
                }
                return _instance;
            }
        }
        public static void Mock(IHorloge instance)
        {
            _instance = instance;
        }
        public static void UnMock()
        {
            _instance = new Horloge();
        }

        public Date GetDate()
        {
            var now = DateTime.Now;
            return new Date(now.Year, now.Month, now.Day);
        }

        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}
