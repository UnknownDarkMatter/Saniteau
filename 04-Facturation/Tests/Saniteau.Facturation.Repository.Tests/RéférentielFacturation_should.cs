using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using Saniteau.Facturation.Repository;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Saniteau.Facturation.Repository.Tests
{
    public class RéférentielFacturation_should
    {
        [Test]
        public void create_facturation()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_facturation");
            var référentielFacturation = new RéférentielFacturationOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielIndexes = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnés.CréeAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bobo"), Tarification.Particulier);
            abonné = référentielAbonnés.EnregistreAbonné(abonné);
            var indexCompteur = new IndexCompteur(IdIndex.Parse(0), IdCompteur.Parse(0), 0, 50, new DateTime(2019, 12, 22));
            indexCompteur = référentielIndexes.EnregistreIndex(indexCompteur);
            var facturationLignes = new List<FacturationLigne>()
            {
                new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.Abonnement, Montant.FromDecimal(150), 100, Montant.FromDecimal(0)),
                new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.ConsommationRéelle, Montant.FromDecimal(250), 200, Montant.FromDecimal(Constantes.PrixM3.Particulier)),
            };
            var facturation = new Domain.Facturation(IdFacturation.Parse(0), 0, abonné, new DateTime(2020, 01, 31), facturationLignes, indexCompteur.IdIndex, false);

            //Exercise
            facturation = référentielFacturation.EnregistrerFacturation(facturation);

            //Verify
            Check.That((int)facturation.IdFacturation).IsStrictlyGreaterThan(0);
            Check.That((int)facturation.LignesFacturation[0].IdFacturationLigne).IsStrictlyGreaterThan(0);
            Check.That((int)facturation.LignesFacturation[1].IdFacturationLigne).IsStrictlyGreaterThan((int)facturation.LignesFacturation[0].IdFacturationLigne);
        }

        [Test]
        public void update_facturation()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_facturation");
            var référentielFacturation = new RéférentielFacturationOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielIndexes = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnés.CréeAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bobo"), Tarification.Particulier);
            abonné = référentielAbonnés.EnregistreAbonné(abonné);
            var indexCompteur = new IndexCompteur(IdIndex.Parse(0), IdCompteur.Parse(0), 0, 50, new DateTime(2019, 12, 22));
            indexCompteur = référentielIndexes.EnregistreIndex(indexCompteur);
            var facturationLignes = new List<FacturationLigne>()
            {
                new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.Abonnement, Montant.FromDecimal(150), 100, Montant.FromDecimal(0)),
                new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.ConsommationRéelle, Montant.FromDecimal(250), 200, Montant.FromDecimal(Constantes.PrixM3.Particulier)),
                new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.ConsommationRéelle, Montant.FromDecimal(251), 201, Montant.FromDecimal(Constantes.PrixM3.Particulier)),
            };
            var facturation = new Domain.Facturation(IdFacturation.Parse(0), 0, abonné, new DateTime(2020, 01, 31), facturationLignes, indexCompteur.IdIndex, false);
            facturation = référentielFacturation.EnregistrerFacturation(facturation);

            facturation.LignesFacturation[0].SetClasse(ClasseLigneFacturation.ConsommationRéelle);
            facturation.LignesFacturation[0].SetConsommationM3(101);
            facturation.LignesFacturation[0].SetMontantEuros(Montant.FromDecimal(101.1M));
            facturation.LignesFacturation.RemoveAt(1);
            facturation.LignesFacturation.RemoveAt(1);
            facturation.LignesFacturation.Add(new FacturationLigne(IdFacturationLigne.Parse(0), IdFacturation.Parse(0), ClasseLigneFacturation.ConsommationRéelle, Montant.FromDecimal(250), 301, Montant.FromDecimal(Constantes.PrixM3.Particulier)));

            //Exercise
            facturation = référentielFacturation.EnregistrerFacturation(facturation);

            //Verify
            Check.That(facturation.LignesFacturation.Count).IsEqualTo(2);
            Check.That(facturation.LignesFacturation[0].Classe).IsEqualTo(ClasseLigneFacturation.ConsommationRéelle);
            Check.That((int)facturation.LignesFacturation[0].ConsommationM3).IsEqualTo(101);
            Check.That((decimal)facturation.LignesFacturation[0].MontantEuros).IsEqualTo(101.1M);
            Check.That((int)facturation.LignesFacturation[1].ConsommationM3).IsEqualTo(301);

        }

    }
}
