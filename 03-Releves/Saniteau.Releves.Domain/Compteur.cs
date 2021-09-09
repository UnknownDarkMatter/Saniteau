using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public class Compteur
    {
        public IdCompteur IdCompteur { get; private set; }
        public ChampLibre NuméroCompteur { get; private set; }
        public bool CompteurPosé { get; private set; }
        public Compteur(IdCompteur idCompteur, ChampLibre numéroCompteur, bool compteurPosé)
        {
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (numéroCompteur is null) { throw new ArgumentNullException(nameof(numéroCompteur)); }

            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
            CompteurPosé = compteurPosé;
        }

        public void PoserCompteur()
        {
            if (CompteurPosé)
            {
                throw new BusinessException($"Un compteur déjà posé ne peut pas être reposé (Compteur : {IdCompteur}.");
            }

            CompteurPosé = true;
        }

        public void DéposerCompteur()
        {
            if (!CompteurPosé)
            {
                throw new BusinessException($"Un compteur non posé ne peut pas être déposé (Compteur : {IdCompteur}.");
            }

            CompteurPosé = false;
        }

        public IndexCompteur GetLastIndexCompteur(RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            var indexes = référentielIndexesCompteurs.GetIndexesOfCompteur(IdCompteur);
            return indexes.OrderByDescending(m => m.DateIndex).ThenByDescending(m => m.IdIndex).FirstOrDefault();
        }
    }
}
