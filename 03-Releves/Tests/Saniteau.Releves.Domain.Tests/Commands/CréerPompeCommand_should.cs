using NFluent;
using NUnit.Framework;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Releves.Domain.Commands;
using Saniteau.Releves.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain.Tests.Commands
{
    [TestFixture]
    public class CréerPompeCommand_should
    {
        [Test]
        public void créer_pompe()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_compteur");
            var référentielPompes = new RéférentielPompesOnEfCore(dbContextFactory);
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);

            var compteur = référentielCompteurs.EnregistrerCompteur(new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur de pompe principale"), true));
            var créerPompeCommand = new CréePompeDomainCommand(compteur.IdCompteur, new ChampLibre("Pompe principale"));

            //Exercise
            var pompe = créerPompeCommand.CréePompe(référentielPompes);

            //Verify
            Check.That((int)pompe.IdPompe).IsStrictlyGreaterThan(0);
        }
    }
}
