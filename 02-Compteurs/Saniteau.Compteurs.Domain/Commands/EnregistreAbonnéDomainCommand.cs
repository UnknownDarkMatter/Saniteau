using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class EnregistreAbonnéDomainCommand
    {
        public IdAbonné IdAbonné { get; private set; }
        public IdAdresse IdAdresse { get; private set; }
        public ChampLibre Nom { get; private set; }
        public ChampLibre Prénom { get; private set; }
        public Tarification Tarification { get; private set; }

        public EnregistreAbonnéDomainCommand(IdAbonné idAbonné, IdAdresse idAdresse, ChampLibre nom, ChampLibre prénom, Tarification tarification)
        {
            if (idAbonné is null) { throw new ArgumentNullException(nameof(idAbonné)); }
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (nom is null) { throw new ArgumentNullException(nameof(nom)); }
            if (prénom is null) { throw new ArgumentNullException(nameof(prénom)); }

            IdAbonné = idAbonné;
            IdAdresse = idAdresse;
            Nom = nom;
            Prénom = prénom;
            Tarification = tarification;
        }

        public Tuple<Abonné, Adresse> EnregistreAbonné(RéférentielAbonnés référentielAbonnés)
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            var abonne = référentielAbonnés.EnregistreAbonné(new Abonné(IdAbonné, IdAdresse, Nom, Prénom, Tarification));
            var adresse = référentielAbonnés.GetAddresse(abonne.IdAdresse);
            return new Tuple<Abonné, Adresse>(abonne, adresse);
        }

    }
}
