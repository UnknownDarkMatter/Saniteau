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
    public class CréeNouveauPDLCommand_should
    {
        [Test]
        public void créer_PDL()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("créer_PDL");
            var référentielPDL = new RéférentielPDLOnEfCore(dbContextFactory);
            var créerCompteurCommand = new CréeNouveauPDLDomainCommand(new ChampLibre("mon PDL"));

            //Exercise
            var newPDL = créerCompteurCommand.CréeNouveauPDL(référentielPDL);

            //Verify
            var pdl = référentielPDL.GetPDL(newPDL.IdPDL);
            Check.That(pdl.NuméroPDL.ToString()).IsEqualTo("mon PDL");
        }

    }
}
