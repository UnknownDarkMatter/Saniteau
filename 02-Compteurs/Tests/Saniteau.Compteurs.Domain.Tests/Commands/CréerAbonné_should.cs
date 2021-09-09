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

namespace Saniteau.Compteurs.Domain.Tests.Commands
{
    [TestFixture]
    public class CréerAbonné_should
    {
        [Test]
        public void créer_abonné()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_adresse");
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var créerAdresseCommand = new EnregistreAdresseDomainCommand(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("Paris"), new ChampLibre("75000"));
            var adresse = créerAdresseCommand.EnregistreAdresse(référentielAbonnés);
            var créerAbonnéCommand = new EnregistreAbonnéDomainCommand(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);

            //Exercise
            var newAbonnéTuple = créerAbonnéCommand.EnregistreAbonné(référentielAbonnés);

            //Verify
            var abonnéResult = référentielAbonnés.GetAbonné(newAbonnéTuple.Item1.IdAbonné);
            var adresseResult = référentielAbonnés.GetAddresse(newAbonnéTuple.Item1.IdAdresse);
            Check.That((int)(newAbonnéTuple.Item1.IdAbonné)).IsEqualTo((int)abonnéResult.IdAbonné);
            Check.That(abonnéResult.Nom.ToString()).IsEqualTo("Marley");
            Check.That(abonnéResult.Prénom.ToString()).IsEqualTo("Bob");
            Check.That(adresseResult.NuméroEtRue.ToString()).IsEqualTo("10 rue des petits pas");
            Check.That(adresseResult.Ville.ToString()).IsEqualTo("Paris");
            Check.That(adresseResult.CodePostal.ToString()).IsEqualTo("75000");

        }

    }
}
