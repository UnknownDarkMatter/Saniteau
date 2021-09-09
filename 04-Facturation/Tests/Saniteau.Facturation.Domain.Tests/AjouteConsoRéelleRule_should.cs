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
    public class AjouteConsoRéelleRule_should
    {
        private Abonné abonne = null;

        [SetUp]
        public void Initialize()
        {
            abonne = new Abonné(IdAbonné.Parse(0), IdAdresse.Parse(0), new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
        }

        [Test]
        public void facture_sans_indexes_et_sans_derniere_facture()
        {
            //Setup
            var facturationsPrecedentes = new List<Facturation>();
            var facturation = CreateFacturation(new DateTime(2020, 12, 31), IdIndex.Parse(0));
            var indexes = new List<IndexCompteur>();
            var consoReeleRule = new AjouteConsoRéelleRule(facturation, facturationsPrecedentes, indexes);

            //Exercise
            if (consoReeleRule.IsSatisfied())
            {
                consoReeleRule.ExecuteRule();
            }

            //Verify
            Check.That(facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.ConsommationRéelle)).IsNull();
        }

        [Test]
        public void facture_avec_indexes_et_sans_derniere_facture()
        {
            //Setup
            var facturationsPrecedentes = new List<Facturation>();
            var facturation = CreateFacturation(new DateTime(2020, 12, 31), IdIndex.Parse(1));
            int conso = 150;
            var indexes = new List<IndexCompteur>();
            indexes.Add(new IndexCompteur(IdIndex.Parse(0), IdCompteur.Parse(0), 0,100, new DateTime(2019, 10, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(1), IdCompteur.Parse(0), 0,conso, new DateTime(2019, 11, 10)));
            var consoReeleRule = new AjouteConsoRéelleRule(facturation, facturationsPrecedentes, indexes);

            //Exercise
            if (consoReeleRule.IsSatisfied())
            {
                consoReeleRule.ExecuteRule();
            }

            //Verify
            Check.That((decimal)facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(Constantes.PrixM3.Particulier * conso);
        }

        [Test]
        public void facture_avec_inexes_depuis_derniere_facture()
        {
            //Setup
            var facturationsPrecedentes = new List<Facturation>();
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2019, 12, 31), IdIndex.Parse(2)));
            var facturation = CreateFacturation(new DateTime(2020, 12, 31), IdIndex.Parse(4));
            int consoPrecedente = 150;
            int consoActuelle = 251;
            var indexes = new List<IndexCompteur>();
            indexes.Add(new IndexCompteur(IdIndex.Parse(1), IdCompteur.Parse(0), 0, 100, new DateTime(2019, 10, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(2), IdCompteur.Parse(0), 0, consoPrecedente, new DateTime(2019, 11, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(3), IdCompteur.Parse(0), 0, 200, new DateTime(2020, 01, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(4), IdCompteur.Parse(0), 0, consoActuelle, new DateTime(2020, 02, 10)));
            var consoReeleRule = new AjouteConsoRéelleRule(facturation, facturationsPrecedentes, indexes);

            //Exercise
            if (consoReeleRule.IsSatisfied())
            {
                consoReeleRule.ExecuteRule();
            }

            //Verify
            Check.That((decimal)facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(Constantes.PrixM3.Particulier * (consoActuelle - consoPrecedente));
        }

        [Test]
        public void facture_sans_inexes_depuis_derniere_facture()
        {
            //Setup
            var indexes = new List<IndexCompteur>();
            indexes.Add(new IndexCompteur(IdIndex.Parse(1), IdCompteur.Parse(0), 0, 100, new DateTime(2019, 10, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(2), IdCompteur.Parse(0), 0, 150, new DateTime(2019, 11, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(3), IdCompteur.Parse(0), 0, 200, new DateTime(2020, 01, 10)));
            indexes.Add(new IndexCompteur(IdIndex.Parse(4), IdCompteur.Parse(0), 0, 250, new DateTime(2020, 02, 10)));
            var facturationsPrecedentes = new List<Facturation>();
            facturationsPrecedentes.Add(CreateFacturation(new DateTime(2020, 03, 31), IdIndex.Parse(4)));
            var facturation = CreateFacturation(new DateTime(2020, 12, 31), IdIndex.Parse(4));
            var consoReeleRule = new AjouteConsoRéelleRule(facturation, facturationsPrecedentes, indexes);

            //Exercise
            if (consoReeleRule.IsSatisfied())
            {
                consoReeleRule.ExecuteRule();
            }

            //Verify
            Check.That(facturation.LignesFacturation.FirstOrDefault(m => m.Classe == ClasseLigneFacturation.ConsommationRéelle)).IsNull();
        }

        private Facturation CreateFacturation(DateTime dateFacturation, IdIndex idIndex)
        {
            return new Facturation(IdFacturation.Parse(0), 0, abonne, dateFacturation, new List<FacturationLigne>(), idIndex, false);
        }

    }
}
