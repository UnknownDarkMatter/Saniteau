using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class DissocieAdressePDLDomainCommand
    {
        public IdAdresse IdAdresse { get; private set; }
        public IdCompteur IdCompteur { get; private set; }

        public DissocieAdressePDLDomainCommand(IdAdresse idAdresse, IdCompteur idCompteur)
        {
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdAdresse = idAdresse;
            IdCompteur = idCompteur;
        }

        public Compteur DissocieAdressePDL(RéférentielCompteurs référentielCompteurs, RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL)
        {
            var adressesPDL = référentielAppairage.GetAdressesPDL(IdAdresse);
            for(int i = 0;i< adressesPDL.Count; i++)
            {
                var adressePDL = adressesPDL[i];
                var appairages = référentielAppairage.GetAppairageOfPDL(adressePDL.IdPDL);
                for(int j=0;j< appairages.Count; j++)
                {
                    var appairage = appairages[j];
                    référentielAppairage.SupprimeAppairagge(appairage);
                }
                référentielAppairage.SupprimeAdressePDL(IdAdresse);
                référentielPDL.SupprimePDL(adressePDL.IdPDL);
            }
            var compteur = référentielCompteurs.GetCompteur(IdCompteur);
            return compteur;
        }
    }
}
