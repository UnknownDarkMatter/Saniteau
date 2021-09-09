using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class AssocieAdressePDLDomainCommand
    {
        public IdAdresse IdAdresse { get; private set; }
        public IdCompteur IdCompteur { get; private set; }

        public AssocieAdressePDLDomainCommand(IdAdresse idAdresse, IdCompteur idCompteur)
        {
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdAdresse = idAdresse;
            IdCompteur = idCompteur;
        }

        public Compteur AssocieAdressePDL(RéférentielCompteurs référentielCompteurs, RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL)
        {
            string numeroPDL = $"PDL compteur {IdCompteur}, adresse {IdAdresse}";
            var pdl = new PDL(IdPDL.Parse(0), new ChampLibre(numeroPDL));
            pdl = référentielPDL.CréeNouveauPDL(pdl);

            var adressePDL = new AdressePDL(IdAdressePDL.Parse(0), IdAdresse, pdl.IdPDL);
            référentielAppairage.CréeAdressePDL(adressePDL);

            var appairage = new AppairageCompteur(IdAppairageCompteur.Parse(0), pdl.IdPDL, IdCompteur, null, null);
            référentielAppairage.EnregistreAppairageCompteur(appairage);

            var compteur = référentielCompteurs.GetCompteur(IdCompteur);
            return compteur;
        }
    }
}
