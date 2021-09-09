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
    public class CréeCompteurCommand_should
    {
        [Test]
        public void créer_compteur()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_compteur");
            var référentielCompteursOnEf = new RéférentielCompteursOnEfCore(dbContextFactory);
            var créerCompteurCommand = new EnregistreCompteurDomainCommand(IdCompteur.Parse(0), new ChampLibre("AAA"));

            //Exercise
            var newCompteur = créerCompteurCommand.EnregistreCompteur(référentielCompteursOnEf);

            //Verify
            var compteur = référentielCompteursOnEf.GetCompteur(newCompteur.IdCompteur);
            Check.That((int)newCompteur.IdCompteur).IsEqualTo((int)compteur.IdCompteur);
        }
    }
}
