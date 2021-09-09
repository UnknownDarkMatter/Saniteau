using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielAbonnés
    {
        Abonné EnregistreAbonné(Abonné abonné);
        Adresse CréeAdresse(Adresse adresse);
        Adresse GetAddresseOfAbonné(IdAbonné idAbonné);
        Adresse GetAddresse(IdAdresse idAdresse);
        List<Adresse> GetAllAddresses();
        Abonné GetAbonné(IdAbonné idAbonné);
        List<Abonné> GetAllAbonnés();
    }
}
