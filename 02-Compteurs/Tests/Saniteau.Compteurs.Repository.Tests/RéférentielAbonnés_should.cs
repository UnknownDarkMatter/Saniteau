using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using Saniteau.Compteurs.Repository;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Saniteau.Compteurs.Repository.Tests
{
    [TestFixture]
    public class RéférentielAbonnés_should
    {
        [Test]
        public void create_adress()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_adress");
            var référentielAbonnnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));

            //Exercise
            adresse = référentielAbonnnés.EnregistreAdresse(adresse);

            //Vérify
            var savedAdress = référentielAbonnnés.GetAddresse(adresse.IdAdresse);
            Check.That(savedAdress.NuméroEtRue.ToString()).IsEqualTo("10 rue des petits pas");
            Check.That(savedAdress.Ville.ToString()).IsEqualTo("PARIS");
            Check.That(savedAdress.CodePostal.ToString()).IsEqualTo("75000");
        }

        [Test]
        public void create_abonné()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_adress");
            var référentielAbonnnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnnés.EnregistreAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);

            //Exercise
            abonné = référentielAbonnnés.EnregistreAbonné(abonné);

            //Vérify
            var savedAbonné = référentielAbonnnés.GetAbonné(abonné.IdAbonné);
            Check.That(savedAbonné.Nom.ToString()).IsEqualTo("Marley");
            Check.That(savedAbonné.Prénom.ToString()).IsEqualTo("Bob");
        }

        [Test]
        public void update_abonné()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_adress");
            var référentielAbonnnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnnés.EnregistreAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
            abonné = référentielAbonnnés.EnregistreAbonné(abonné);
            abonné.ChangeNom(new ChampLibre("Mandela"));
            abonné.ChangePrénom(new ChampLibre("Nelson"));

            //Exercise
            abonné = référentielAbonnnés.EnregistreAbonné(abonné);

            //Vérify
            var savedAbonné = référentielAbonnnés.GetAbonné(abonné.IdAbonné);
            Check.That(savedAbonné.Nom.ToString()).IsEqualTo("Mandela");
            Check.That(savedAbonné.Prénom.ToString()).IsEqualTo("Nelson");
        }
    }
}