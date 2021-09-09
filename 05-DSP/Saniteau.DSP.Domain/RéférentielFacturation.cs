using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public interface RéférentielFacturation
    {
        List<Facturation> GetFacturations(IdAbonné idAbonné);
    }
}
