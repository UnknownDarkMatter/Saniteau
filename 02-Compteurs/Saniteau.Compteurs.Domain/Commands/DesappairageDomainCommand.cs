using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class DesappairageDomainCommand
    {
        public IdAppairageCompteur IdAppairageCompteur { get; private set; }

        public DesappairageDomainCommand(IdAppairageCompteur idAppairageCompteur)
        {
            if (idAppairageCompteur is null) { throw new ArgumentNullException(nameof(idAppairageCompteur)); }

            IdAppairageCompteur = idAppairageCompteur;
        }

        public AppairageCompteur DesappairageCompteur(RéférentielAppairage référentielAppairage)
        {
            var appairage = référentielAppairage.GetAppairageOfPDL(IdAppairageCompteur);
            appairage.DesappaireCompteur();
            référentielAppairage.EnregistreAppairageCompteur(appairage);
            return appairage;
        }

    }
}
