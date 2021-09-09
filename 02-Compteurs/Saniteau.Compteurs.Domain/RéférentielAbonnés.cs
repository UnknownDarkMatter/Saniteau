using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public interface RéférentielAbonnés
    {
        Adresse EnregistreAdresse(Adresse adresse);
        Abonné EnregistreAbonné(Abonné abonné);
        Adresse GetAddresseOfAbonné(IdAbonné idAbonné);
        Adresse GetAddresse(IdAdresse idAdresse);
        Abonné GetAbonné(IdAbonné idAbonné);
        List<Tuple<Abonné, Adresse>> GetAllAbonnés(bool filtrerAbonnésAvecCompteur);
        void SupprimeAbonné(IdAbonné idAbonné);
        void SupprimeAdresse(IdAdresse idAdresse);
    }
}
