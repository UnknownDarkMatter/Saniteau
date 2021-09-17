using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public class Compteur
    {
        public IdCompteur IdCompteur { get; private set; }
        public ChampLibre NuméroCompteur { get; private set; }
        public bool CompteurPosé { get; private set; }
        public bool CompteurAppairé { get; private set; }
        public PDL PDL { get; set; }
        public IdAbonné IdAbonné { get; private set; }

        public Compteur(IdCompteur idCompteur, ChampLibre numéroCompteur, bool compteurPosé, bool compteurAppairé, PDL pdl, IdAbonné idAbonné)
        {
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (numéroCompteur is null) { throw new ArgumentNullException(nameof(numéroCompteur)); }

            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
            CompteurPosé = compteurPosé;
            CompteurAppairé = compteurAppairé;
            PDL = pdl;
            IdAbonné = idAbonné;
        }

        public void PoserCompteur()
        {
            if (CompteurPosé)
            {
                throw new BusinessException($"Un compteur déjà posé ne peut pas être reposé (Compteur : {IdCompteur}).");
            }

            CompteurPosé = true;
        }

        public void RetirerCompteur()
        {
            if (!CompteurPosé)
            {
                throw new BusinessException($"Un compteur non posé ne peut pas être retiré (Compteur : {IdCompteur}).");
            }

            CompteurPosé = false;
        }
        public IndexCompteur GetLastIndexCompteur(RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            var indexes = référentielIndexesCompteurs.GetIndexesOfCompteur(IdCompteur);
            return indexes.OrderByDescending(m => m.DateIndex).ThenByDescending(m => m.IdIndex).FirstOrDefault();
        }

        public void SetNuméroCompteur(ChampLibre numéroCompteur)
        {
            NuméroCompteur = numéroCompteur;
        }
    }
}
