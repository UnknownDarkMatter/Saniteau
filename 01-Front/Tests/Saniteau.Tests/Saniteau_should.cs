using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.Compteurs.Application.Handlers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Repository;
using Saniteau.DSP.Application.Handlers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.Facturation.Application.Handlers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Releves.Application.Handlers;
using Saniteau.Releves.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Saniteau.Tests
{
    [TestFixture]
    public class Saniteau_should
    {
        [Test, Order(1)]
        public void run_integration_tests_pas_tout_en_meme_temps()
        {
            //////// Compteurs ///////////
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("run_integration_tests_pas_tout_en_meme_temps");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPDL = new RéférentielPDLOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairage = new RéférentielAppairageOnEfCore(dbContextFactory);

            var now = new DateTime(2019, 11, 20);
            var horlogeMock = new HorlogeMock(now);
            Horloge.Mock(horlogeMock);

            var créerAdresseCommand = new EnregistreAdresseCommand(0, "10 rue des petits pas", "Paris", "75000");
            var créeAdresseCommandHandler = new EnregistreAdresseCommandHandler(référentielAbonnés);

            var adresse = créeAdresseCommandHandler.Handle(créerAdresseCommand);

            var créeAbonnéCommand = new EnregistreAbonnéCommand(0, adresse.IdAdresse, "Marley", "Bob", Compteurs.Contract.Model.Tarification.Particulier);
            var créeAbonnéCommandHandler = new EnregistreAbonnéCommandHandler(référentielAbonnés);
            var abonné = créeAbonnéCommandHandler.Handle(créeAbonnéCommand);

            var créeCompteurCommand = new EnregistreCompteurCommand(0, "Compteur de Bob Marley");
            var créeCompteurCommandHandler = new EnregistreCompteurCommandHandler(référentielCompteurs);
            var compteur = créeCompteurCommandHandler.Handle(créeCompteurCommand);

            var poseCompteurCommand = new PoseCompteurCommand(compteur);
            var poseCompteurCommandHandler = new PoseCompteurCommandHandler(référentielCompteurs, référentielIndexesCompteurs);
            poseCompteurCommandHandler.Handle(poseCompteurCommand);
            var créeNouveauPDLCommand = new CréeNouveauPDLCommand("Point de livraison de Bob Marley");
            var créeNouveauPDLCommandHandler = new CréeNouveauPDLCommandHandler(référentielPDL);
            var pdl = créeNouveauPDLCommandHandler.Handle(créeNouveauPDLCommand);
            var appairageCommand = new AppairageCommand(0, adresse.IdAdresse, compteur.IdCompteur, pdl.IdPDL);
            var appairageCommandHandler = new AppairageCommandHandler(référentielCompteurs, référentielPDL, référentielAbonnés, référentielAppairage);
            var appairage = appairageCommandHandler.Handle(appairageCommand);

            //////// Relevés ///////////
            Horloge.Mock(new HorlogeMock(new DateTime(2019, 12, 01)));
            var référentielCompteursOfRelevé = new Releves.Repository.RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfRelevé = new Releves.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPompesOfRelevé = new Releves.Repository.RéférentielPompesOnEfCore(dbContextFactory);

            var créeCompteurDePompeCommand = new EnregistreCompteurCommand(0, "Compteur de pompe principale");
            var créeCompteurDeCompeCommandHandler = new EnregistreCompteurCommandHandler(référentielCompteurs);
            var compteurDePompe = créeCompteurDeCompeCommandHandler.Handle(créeCompteurDePompeCommand);
            var créePompeCommand = new CréePompeCommand(compteurDePompe.IdCompteur, "Pompe principale");
            var créePompeCommandHandler = new CréePompeCommandHandler(référentielPompesOfRelevé);
            var pompe = créePompeCommandHandler.Handle(créePompeCommand);

            var getLastIndexCommand = new GetLastIndexCommand(compteur.IdCompteur);
            var getLastIndexCommandHandler = new GetLastIndexCommandHandler(référentielIndexesCompteurs);
            var indexAvantRelevé = getLastIndexCommandHandler.Handle(getLastIndexCommand);

            Horloge.Mock(new HorlogeMock(new DateTime(2020, 01, 01)));
            var incrémenteCompteursCommand = new IncrémenteCompteursCommand();
            var incrémenteCompteursCommandHandler = new IncrémenteCompteursCommandHandler(référentielCompteursOfRelevé, référentielIndexesCompteursOfRelevé, référentielPompesOfRelevé);
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());

            var indexAprèsRelevé = getLastIndexCommandHandler.Handle(getLastIndexCommand);
            Check.That(indexAprèsRelevé.IndexM3).IsStrictlyGreaterThan(indexAvantRelevé.IndexM3);

            //////// Facturation ///////////
            decimal sommeFacturation = 0;
            Horloge.Mock(new HorlogeMock(new DateTime(2020, 02, 01)));
            var référentielAbonnésOfFacturation = new Facturation.Repository.RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairageOfFacturation = new Facturation.Repository.RéférentielAppairageOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfFacturation = new Facturation.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielFacturation = new Facturation.Repository.RéférentielFacturationOnEfCore(dbContextFactory);

            var dateFacturation = new DateTime(2020, 02, 28);
            var créeFacturationCommand = new CréeFacturationCommand(dateFacturation);
            var créeFacturationCommandHandler = new CréeFacturationCommandHandler(référentielAbonnésOfFacturation, référentielAppairageOfFacturation,
                référentielIndexesCompteursOfFacturation, référentielFacturation);
            var facturations = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.Abonnement).MontantEuros).IsEqualTo(Constantes.Abonnement.Particulier);
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(indexAprèsRelevé.IndexM3 * Constantes.PrixM3.Particulier);
            sommeFacturation += facturations[0].FacturationLignes.Sum(m => m.MontantEuros);

            //on refait un relevé
            Horloge.Mock(new HorlogeMock(new DateTime(2020, 03, 01)));
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());
            var indexAprèsRelevé2 = getLastIndexCommandHandler.Handle(getLastIndexCommand);

            //on met à jour la facturation
            Horloge.Mock(new HorlogeMock(new DateTime(2020, 04, 01)));
            dateFacturation = new DateTime(2020, 04, 30);
            créeFacturationCommand = new CréeFacturationCommand(dateFacturation);
            facturations = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            Check.That(facturations[0].FacturationLignes.Where(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.Abonnement).Count()).IsEqualTo(0);
            int consoM3 = indexAprèsRelevé2.IndexM3 - indexAprèsRelevé.IndexM3;
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(consoM3 * Constantes.PrixM3.Particulier);
            sommeFacturation += facturations[0].FacturationLignes.Sum(m => m.MontantEuros);

            //////// DSP ///////////
            Horloge.Mock(new HorlogeMock(new DateTime(2020, 05, 01)));
            var référentielDelegantOfDSP = new DSP.Repository.RéférentielDelegantOnEfCore(dbContextFactory);
            var référentielPayeOfDSP = new DSP.Repository.RéférentielPayeOnEfCore(dbContextFactory);
            var référentielAbonnésOfDSP = new DSP.Repository.RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielFacturationOfDSP = new DSP.Repository.RéférentielFacturationOnEfCore(dbContextFactory);
            var référentielPompesOfDSP = new DSP.Repository.RéférentielPompesOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfDSP = new DSP.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielCompteursOfDSP = new DSP.Repository.RéférentielCompteursOnEfCore(dbContextFactory);

            var créeDelegantCommand = new CréeDelegantCommand("Ville de Paris", "1 rue de l'hotel de ville, 75000, PARIS", new Date(2019, 05, 20));
            var créeDelegantCommandHandler = new CréeDelegantCommandHandler(référentielDelegantOfDSP);
            var delegant = créeDelegantCommandHandler.Handle(créeDelegantCommand);

            var créePayeDspCommand = new CréePayeCommand(Horloge.Instance.GetDate(), delegant.IdDelegant); ;
            var créePayeDspCommandHandler = new CréePayeCommandHandler(référentielPayeOfDSP, référentielAbonnésOfDSP, référentielFacturationOfDSP, référentielPompesOfDSP, référentielIndexesCompteursOfDSP, référentielCompteursOfDSP);
            créePayeDspCommandHandler.Handle(créePayeDspCommand);

            var obtientPayesCommandHandler = new ObtientPayesCommandHandler(référentielPayeOfDSP);
            var obtientPayesCommand = new ObtientPayesCommand();
            var payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);

            var getLastIndexOfPompeCommand = new GetLastIndexCommand(pompe.IdCompteur);
            var indexPompe = getLastIndexCommandHandler.Handle(getLastIndexOfPompeCommand);

            decimal coutFuitesEuros = ((decimal)indexPompe.IndexM3 - (decimal)indexAprèsRelevé2.IndexM3) * Constantes.PrixM3.Fuites;
            decimal sommeFuitesDSP = 0;
            payesDelegant.ForEach(m => { sommeFuitesDSP += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).Sum(n => n.MontantEuros); });
            Check.That(sommeFuitesDSP).IsEqualTo(coutFuitesEuros);
            decimal sommeFacturationDSP = 0;
            payesDelegant.ForEach(m => { sommeFacturationDSP += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).Sum(n => n.MontantEuros); });
            Check.That(sommeFacturation).IsEqualTo(sommeFacturationDSP);
            decimal sommeRemunerationSaniteau = 0;
            payesDelegant.ForEach(m => { sommeRemunerationSaniteau += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).Sum(n => n.MontantEuros); });
            Check.That(sommeRemunerationSaniteau).IsEqualTo(((sommeFacturation - coutFuitesEuros) * Constantes.RemunerationSaniteauPourcentage / 100));

            //on relance une paye Delegant sans avoir créé de nouvelle facturation
            créePayeDspCommandHandler.Handle(créePayeDspCommand);
            payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);
            decimal sommeFuitesDSP2 = 0;
            payesDelegant[1].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).ToList().ForEach(m => { sommeFuitesDSP2 += m.MontantEuros; });
            decimal sommeFacturationDSP2 = 0;
            payesDelegant[1].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).ToList().ForEach(m => { sommeFacturationDSP2 += m.MontantEuros; });
            decimal sommeRemunerationSaniteau2 = 0;
            payesDelegant[1].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).ToList().ForEach(m => { sommeRemunerationSaniteau2 += m.MontantEuros; });
            Check.That(sommeFuitesDSP2).IsEqualTo(0);
            Check.That(sommeFacturationDSP2).IsEqualTo(0);
            Check.That(sommeRemunerationSaniteau2).IsEqualTo(0);

            //on relance une paye Delegant après avoir créé une nouvelle facturation
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());
            var indexAprèsRelevé3 = getLastIndexCommandHandler.Handle(getLastIndexCommand);
            var indexPompe3 = getLastIndexCommandHandler.Handle(getLastIndexOfPompeCommand);
            créeFacturationCommand = new CréeFacturationCommand(Horloge.Instance.GetDateTime());
            var facturations3 = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            créePayeDspCommandHandler.Handle(créePayeDspCommand);
            payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);
            decimal sommeFacturation3 = ((decimal)indexAprèsRelevé3.IndexM3 - (decimal)indexAprèsRelevé2.IndexM3) * Constantes.PrixM3.Particulier;
            Check.That(sommeFacturation3).IsEqualTo(facturations3[0].FacturationLignes.Sum(m => m.MontantEuros));

            decimal sommeFuitesDSP3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).ToList().ForEach(m => { sommeFuitesDSP3 += m.MontantEuros; });
            decimal sommeFacturationDSP3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).ToList().ForEach(m => { sommeFacturationDSP3 += m.MontantEuros; });
            decimal sommeRemunerationSaniteau3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).ToList().ForEach(m => { sommeRemunerationSaniteau3 += m.MontantEuros; });
            decimal coutFuitesEuros3 = ((decimal)indexPompe3.IndexM3 - (decimal)indexAprèsRelevé3.IndexM3) * Constantes.PrixM3.Fuites;
            Check.That(sommeFuitesDSP3).IsEqualTo(coutFuitesEuros3);
            Check.That(sommeFacturationDSP3).IsEqualTo(facturations3[0].FacturationLignes.Sum(m => m.MontantEuros));

            Horloge.UnMock();
        }


        [Test, Order(2)]
        public void run_integration_tests_tout_en_meme_temps()
        {
            Horloge.UnMock();
            //////// Compteurs ///////////
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("run_integration_tests_tout_en_meme_temps");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPDL = new RéférentielPDLOnEfCore(dbContextFactory);
            var référentielAbonnés = new RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairage = new RéférentielAppairageOnEfCore(dbContextFactory);

            var créerAdresseCommand = new EnregistreAdresseCommand(0, "10 rue des petits pas", "Paris", "75000");
            var créeAdresseCommandHandler = new EnregistreAdresseCommandHandler(référentielAbonnés);

            var adresse = créeAdresseCommandHandler.Handle(créerAdresseCommand);

            var créeAbonnéCommand = new EnregistreAbonnéCommand(0, adresse.IdAdresse, "Marley", "Bob", Compteurs.Contract.Model.Tarification.Particulier);
            var créeAbonnéCommandHandler = new EnregistreAbonnéCommandHandler(référentielAbonnés);
            var abonné = créeAbonnéCommandHandler.Handle(créeAbonnéCommand);

            var créeCompteurCommand = new EnregistreCompteurCommand(0, "Compteur de Bob Marley");
            var créeCompteurCommandHandler = new EnregistreCompteurCommandHandler(référentielCompteurs);
            var compteur = créeCompteurCommandHandler.Handle(créeCompteurCommand);
            var poseCompteurCommand = new PoseCompteurCommand(compteur);
            var poseCompteurCommandHandler = new PoseCompteurCommandHandler(référentielCompteurs, référentielIndexesCompteurs);
            poseCompteurCommandHandler.Handle(poseCompteurCommand);

            var créeNouveauPDLCommand = new CréeNouveauPDLCommand("Point de livraison de Bob Marley");
            var créeNouveauPDLCommandHandler = new CréeNouveauPDLCommandHandler(référentielPDL);
            var pdl = créeNouveauPDLCommandHandler.Handle(créeNouveauPDLCommand);

            var appairageCommand = new AppairageCommand(0, adresse.IdAdresse, compteur.IdCompteur, pdl.IdPDL);
            var appairageCommandHandler = new AppairageCommandHandler(référentielCompteurs, référentielPDL, référentielAbonnés, référentielAppairage);
            var appairage = appairageCommandHandler.Handle(appairageCommand);

            //////// Relevés ///////////
            var référentielCompteursOfRelevé = new Releves.Repository.RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfRelevé = new Releves.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPompesOfRelevé = new Releves.Repository.RéférentielPompesOnEfCore(dbContextFactory);

            var créeCompteurDePompeCommand = new EnregistreCompteurCommand(0, "Compteur de pompe principale");
            var créeCompteurDeCompeCommandHandler = new EnregistreCompteurCommandHandler(référentielCompteurs);
            var compteurDePompe = créeCompteurDeCompeCommandHandler.Handle(créeCompteurDePompeCommand);
            var créePompeCommand = new CréePompeCommand(compteurDePompe.IdCompteur, "Pompe principale");
            var créePompeCommandHandler = new CréePompeCommandHandler(référentielPompesOfRelevé);
            var pompe = créePompeCommandHandler.Handle(créePompeCommand);

            var getLastIndexCommand = new GetLastIndexCommand(compteur.IdCompteur);
            var getLastIndexCommandHandler = new GetLastIndexCommandHandler(référentielIndexesCompteurs);
            var indexAvantRelevé = getLastIndexCommandHandler.Handle(getLastIndexCommand);

            var incrémenteCompteursCommand = new IncrémenteCompteursCommand();
            var incrémenteCompteursCommandHandler = new IncrémenteCompteursCommandHandler(référentielCompteursOfRelevé, référentielIndexesCompteursOfRelevé, référentielPompesOfRelevé);
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());

            var indexAprèsRelevé = getLastIndexCommandHandler.Handle(getLastIndexCommand);
            Check.That(indexAprèsRelevé.IndexM3).IsStrictlyGreaterThan(indexAvantRelevé.IndexM3);

            //////// Facturation ///////////
            decimal sommeFacturation = 0;
            var référentielAbonnésOfFacturation = new Facturation.Repository.RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielAppairageOfFacturation = new Facturation.Repository.RéférentielAppairageOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfFacturation = new Facturation.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielFacturation = new Facturation.Repository.RéférentielFacturationOnEfCore(dbContextFactory);

            var dateFacturation = Horloge.Instance.GetDateTime();
            var créeFacturationCommand = new CréeFacturationCommand(dateFacturation);
            var créeFacturationCommandHandler = new CréeFacturationCommandHandler(référentielAbonnésOfFacturation, référentielAppairageOfFacturation,
                référentielIndexesCompteursOfFacturation, référentielFacturation);
            var facturations = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.Abonnement).MontantEuros).IsEqualTo(Constantes.Abonnement.Particulier);
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(indexAprèsRelevé.IndexM3 * Constantes.PrixM3.Particulier);
            sommeFacturation += facturations[0].FacturationLignes.Sum(m => m.MontantEuros);

            //on refait un relevé
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());
            var indexAprèsRelevé2 = getLastIndexCommandHandler.Handle(getLastIndexCommand);

            //on met à jour la facturation
            dateFacturation = Horloge.Instance.GetDateTime();
            créeFacturationCommand = new CréeFacturationCommand(dateFacturation);
            facturations = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            Check.That(facturations[0].FacturationLignes.Where(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.Abonnement).Count()).IsEqualTo(0);
            int consoM3 = indexAprèsRelevé2.IndexM3 - indexAprèsRelevé.IndexM3;
            Check.That(facturations[0].FacturationLignes.First(m => m.ClasseLigneFacturation == Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle).MontantEuros).IsEqualTo(consoM3 * Constantes.PrixM3.Particulier);
            sommeFacturation += facturations[0].FacturationLignes.Sum(m => m.MontantEuros);

            //////// DSP ///////////
            var référentielDelegantOfDSP = new DSP.Repository.RéférentielDelegantOnEfCore(dbContextFactory);
            var référentielPayeOfDSP = new DSP.Repository.RéférentielPayeOnEfCore(dbContextFactory);
            var référentielAbonnésOfDSP = new DSP.Repository.RéférentielAbonnésOnEfCore(dbContextFactory);
            var référentielFacturationOfDSP = new DSP.Repository.RéférentielFacturationOnEfCore(dbContextFactory);
            var référentielPompesOfDSP = new DSP.Repository.RéférentielPompesOnEfCore(dbContextFactory);
            var référentielIndexesCompteursOfDSP = new DSP.Repository.RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielCompteursOfDSP = new DSP.Repository.RéférentielCompteursOnEfCore(dbContextFactory);

            var créeDelegantCommand = new CréeDelegantCommand("Ville de Paris", "1 rue de l'hotel de ville, 75000, PARIS", new Date(2019, 05, 20));
            var créeDelegantCommandHandler = new CréeDelegantCommandHandler(référentielDelegantOfDSP);
            var delegant = créeDelegantCommandHandler.Handle(créeDelegantCommand);

            var créePayeDspCommand = new CréePayeCommand(Horloge.Instance.GetDate(), delegant.IdDelegant); ;
            var créePayeDspCommandHandler = new CréePayeCommandHandler(référentielPayeOfDSP, référentielAbonnésOfDSP, référentielFacturationOfDSP, référentielPompesOfDSP, référentielIndexesCompteursOfDSP, référentielCompteursOfDSP);
            créePayeDspCommandHandler.Handle(créePayeDspCommand);

            var obtientPayesCommandHandler = new ObtientPayesCommandHandler(référentielPayeOfDSP);
            var obtientPayesCommand = new ObtientPayesCommand();
            var payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);

            var getLastIndexOfPompeCommand = new GetLastIndexCommand(pompe.IdCompteur);
            var indexPompe = getLastIndexCommandHandler.Handle(getLastIndexOfPompeCommand);

            decimal coutFuitesEuros = ((decimal)indexPompe.IndexM3 - (decimal)indexAprèsRelevé2.IndexM3) * Constantes.PrixM3.Fuites;
            decimal sommeFuitesDSP = 0;
            payesDelegant.ForEach(m => { sommeFuitesDSP += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).Sum(n => n.MontantEuros); });
            Check.That(sommeFuitesDSP).IsEqualTo(coutFuitesEuros);
            decimal sommeFacturationDSP = 0;
            payesDelegant.ForEach(m => { sommeFacturationDSP += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).Sum(n => n.MontantEuros);  });
            Check.That(sommeFacturation).IsEqualTo(sommeFacturationDSP);
            decimal sommeRemunerationSaniteau = 0;
            payesDelegant.ForEach(m => { sommeRemunerationSaniteau += m.LignesPaye.Where(n => n.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).Sum(n => n.MontantEuros); });
            Check.That(sommeRemunerationSaniteau).IsEqualTo(((sommeFacturation - coutFuitesEuros ) * Constantes.RemunerationSaniteauPourcentage/100));

            //on relance une paye Delegant sans avoir créé de nouvelle facturation
            créePayeDspCommandHandler.Handle(créePayeDspCommand);
            payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);
            decimal sommeFuitesDSP2 = 0;
            payesDelegant[1].LignesPaye.Where(m=>m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).ToList().ForEach(m => { sommeFuitesDSP2 += m.MontantEuros; });
            decimal sommeFacturationDSP2 = 0;
            payesDelegant[1].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).ToList().ForEach(m => { sommeFacturationDSP2 += m.MontantEuros; });
            decimal sommeRemunerationSaniteau2 = 0;
            payesDelegant[1].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).ToList().ForEach(m => { sommeRemunerationSaniteau2 += m.MontantEuros; });
            Check.That(sommeFuitesDSP2).IsEqualTo(0);
            Check.That(sommeFacturationDSP2).IsEqualTo(0);
            Check.That(sommeRemunerationSaniteau2).IsEqualTo(0);

            //on relance une paye Delegant après avoir créé une nouvelle facturation
            incrémenteCompteursCommandHandler.Handle(new IncrémenteCompteursCommand());
            var indexAprèsRelevé3 = getLastIndexCommandHandler.Handle(getLastIndexCommand);
            var indexPompe3 = getLastIndexCommandHandler.Handle(getLastIndexOfPompeCommand);
            var facturations3 = créeFacturationCommandHandler.Handle(créeFacturationCommand);
            créePayeDspCommandHandler.Handle(créePayeDspCommand);
            payesDelegant = obtientPayesCommandHandler.Handle(obtientPayesCommand);
            decimal sommeFacturation3 = ((decimal)indexAprèsRelevé3.IndexM3 - (decimal)indexAprèsRelevé2.IndexM3) * Constantes.PrixM3.Particulier;
            Check.That(sommeFacturation3).IsEqualTo(facturations3[0].FacturationLignes.Sum(m => m.MontantEuros));

            decimal sommeFuitesDSP3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.ConsommationFuites).ToList().ForEach(m => { sommeFuitesDSP3 += m.MontantEuros; });
            decimal sommeFacturationDSP3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés).ToList().ForEach(m => { sommeFacturationDSP3 += m.MontantEuros; });
            decimal sommeRemunerationSaniteau3 = 0;
            payesDelegant[2].LignesPaye.Where(m => m.Classe == DSP.Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau).ToList().ForEach(m => { sommeRemunerationSaniteau3 += m.MontantEuros; });
            decimal coutFuitesEuros3 = ((decimal)indexPompe3.IndexM3 - (decimal)indexAprèsRelevé3.IndexM3) * Constantes.PrixM3.Fuites;
            Check.That(sommeFuitesDSP3).IsEqualTo(coutFuitesEuros3);
            Check.That(sommeFacturationDSP3).IsEqualTo(facturations3[0].FacturationLignes.Sum(m=>m.MontantEuros));

        }


    }
}
