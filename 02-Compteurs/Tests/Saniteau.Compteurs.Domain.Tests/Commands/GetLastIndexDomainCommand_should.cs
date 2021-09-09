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
    public class GetLastIndexDomainCommand_should
    {
        [Test]
        public void return_last_index()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_compteur");
            var référentielCompteursOnEf = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);

            var créerCompteurCommand = new EnregistreCompteurDomainCommand(IdCompteur.Parse(0), new ChampLibre("AAA"));
            var newCompteur = créerCompteurCommand.EnregistreCompteur(référentielCompteursOnEf);

            var index2 = référentielIndexesCompteurs.EnregistreIndex(new IndexCompteur(IdIndex.Parse(0), newCompteur.IdCompteur, 2000, 0, new DateTime(2020, 01, 02)));
            var index3 = référentielIndexesCompteurs.EnregistreIndex(new IndexCompteur(IdIndex.Parse(0), newCompteur.IdCompteur, 3000, 0, new DateTime(2020, 01, 02)));
            var index1 = référentielIndexesCompteurs.EnregistreIndex(new IndexCompteur(IdIndex.Parse(0), newCompteur.IdCompteur, 1000, 0, new DateTime(2020, 01, 01)));

            var getLastIndexCommand = new GetLastIndexDomainCommand(newCompteur.IdCompteur);

            //Exercise
            var lastIndex = getLastIndexCommand.GetLastIndex(référentielIndexesCompteurs);

            //Verify
            Check.That(lastIndex.IndexM3).IsEqualTo(index3.IndexM3);
        }
    }
}
