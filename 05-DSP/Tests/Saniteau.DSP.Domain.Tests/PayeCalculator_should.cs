using Moq;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Domain.Tests
{
    [TestFixture]
    public class PayeCalculator_should
    {
        [Test]
        public void calculate_paye_delegant()
        {
            //Setup
            Date datePaye = DateTime.Now.ToDate();
            IdDelegant idDelegant = IdDelegant.Parse(1);
            var référentielPaye = new Mock<RéférentielPaye>();
            var référentielAbonnés = new Mock<RéférentielAbonnés>();
            var référentielFacturation = new Mock<RéférentielFacturation>();
            var référentielPompes = new Mock<RéférentielPompes>();
            var référentielCompteurs = new Mock<RéférentielCompteurs>();
            var référentielIndexesCompteurs = new Mock<RéférentielIndexesCompteurs>();
            Abonné abonné = new Abonné(IdAbonné.Parse(1), IdAdresse.Parse(1), new ChampLibre("Marley"), new ChampLibre("Bob"), Tarification.Particulier);
            IdCompteur idCompteurAbonné = IdCompteur.Parse(1);
            IdCompteur idCompteurPompe = IdCompteur.Parse(2);
            IndexCompteur indexCompteur1 = new IndexCompteur(IdIndex.Parse(1), idCompteurAbonné, 0, 100, datePaye.ToDateTime(0, 0, 0));
            IndexCompteur indexCompteur2 = new IndexCompteur(IdIndex.Parse(2), idCompteurAbonné, 0, 200, datePaye.ToDateTime(0, 0, 0));
            IndexCompteur indexCompteur3 = new IndexCompteur(IdIndex.Parse(3), idCompteurAbonné, 0, 300, datePaye.ToDateTime(0, 0, 0));
            IndexCompteur indexCompteurPompe1 = new IndexCompteur(IdIndex.Parse(4), idCompteurPompe, 0, 151, datePaye.ToDateTime(0, 0, 0));
            IndexCompteur indexCompteurPompe2 = new IndexCompteur(IdIndex.Parse(5), idCompteurPompe, 0, 252, datePaye.ToDateTime(0, 0, 0));
            IndexCompteur indexCompteurPompe3 = new IndexCompteur(IdIndex.Parse(6), idCompteurPompe, 0, 353, datePaye.ToDateTime(0, 0, 0));
            référentielPaye.Setup(m => m.GetFacturesPayeesAuDelegants()).Returns(() => new List<FacturePayeeAuDelegant>()
            {
                new FacturePayeeAuDelegant(IdFacturePayeeAuDelegant.Parse(1), IdPayeDelegant.Parse(1), IdFacturation.Parse(1), abonné.IdAbonné)
            });
            référentielPaye.Setup(m => m.GetIndexesPayesParDelegants()).Returns(() => new List<IndexPayéParDelegant>()
            {
                new IndexPayéParDelegant(IdIndexPayéParDelegant.Parse(1), IdPayeDelegant.Parse(1), idCompteurAbonné, indexCompteur1.IdIndex),
                new IndexPayéParDelegant(IdIndexPayéParDelegant.Parse(2), IdPayeDelegant.Parse(2), idCompteurPompe, indexCompteurPompe1.IdIndex),
            });
            référentielAbonnés.Setup(m => m.GetAllAbonnés()).Returns(() => new List<Abonné>()
            {
                abonné
            });
            référentielFacturation.Setup(m => m.GetFacturations(It.IsAny<IdAbonné>())).Returns(() => new List<Facturation>()
            {
                new Facturation(IdFacturation.Parse(1), 0, abonné, datePaye, 
                new List<FacturationLigne>() { new FacturationLigne(IdFacturationLigne.Parse(1), IdFacturation.Parse(1), ClasseLigneFacturation.ConsommationRéelle, 
                                            Montant.FromDecimal((decimal)indexCompteur1.IndexM3 * Constantes.PrixM3.Particulier), indexCompteur1.IndexM3, 
                                            Montant.FromDecimal(Constantes.PrixM3.Particulier)) }, indexCompteur1.IdIndex, false),

                new Facturation(IdFacturation.Parse(2), 0, abonné, datePaye,
                new List<FacturationLigne>() { new FacturationLigne(IdFacturationLigne.Parse(2), IdFacturation.Parse(2), ClasseLigneFacturation.ConsommationRéelle,
                                            Montant.FromDecimal((decimal)indexCompteur2.IndexM3 * Constantes.PrixM3.Particulier), indexCompteur2.IndexM3,
                                            Montant.FromDecimal(Constantes.PrixM3.Particulier)) }, indexCompteur2.IdIndex, false),

                new Facturation(IdFacturation.Parse(3), 0, abonné, datePaye,
                new List<FacturationLigne>() { new FacturationLigne(IdFacturationLigne.Parse(3), IdFacturation.Parse(3), ClasseLigneFacturation.ConsommationRéelle,
                                            Montant.FromDecimal((decimal)indexCompteur3.IndexM3 * Constantes.PrixM3.Particulier), indexCompteur3.IndexM3,
                                            Montant.FromDecimal(Constantes.PrixM3.Particulier)) }, indexCompteur3.IdIndex, false),


            });
            référentielPompes.Setup(m => m.GetAllPompes()).Returns(() => new List<Pompe>()
            {
                new Pompe(IdPompe.Parse(1), idCompteurPompe, new ChampLibre("Pompe principale"))
            });
            référentielCompteurs.Setup(m => m.GetAllCompteurs()).Returns(() => new List<Compteur>()
            {
                new Compteur(idCompteurAbonné, new ChampLibre("Compteur de Bob Marley"), true),
                new Compteur(idCompteurPompe, new ChampLibre("Compteur de pompte principale"), true),
            });
            référentielIndexesCompteurs.Setup(m => m.GetIndexesOfCompteur(idCompteurAbonné)).Returns(() => new List<IndexCompteur>()
            {
                indexCompteur1, indexCompteur2, indexCompteur3
            });
            référentielIndexesCompteurs.Setup(m => m.GetIndexesOfCompteur(idCompteurPompe)).Returns(() => new List<IndexCompteur>()
            {
                indexCompteurPompe1, indexCompteurPompe2, indexCompteurPompe3
            });
            PayeCalculator payeCalculator = new PayeCalculator(référentielPaye.Object, référentielAbonnés.Object, référentielFacturation.Object, référentielPompes.Object, référentielIndexesCompteurs.Object, référentielCompteurs.Object);

            //Exercise
            var payeDelegant = payeCalculator.CalculePayeDelegant(datePaye, idDelegant, out List<IndexPayéParDelegant> nouveauxIndexPayés, out List<FacturePayeeAuDelegant> nouvellesFacturesPayées);

            //Verify
            var ligneFacturations = payeDelegant.LignesPaye.First(m => m.Classe == ClasseLignePayeDelegant.FacturationAbonnés);
            decimal sommeNouvellesFactus = ((decimal)indexCompteur2.IndexM3 + (decimal)indexCompteur3.IndexM3) * Constantes.PrixM3.Particulier;
            Check.That((decimal)ligneFacturations.MontantEuros).IsEqualTo(sommeNouvellesFactus);
            var ligneFuites = payeDelegant.LignesPaye.First(m => m.Classe == ClasseLignePayeDelegant.ConsommationFuites);
            decimal sommeFuitesEuros = (decimal)(indexCompteurPompe3.IndexM3 - indexCompteur3.IndexM3) * Constantes.PrixM3.Fuites;
            Check.That((decimal)ligneFuites.MontantEuros).IsEqualTo(sommeFuitesEuros);
            var ligneRemunerationSaniteau = payeDelegant.LignesPaye.First(m => m.Classe == ClasseLignePayeDelegant.RéumnérationSaniteau);
            decimal sommeRemunerationSaniteau = (sommeNouvellesFactus - sommeFuitesEuros) * Constantes.RemunerationSaniteauPourcentage / 100;
            Check.That((decimal)ligneRemunerationSaniteau.MontantEuros).IsEqualTo(sommeRemunerationSaniteau);
        }
    }
}
