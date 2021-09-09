using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class PoseCompteurDomainCommand
    {
        public Compteur Compteur { get; private set; }

        public PoseCompteurDomainCommand(Compteur compteur)
        {
            if (compteur is null) { throw new ArgumentNullException(nameof(compteur)); }

            Compteur = compteur;
        }

        public void PoseCompteur(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }

            Compteur.PoserCompteur();
            référentielCompteurs.EnregistrerCompteur(Compteur);
            int idCampagneReleve = 0;
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(Compteur.IdCompteur).OrderByDescending(m => m.IdIndex).FirstOrDefault();
            if(existingIndex != null)
            {
                idCampagneReleve = existingIndex.IdCampagneReleve + 1;
            }
            var zeroIndex = new IndexCompteur(IdIndex.Parse(0), Compteur.IdCompteur, 0, idCampagneReleve, Horloge.Instance.GetDateTime());
            référentielIndexesCompteurs.EnregistreIndex(zeroIndex);
        }
    }
}
