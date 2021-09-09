using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Saniteau.DSP.Repository.Tests
{
    [TestFixture]
    public class RéférentielPaye_should
    {
        [Test]
        public void créer_PayeDelegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_PayeDelegant");
            var référentielPaye = new RéférentielPayeOnEfCore(dbContextFactory);
            PayeDelegant payeDelegant = new PayeDelegant(IdPayeDelegant.Parse(0), IdDelegant.Parse(1), new Date(2019, 05, 20), new List<PayeDelegantLigne>()
            {
                new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), ClasseLignePayeDelegant.FacturationAbonnés, Montant.FromDecimal(200.2M)),
                new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), ClasseLignePayeDelegant.RéumnérationSaniteau, Montant.FromDecimal(100.1M)),
            });

            //Exercise
            payeDelegant = référentielPaye.EnregistrePayeDelegant(payeDelegant);

            //Vérify
            var savedPayeDelegant = référentielPaye.GetPayesDelegants()[0];
            Check.That((int)savedPayeDelegant.IdPayeDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.IdDelegant).IsEqualTo(1);
            Check.That(savedPayeDelegant.DatePaye).IsEqualTo(new Date(2019, 05, 20));

            Check.That(savedPayeDelegant.LignesPaye.Count).IsEqualTo(2);

            Check.That((int)savedPayeDelegant.LignesPaye[0].IdPayeDelegantLigne).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.LignesPaye[0].IdPayeDelegant).IsEqualTo((int)payeDelegant.IdPayeDelegant);
            Check.That(savedPayeDelegant.LignesPaye[0].Classe).IsEqualTo(ClasseLignePayeDelegant.FacturationAbonnés);
            Check.That((decimal)savedPayeDelegant.LignesPaye[0].MontantEuros).IsEqualTo(200.2M);

            Check.That((int)savedPayeDelegant.LignesPaye[1].IdPayeDelegantLigne).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.LignesPaye[1].IdPayeDelegant).IsEqualTo((int)payeDelegant.IdPayeDelegant);
            Check.That(savedPayeDelegant.LignesPaye[1].Classe).IsEqualTo(ClasseLignePayeDelegant.RéumnérationSaniteau);
            Check.That((decimal)savedPayeDelegant.LignesPaye[1].MontantEuros).IsEqualTo(100.1M);
        }

        [Test]
        public void update_PayeDelegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("update_PayeDelegant");
            var référentielPaye = new RéférentielPayeOnEfCore(dbContextFactory);
            PayeDelegant payeDelegant = new PayeDelegant(IdPayeDelegant.Parse(0), IdDelegant.Parse(1), new Date(2019, 05, 20), new List<PayeDelegantLigne>()
            {
                new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), ClasseLignePayeDelegant.FacturationAbonnés, Montant.FromDecimal(200.2M)),
                new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), ClasseLignePayeDelegant.RéumnérationSaniteau, Montant.FromDecimal(100.1M)),
            });
            payeDelegant = référentielPaye.EnregistrePayeDelegant(payeDelegant);
            payeDelegant.ChangeDatePaye(new Date(2019, 05, 21));
            payeDelegant.LignesPaye.RemoveAt(1);
            payeDelegant.LignesPaye.Add(new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), ClasseLignePayeDelegant.ConsommationFuites, Montant.FromDecimal(50.5M)));
            payeDelegant.LignesPaye[0].ChangeMontantEuros(Montant.FromDecimal(201.2M));
            payeDelegant.LignesPaye[0].ChangeClasse(ClasseLignePayeDelegant.RéumnérationSaniteau);

            //Exercise
            payeDelegant = référentielPaye.EnregistrePayeDelegant(payeDelegant);

            //Vérify
            var savedPayeDelegant = référentielPaye.GetPayesDelegants()[0];
            Check.That((int)savedPayeDelegant.IdPayeDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.IdDelegant).IsEqualTo(1);
            Check.That(savedPayeDelegant.DatePaye).IsEqualTo(new Date(2019, 05, 21));

            Check.That(savedPayeDelegant.LignesPaye.Count).IsEqualTo(2);

            Check.That((int)savedPayeDelegant.LignesPaye[0].IdPayeDelegantLigne).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.LignesPaye[0].IdPayeDelegant).IsEqualTo((int)payeDelegant.IdPayeDelegant);
            Check.That(savedPayeDelegant.LignesPaye[0].Classe).IsEqualTo(ClasseLignePayeDelegant.RéumnérationSaniteau);
            Check.That((decimal)savedPayeDelegant.LignesPaye[0].MontantEuros).IsEqualTo(201.2M);

            Check.That((int)savedPayeDelegant.LignesPaye[1].IdPayeDelegantLigne).IsStrictlyGreaterThan(0);
            Check.That((int)savedPayeDelegant.LignesPaye[1].IdPayeDelegant).IsEqualTo((int)payeDelegant.IdPayeDelegant);
            Check.That(savedPayeDelegant.LignesPaye[1].Classe).IsEqualTo(ClasseLignePayeDelegant.ConsommationFuites);
            Check.That((decimal)savedPayeDelegant.LignesPaye[1].MontantEuros).IsEqualTo(50.5M);
        }

        [Test]
        public void créer_IndexPayéParDelegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_IndexPayéParDelegant");
            var référentielPaye = new RéférentielPayeOnEfCore(dbContextFactory);
            var indexPayéParDelegant = new IndexPayéParDelegant(IdIndexPayéParDelegant.Parse(0), IdPayeDelegant.Parse(1), IdCompteur.Parse(1), IdIndex.Parse(1));

            //Exercise
            indexPayéParDelegant = référentielPaye.EnregistreIndexesPayésParDelegant(indexPayéParDelegant);

            //Vérify
            var savedIndexPayéParDelegant = référentielPaye.GetIndexesPayesParDelegants()[0];
            Check.That((int)indexPayéParDelegant.IdIndexPayéParDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedIndexPayéParDelegant.IdIndexPayéParDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedIndexPayéParDelegant.IdIndexPayéParDelegant).IsEqualTo((int)indexPayéParDelegant.IdIndexPayéParDelegant);
            Check.That((int)savedIndexPayéParDelegant.IdPayeDelegant).IsEqualTo(1);
            Check.That((int)savedIndexPayéParDelegant.IdCompteur).IsEqualTo(1);
            Check.That((int)savedIndexPayéParDelegant.IdIndex).IsEqualTo(1);
        }

        [Test]
        public void créer_FacturePayeeAuDelegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_FacturePayeeAuDelegant");
            var référentielPaye = new RéférentielPayeOnEfCore(dbContextFactory);
            var facturePayeeAuDelegant = new FacturePayeeAuDelegant(IdFacturePayeeAuDelegant.Parse(0), IdPayeDelegant.Parse(1), IdFacturation.Parse(1), IdAbonné.Parse(1));

            //Exercise
            facturePayeeAuDelegant = référentielPaye.EnregistreFacturePayeeAuDelegant(facturePayeeAuDelegant);

            //Vérify
            var savedFacturePayeeAuDelegant = référentielPaye.GetFacturesPayeesAuDelegants()[0];
            Check.That((int)facturePayeeAuDelegant.IdFacturePayeeAuDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedFacturePayeeAuDelegant.IdFacturePayeeAuDelegant).IsStrictlyGreaterThan(0);
            Check.That((int)savedFacturePayeeAuDelegant.IdFacturePayeeAuDelegant).IsEqualTo((int)facturePayeeAuDelegant.IdFacturePayeeAuDelegant);
            Check.That((int)savedFacturePayeeAuDelegant.IdPayeDelegant).IsEqualTo(1);
            Check.That((int)savedFacturePayeeAuDelegant.IdFacturation).IsEqualTo(1);
            Check.That((int)savedFacturePayeeAuDelegant.IdAbonné).IsEqualTo(1);
        }


    }
}
