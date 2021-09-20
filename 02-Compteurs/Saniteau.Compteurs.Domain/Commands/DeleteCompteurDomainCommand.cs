using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class DeleteCompteurDomainCommand
    {
        public IdCompteur IdCompteur { get; private set; }
        public DeleteCompteurDomainCommand(IdCompteur idCompteur)
        {
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdCompteur = idCompteur;
        }

        public void DeleteCompteur(RéférentielCompteurs référentielCompteurs, RéférentielAppairage référentielAppairage, 
            RéférentielPDL référentielPDL, RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            var compteur = référentielCompteurs.GetCompteur(IdCompteur);
            if(compteur is null) { throw new Exception($"Impossible de supprimer le compteur {IdCompteur} car il n'existe pas en base"); }

            var indexes = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur.IdCompteur);
            for (int i = 0; i < indexes.Count; i++)
            {
                var index = indexes[i];
                référentielIndexesCompteurs.SupprimeIndex(index);
            }

            if (compteur.PDL != null) {
                var adressesPDL = référentielAppairage.GetAdressesPDL(compteur.PDL.IdPDL);
                for (int i = 0; i < adressesPDL.Count; i++)
                {
                    var adressePDL = adressesPDL[i];
                    référentielAppairage.SupprimeAdressePDL(adressePDL.IdAdresse);
                }
                var appairages = référentielAppairage.GetAppairageOfPDL(compteur.PDL.IdPDL);
                for (int i = 0; i < appairages.Count; i++)
                {
                    var appairage = appairages[i];
                    référentielAppairage.SupprimeAppairagge(appairage);
                }
                référentielPDL.SupprimePDL(compteur.PDL.IdPDL);
            }
            référentielCompteurs.SupprimerCompteur(compteur.IdCompteur);

        }
    }
}
