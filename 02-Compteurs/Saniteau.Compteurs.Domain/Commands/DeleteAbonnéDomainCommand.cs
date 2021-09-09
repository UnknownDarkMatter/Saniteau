using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class DeleteAbonnéDomainCommand
    {
        public IdAbonné IdAbonné { get; private set; }

        public DeleteAbonnéDomainCommand(IdAbonné idAbonné)
        {
            if (idAbonné is null) { throw new ArgumentNullException(nameof(idAbonné)); }

            IdAbonné = idAbonné;
        }

        public void DeleteAbonné(RéférentielAbonnés référentielAbonnés)
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            var abonne = référentielAbonnés.GetAbonné(IdAbonné);
            référentielAbonnés.SupprimeAdresse(abonne.IdAdresse);
            référentielAbonnés.SupprimeAbonné(IdAbonné);
        }

    }
}
