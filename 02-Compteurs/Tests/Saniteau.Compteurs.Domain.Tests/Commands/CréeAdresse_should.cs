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
    public class CréeAdresse_should
    {
        [Test]
        public void créer_adresse()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_adresse");
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var créerAdresseCommand = new EnregistreAdresseDomainCommand(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("Paris"), new ChampLibre("75000"));

            //Exercise
            var newAdresse = créerAdresseCommand.EnregistreAdresse(référentielAbonnés);

            //Verify
            var adresse = référentielAbonnés.GetAddresse(newAdresse.IdAdresse);
            Check.That((int)newAdresse.IdAdresse).IsEqualTo((int)adresse.IdAdresse);
            Check.That(adresse.NuméroEtRue.ToString()).IsEqualTo("10 rue des petits pas");
            Check.That(adresse.Ville.ToString()).IsEqualTo("Paris");
            Check.That(adresse.CodePostal.ToString()).IsEqualTo("75000");
        }

    }
}
