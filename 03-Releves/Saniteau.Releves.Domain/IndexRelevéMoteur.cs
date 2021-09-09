using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public class IndexRelevéMoteur
    {
        public const int PourcentageFuitesMin = 20;
        public const int PourcentageFuitesMax = 60;

        public IndexRelevéMoteur()
        {
            ReinitialiseMoteur();
        }

        /// <summary>
        /// Représente la valeur ajoutée aux compteurs, c'est un index relatif et non absolu
        /// </summary>
        public int IncrémentRelatifIndexCompteursM3 { get; private set; }
        public int PourcentageFuites { get; private set; }

        private void AjouteIncrémentIndexCompteur(int incrémentM3)
        {
            IncrémentRelatifIndexCompteursM3 += incrémentM3;
        }

        public void ReinitialiseMoteur()
        {
            IncrémentRelatifIndexCompteursM3 = 0;
            PourcentageFuites = 0;
        }

        public void ExecuteRelevé(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes)
        {
            Random random = new Random();
            var indexCompteurMoteur = new IndexCompteurMoteur();
            int idCampagneReleve = GetIdCampagneReleve(référentielIndexesCompteurs, référentielPompes);
            indexCompteurMoteur.ExecuteIncrémentCompteurs(idCampagneReleve, référentielCompteurs, référentielIndexesCompteurs, référentielPompes);

            PourcentageFuites = random.Next(PourcentageFuitesMin, PourcentageFuitesMax);
            int incrémentAllPompes = (indexCompteurMoteur.IncrémentRelatifIndexCompteursM3 * (PourcentageFuites + 100)) / 100;

            AjouteIncrémentIndexCompteur(incrémentAllPompes);
            AjouteIncrémentPompes(incrémentAllPompes, idCampagneReleve, référentielIndexesCompteurs, référentielPompes);
        }

        private int GetIdCampagneReleve(RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes)
        {
            int idCampagneReleve = 0;
            var pompes = référentielPompes.GetAllPompes();
            if (pompes.Count == 0)
            {
                return idCampagneReleve;
            }
            var pompe = pompes[0];
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe.IdCompteur).OrderByDescending(m => m.IdIndex).FirstOrDefault();
            if (existingIndex != null)
            {
                idCampagneReleve = existingIndex.IdCampagneReleve + 1;
            }
            return idCampagneReleve;
        }

        private void AjouteIncrémentPompes(int incrémentAllPompes, int idCampagneReleve, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes)
        {
            var pompes = référentielPompes.GetAllPompes();
            if(pompes.Count == 0)
            {
                return;
            }
            var pompe = pompes[0];
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe.IdCompteur).OrderByDescending(m => m.IdIndex).FirstOrDefault();
            int incrément = incrémentAllPompes / pompes.Count;
            int sommeIncréments = 0;
            for (int i = 0; i < pompes.Count; i++)
            {
                if (i == pompes.Count - 1)
                {
                    int dernierIncrement = incrémentAllPompes - sommeIncréments;
                    AjouteIncrementAPompe(pompes[i], dernierIncrement, idCampagneReleve, référentielIndexesCompteurs);
                    break;
                }
                AjouteIncrementAPompe(pompes[i], incrément, idCampagneReleve, référentielIndexesCompteurs);
                sommeIncréments += incrément;
            }
        }

        private void AjouteIncrementAPompe(Pompe pompe, int incrément, int idCampagneReleve, RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            int existingIndexValue = 0;
            var existingIndex = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe.IdCompteur).OrderByDescending(i => i.DateIndex).FirstOrDefault();
            if (existingIndex != null)
            {
                existingIndexValue += existingIndex.IndexM3;
            }
            int newIndex = existingIndexValue + incrément;
            var indexCompteur = new IndexCompteur(IdIndex.Parse(0), pompe.IdCompteur, idCampagneReleve, newIndex, Horloge.Instance.GetDateTime());
            référentielIndexesCompteurs.EnregistreIndex(indexCompteur);
        }

    }
}
