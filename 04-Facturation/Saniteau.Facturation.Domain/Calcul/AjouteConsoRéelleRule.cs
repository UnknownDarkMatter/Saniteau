using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain.Calcul
{
    public class AjouteConsoRéelleRule : IRule
    {
        private readonly FacturationCalculator _facturationCalculator;
        public Facturation Facturation { get; }
        public List<IndexCompteur> IndexCompteurs { get; }
        public List<Facturation> FacturationsPrecedentes { get; }
        public AjouteConsoRéelleRule(Facturation facturation, List<Facturation> facturationsPrecedentes, List<IndexCompteur> indexCompteurs)
        {
            _facturationCalculator = new FacturationCalculator();
            Facturation = facturation;
            IndexCompteurs = indexCompteurs;
            FacturationsPrecedentes = facturationsPrecedentes;
        }

        public override bool IsSatisfied()
        {
            IndexCompteur dernierIndex = IndexCompteurs.OrderByDescending(m => (int)m.IdIndex).FirstOrDefault();
            if(dernierIndex is null)
            {
                return false;
            }
            var dernièreFacturation = FacturationsPrecedentes.OrderByDescending(m => m.DateFacturation).FirstOrDefault();
            if(dernièreFacturation != null)
            {
                if ((int)dernierIndex.IdIndex <= (int)dernièreFacturation.IdDernierIndex)
                {
                    return false;
                }
            }

            return true;
        }

        public override void ExecuteRule()
        {
            IndexCompteur indexPrécédent = null;
            var dernièreFacturation = FacturationsPrecedentes.OrderByDescending(m => (int)m.IdFacturation).FirstOrDefault();
            if(dernièreFacturation != null)
            {
                indexPrécédent = IndexCompteurs.First(m => m.IdIndex == dernièreFacturation.IdDernierIndex);
            }
            IndexCompteur dernierIndex = IndexCompteurs.OrderByDescending(m => (int)m.IdIndex).FirstOrDefault();
            int consommationM3 = dernierIndex.IndexM3;
            if(indexPrécédent != null)
            {
                consommationM3 = dernierIndex.IndexM3 - indexPrécédent.IndexM3; 
            }
            decimal prixM3 = _facturationCalculator.GetPrixM3(Facturation.Abonné.Tarification);
            decimal prixConsommation = ((decimal)consommationM3) * prixM3;
            if(prixConsommation == 0) { return; }

            FacturationLigne ligneFacturation = new FacturationLigne(
                IdFacturationLigne.Parse(0),
                Facturation.IdFacturation,
                ClasseLigneFacturation.ConsommationRéelle,
                Montant.FromDecimal(prixConsommation),
                consommationM3,
                Montant.FromDecimal(prixM3));
            Facturation.LignesFacturation.Add(ligneFacturation);


        }
    }
}
