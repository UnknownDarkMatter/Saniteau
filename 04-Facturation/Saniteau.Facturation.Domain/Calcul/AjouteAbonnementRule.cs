using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain.Calcul
{
    public class AjouteAbonnementRule : IRule
    {
        private readonly FacturationCalculator _facturationCalculator;
        public Facturation Facturation { get; }
        public List<Facturation> FacturationsPrecedentes { get; }

        public AjouteAbonnementRule(Facturation facturation, List<Facturation> facturationsPrecedentes)
        {
            _facturationCalculator = new FacturationCalculator();
            Facturation = facturation;
            FacturationsPrecedentes = facturationsPrecedentes;
        }

        public override bool IsSatisfied()
        {
            Facturation lastFacturation = FacturationsPrecedentes.OrderByDescending(m => m.DateFacturation).FirstOrDefault();
            if (lastFacturation == null)
            {
                return true;
            }
            if(lastFacturation.DateFacturation.Year < Facturation.DateFacturation.Year)
            {
                return true;
            }

            return false;
        }
        public override void ExecuteRule()
        {
            var montant = _facturationCalculator.GetAbonnement(Facturation.Abonné.Tarification);
            FacturationLigne ligneFacturation = new FacturationLigne(
                IdFacturationLigne.Parse(0),
                Facturation.IdFacturation,
                ClasseLigneFacturation.Abonnement,
                Montant.FromDecimal(montant),
                0,
                Montant.FromDecimal(0));
            Facturation.LignesFacturation.Add(ligneFacturation);
        }


    }
}
