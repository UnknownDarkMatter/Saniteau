using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public class IndexCompteurMoteur
    {
        public const int IncrémentMinValue = 5;
        public const int IncrémentMaxValue = 15;

        /// <summary>
        /// Augmente les index des compteurs à partir d'une simulation aléatoire.
        /// N'ajoute qu'un seul index aux compteurs
        /// </summary>
        public IndexCompteurMoteur()
        {
            ReinitialiseMoteur();
        }

        /// <summary>
        /// Représente la valeur ajoutée aux compteurs, c'est un index relatif et non absolu
        /// </summary>
        public int IncrémentRelatifIndexCompteursM3 { get; private set; }

        private void AjouteIncrémentIndexCompteur(int incrémentM3)
        {
            IncrémentRelatifIndexCompteursM3 += incrémentM3;
        }

        public void ReinitialiseMoteur()
        {
            IncrémentRelatifIndexCompteursM3 = 0;
        }

        public void ExecuteIncrémentCompteurs(int idCampagneReleve, RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes)
        {
            Random random = new Random();
            var allPompes = référentielPompes.GetAllPompes();
            var pompe = allPompes.First();
            foreach (var compteur in référentielCompteurs.GetAllCompteurs().Where(c => c.CompteurPosé && !allPompes.Any(p=>p.IdCompteur == c.IdCompteur)))
            {
                int incrément = IncrémentMinValue + random.Next(IncrémentMinValue, IncrémentMaxValue);
                AjouteIncrementACompteur(compteur, incrément, idCampagneReleve, référentielIndexesCompteurs);
                AjouteIncrémentIndexCompteur(incrément);
            }
        }

        private void AjouteIncrementACompteur(Compteur compteur, int incrément, int idCampagneReleve, RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            int existingIndexValue = 0;
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur.IdCompteur).OrderByDescending(i => i.DateIndex).FirstOrDefault();
            if (existingIndex != null)
            {
                existingIndexValue += existingIndex.IndexM3;
            }
            int newIndex = existingIndexValue + incrément;
            var indexCompteur = new IndexCompteur(IdIndex.Parse(0), compteur.IdCompteur, idCampagneReleve, newIndex, Horloge.Instance.GetDateTime());
            référentielIndexesCompteurs.EnregistreIndex(indexCompteur);
        }
    }
}
