using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Releves.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Domain.Tests
{
    [TestFixture]
    public class IndexRelevéMoteur_should
    {
        [Test]
        public void incrémenter_indexes_compteurs()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("incrémenter_indexes_compteurs");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPompes = new RéférentielPompesOnEfCore(dbContextFactory);

            Horloge.Mock(new HorlogeMock(new DateTime(2019, 01, 01)));

            var compteur1 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 1"), true);
            compteur1 = référentielCompteurs.EnregistrerCompteur(compteur1);
            var compteur2 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 2"), true);
            compteur2 = référentielCompteurs.EnregistrerCompteur(compteur2);
            var compteur3 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 3"), true);
            compteur3 = référentielCompteurs.EnregistrerCompteur(compteur3);

            var compteurPompe1 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur de pompe 1"), true);
            compteurPompe1 = référentielCompteurs.EnregistrerCompteur(compteurPompe1);
            var pompe1 = new Pompe(IdPompe.Parse(0), compteurPompe1.IdCompteur, new ChampLibre("Pompe 1"));
            pompe1 = référentielPompes.EnregistrerPompe(pompe1);
            int initialIndexPompe1 = 1111;
            var indexPompe1 = new IndexCompteur(IdIndex.Parse(0), compteurPompe1.IdCompteur, 0, initialIndexPompe1, Horloge.Instance.GetDateTime());
            indexPompe1 = référentielIndexesCompteurs.EnregistreIndex(indexPompe1);


            int initialIndexCompteur1 = 101;
            var indexCompteur1 = new IndexCompteur(IdIndex.Parse(0), compteur1.IdCompteur, 0, initialIndexCompteur1, Horloge.Instance.GetDateTime());
            indexCompteur1 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur1);
            int initialIndexCompteur2 = 102;
            var indexCompteur2 = new IndexCompteur(IdIndex.Parse(0), compteur2.IdCompteur, 0, initialIndexCompteur2, Horloge.Instance.GetDateTime());
            indexCompteur2 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur2);
            int initialIndexCompteur3 = 103;
            var indexCompteur3 = new IndexCompteur(IdIndex.Parse(0), compteur3.IdCompteur, 0, initialIndexCompteur3, Horloge.Instance.GetDateTime());
            indexCompteur3 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur3);

            Horloge.Mock(new HorlogeMock(new DateTime(2019, 01, 02)));

            var indexCompteurMoteur = new IndexCompteurMoteur();

            //Exercise
            indexCompteurMoteur.ExecuteIncrémentCompteurs(1, référentielCompteurs, référentielIndexesCompteurs, référentielPompes);

            //Verify
            indexCompteur1 = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur1.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexCompteur1.IndexM3).IsStrictlyGreaterThan(initialIndexCompteur1);
            indexCompteur2 = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur2.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexCompteur2.IndexM3).IsStrictlyGreaterThan(initialIndexCompteur2);
            indexCompteur3 = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur3.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexCompteur3.IndexM3).IsStrictlyGreaterThan(initialIndexCompteur3);
            int incrément = (indexCompteur1.IndexM3 - initialIndexCompteur1) + (indexCompteur2.IndexM3 - initialIndexCompteur2) + (indexCompteur3.IndexM3 - initialIndexCompteur3);
            Check.That(indexCompteurMoteur.IncrémentRelatifIndexCompteursM3).IsEqualTo(incrément);
        }

        [Test]
        public void incrémenter_indexes_compteurs_et_pompe()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("incrémenter_indexes_compteurs_et_pompe");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var référentielPompes = new RéférentielPompesOnEfCore(dbContextFactory);

            Horloge.Mock(new HorlogeMock(new DateTime(2019, 01, 01)));

            var compteur1 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 1"), true);
            compteur1 = référentielCompteurs.EnregistrerCompteur(compteur1);
            var compteur2 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 2"), true);
            compteur2 = référentielCompteurs.EnregistrerCompteur(compteur2);
            var compteur3 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur 3"), true);
            compteur3 = référentielCompteurs.EnregistrerCompteur(compteur3);

            var compteurPompe1 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur de pompe 1"), true);
            compteurPompe1 = référentielCompteurs.EnregistrerCompteur(compteurPompe1);
            var pompe1 = new Pompe(IdPompe.Parse(0), compteurPompe1.IdCompteur, new ChampLibre("Pompe 1"));
            pompe1 = référentielPompes.EnregistrerPompe(pompe1);
            int initialIndexPompe1 = 1111;
            var indexPompe1 = new IndexCompteur(IdIndex.Parse(0), compteurPompe1.IdCompteur, 0, initialIndexPompe1, Horloge.Instance.GetDateTime());
            indexPompe1 = référentielIndexesCompteurs.EnregistreIndex(indexPompe1);

            var compteurPompe2 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur de pompe 2"), true);
            compteurPompe2 = référentielCompteurs.EnregistrerCompteur(compteurPompe2);
            var pompe2 = new Pompe(IdPompe.Parse(0), compteurPompe2.IdCompteur, new ChampLibre("Pompe 2"));
            pompe2 = référentielPompes.EnregistrerPompe(pompe2);
            int initialIndexPompe2 = 2222;
            var indexPompe2 = new IndexCompteur(IdIndex.Parse(0), compteurPompe2.IdCompteur, 0, initialIndexPompe2, Horloge.Instance.GetDateTime());
            indexPompe2 = référentielIndexesCompteurs.EnregistreIndex(indexPompe2);

            var compteurPompe3 = new Compteur(IdCompteur.Parse(0), new ChampLibre("Compteur de pompe 3"), true);
            compteurPompe3 = référentielCompteurs.EnregistrerCompteur(compteurPompe3);
            var pompe3 = new Pompe(IdPompe.Parse(0), compteurPompe3.IdCompteur, new ChampLibre("Pompe 3"));
            pompe3 = référentielPompes.EnregistrerPompe(pompe3);
            int initialIndexPompe3 = 3333;
            var indexPompe3 = new IndexCompteur(IdIndex.Parse(0), compteurPompe3.IdCompteur, 0,  initialIndexPompe3, Horloge.Instance.GetDateTime());
            indexPompe3 = référentielIndexesCompteurs.EnregistreIndex(indexPompe3);

            int initialIndexCompteur1 = 101;
            var indexCompteur1 = new IndexCompteur(IdIndex.Parse(0), compteur1.IdCompteur, 0, initialIndexCompteur1, Horloge.Instance.GetDateTime());
            indexCompteur1 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur1);
            int initialIndexCompteur2 = 102;
            var indexCompteur2 = new IndexCompteur(IdIndex.Parse(0), compteur2.IdCompteur, 0, initialIndexCompteur2, Horloge.Instance.GetDateTime());
            indexCompteur2 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur2);
            int initialIndexCompteur3 = 103;
            var indexCompteur3 = new IndexCompteur(IdIndex.Parse(0), compteur3.IdCompteur, 0, initialIndexCompteur3, Horloge.Instance.GetDateTime());
            indexCompteur3 = référentielIndexesCompteurs.EnregistreIndex(indexCompteur3);

            Horloge.Mock(new HorlogeMock(new DateTime(2019, 01, 02)));
            IndexRelevéMoteur indexRelevéMoteur = new IndexRelevéMoteur();

            //Exercise
            indexRelevéMoteur.ExecuteRelevé(référentielCompteurs, référentielIndexesCompteurs, référentielPompes);

            //Verify
            indexPompe1 = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe1.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexPompe1.IndexM3).IsStrictlyGreaterThan(initialIndexPompe1);
            indexPompe2 = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe2.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexPompe2.IndexM3).IsStrictlyGreaterThan(initialIndexPompe2);
            indexPompe3 = référentielIndexesCompteurs.GetIndexesOfCompteur(pompe3.IdCompteur).OrderByDescending(c => c.DateIndex).First();
            Check.That(indexPompe3.IndexM3).IsStrictlyGreaterThan(initialIndexPompe3);
            int incrément = (indexPompe1.IndexM3 - initialIndexPompe1) + (indexPompe2.IndexM3 - initialIndexPompe2) + (indexPompe3.IndexM3 - initialIndexPompe3);
            Check.That(indexRelevéMoteur.IncrémentRelatifIndexCompteursM3).IsEqualTo(incrément);
        }

    }
}
