using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielFacturation
    {
        List<Facturation> GetFacturations(IdAbonné idAbonné);
        List<Facturation> GeAllFacturations();
        Facturation EnregistrerFacturation(Facturation facturation);
        int GetDerniereCampagneFacturationId();
        Facturation GetFacturation(IdFacturation idFacturation);
    }
}
