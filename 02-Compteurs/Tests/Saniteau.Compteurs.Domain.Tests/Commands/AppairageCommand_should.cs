using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
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
    public class AppairageCommand_should
    {
        [Test]
        public void appairer_compteur_successfully()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("appairer_compteur_successfully");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPDL = new RéférentielPDLOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairage = new RéférentielAppairageOnEfCore(dbContextFactory);

            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnés.EnregistreAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
            abonné = référentielAbonnés.EnregistreAbonné(abonné);
            Compteur compteur = référentielCompteurs.CréerCompteur(new ChampLibre("compteur de Bob Marley"));
            var poserCompteurCommand = new PoseCompteurDomainCommand(compteur);
            poserCompteurCommand.PoseCompteur(référentielCompteurs, référentielIndexesCompteurs);
            PDL pdl = référentielPDL.CréeNouveauPDL(new PDL(IdPDL.Parse(0), new ChampLibre("Point de livraison de Bob Marley")));

            var now = new DateTime(2019, 11, 20);
            var horlogeMock = new HorlogeMock(now);
            Horloge.Mock(horlogeMock);

            var appairageDomainCommand = new AppairageDomainCommand(IdAppairageCompteur.Parse(0), adresse.IdAdresse, compteur.IdCompteur, pdl.IdPDL);

            //Exercise
            var appairageCompteur = appairageDomainCommand.AppairageCompteur(référentielCompteurs, référentielPDL, référentielAbonnés, référentielAppairage);

            //Verify
            var appairagesOfPDL = référentielAppairage.GetAppairageOfPDL(pdl.IdPDL);
            Check.That(appairagesOfPDL.Count).IsEqualTo(1);
            Check.That((int)appairagesOfPDL[0].IdCompteur).IsEqualTo((int)compteur.IdCompteur);
            Check.That((int)appairagesOfPDL[0].IdPDL).IsEqualTo((int)pdl.IdPDL);
            Check.That(appairagesOfPDL[0].DateAppairage).IsEqualTo(Date.FromDateTime(now));
            Check.That(appairagesOfPDL[0].DateDésappairage).IsNull();
        }

        [Test]
        public void appairer_compteur_unsuccessfully_because_compteur_not_posé()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("appairer_compteur_unsuccessfully_because_compteur_not_posé");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPDL = new RéférentielPDLOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairage = new RéférentielAppairageOnEfCore(dbContextFactory);

            Adresse adresse = new Adresse(IdAdresse.Parse(0), new ChampLibre("10 rue des petits pas"), new ChampLibre("PARIS"), new ChampLibre("75000"));
            adresse = référentielAbonnés.EnregistreAdresse(adresse);
            Abonné abonné = new Abonné(IdAbonné.Parse(0), adresse.IdAdresse, new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
            abonné = référentielAbonnés.EnregistreAbonné(abonné);
            Compteur compteur = référentielCompteurs.CréerCompteur(new ChampLibre("compteur de Bob Marley"));
            PDL pdl = référentielPDL.CréeNouveauPDL(new PDL(IdPDL.Parse(0), new ChampLibre("Point de livraison de Bob Marley")));

            var now = new DateTime(2019, 11, 20);
            var horlogeMock = new HorlogeMock(now);
            Horloge.Mock(horlogeMock);

            var appairageDomainCommand = new AppairageDomainCommand(IdAppairageCompteur.Parse(0), adresse.IdAdresse, compteur.IdCompteur, pdl.IdPDL);

            //Exercise && Verify
            Check.ThatCode(() => {
                appairageDomainCommand.AppairageCompteur(référentielCompteurs, référentielPDL, référentielAbonnés, référentielAppairage);
            }).Throws<BusinessException>();

        }

    }
}
