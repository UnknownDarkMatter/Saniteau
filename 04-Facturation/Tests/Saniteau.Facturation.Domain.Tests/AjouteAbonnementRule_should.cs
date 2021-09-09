using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.Facturation.Domain.Calcul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain.Tests
{
    [TestFixture]
    public class AjouteAbonnementRule_should
    {
        private Abonné abonne = null;

        [SetUp]
        public void Initialize()
        {
            abonne = new Abonné(IdAbonné.Parse(0), IdAdresse.Parse(0), new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
        }

        [Test]
        public void calcule_abonnement_avec_dû()
        {
            //Setup
            var facturationsPrecedentes = new List<Facturation>();
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2018, 12, 31)));
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2019, 12, 31)));
            var facturation = CreateFacturation(new DateTime(2020, 12, 31));
            var abonnementRule = new AjouteAbonnementRule(facturation, facturationsPrecedentes);

            //Exercise
            if (abonnementRule.IsSatisfied())
            {
                abonnementRule.ExecuteRule();
            }

            //Verify
            Check.That((decimal)facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.Abonnement).MontantEuros).IsEqualTo(Constantes.Abonnement.Particulier);
        }

        [Test]
        public void calcule_abonnement_sans_dû()
        {
            //Setup
            var facturationsPrecedentes = new List<Facturation>();
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2018, 12, 31)));
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2019, 10, 31)));
            var facturation = CreateFacturation(new DateTime(2019, 12, 31));
            var abonnementRule = new AjouteAbonnementRule(facturation, facturationsPrecedentes);

            //Exercise
            if (abonnementRule.IsSatisfied())
            {
                abonnementRule.ExecuteRule();
            }

            //Verify
            Check.That(facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.Abonnement)).IsNull();
        }

        private Facturation CreateFacturation(DateTime dateFacturation)
        {
            return new Facturation(IdFacturation.Parse(0), 0, abonne, dateFacturation, new List<FacturationLigne>(), IdIndex.Parse(0), false);
        }
    }
}
