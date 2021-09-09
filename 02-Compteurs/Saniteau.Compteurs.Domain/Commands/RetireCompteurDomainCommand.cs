using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class RetireCompteurDomainCommand
    {
        public Compteur Compteur { get; private set; }

        public RetireCompteurDomainCommand(Compteur compteur)
        {
            if (compteur is null) { throw new ArgumentNullException(nameof(compteur)); }

            Compteur = compteur;
        }

        public void RetireCompteur(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs, 
            RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL)
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }

            Compteur.RetirerCompteur();
            référentielCompteurs.EnregistrerCompteur(Compteur);
            int idCampagneReleve = 0;
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(Compteur.IdCompteur).OrderByDescending(m=>m.IdIndex).FirstOrDefault();
            if(existingIndex != null)
            {
                idCampagneReleve = existingIndex.IdCampagneReleve + 1;
            }
            var zeroIndex = new IndexCompteur(IdIndex.Parse(0), Compteur.IdCompteur, 0, idCampagneReleve, Horloge.Instance.GetDateTime());
            référentielIndexesCompteurs.EnregistreIndex(zeroIndex);
            if(Compteur.PDL == null) { return; }
            var adressesPDL = référentielAppairage.GetAdressesPDL(Compteur.PDL.IdPDL); 
            for (int i = 0; i < adressesPDL.Count; i++)
            {
                var adressePDL = adressesPDL[i];
                référentielAppairage.SupprimeAdressePDL(adressePDL.IdAdresse);
            }
            var appairages = référentielAppairage.GetAppairageOfPDL(Compteur.PDL.IdPDL);
            for(int i=0;i< appairages.Count; i++)
            {
                var appairage = appairages[i];
                référentielAppairage.SupprimeAppairagge(appairage);
            }
            référentielPDL.SupprimePDL(Compteur.PDL.IdPDL);
        }
    }
}
