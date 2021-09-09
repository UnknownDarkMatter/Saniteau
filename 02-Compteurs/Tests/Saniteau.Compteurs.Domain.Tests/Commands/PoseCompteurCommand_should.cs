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
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Tests.Commands
{
    [TestFixture]
    public class PoseCompteurCommand_should
    {
        [Test]
        public void poser_compteur_not_posed_successfully()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("poser_compteur");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var créerCompteurCommand = new EnregistreCompteurDomainCommand(IdCompteur.Parse(0), new ChampLibre("AAA"));
            var newCompteur = créerCompteurCommand.EnregistreCompteur(référentielCompteurs);
            bool initialState = newCompteur.CompteurPosé;
            var poserCompteurCommand = new PoseCompteurDomainCommand(newCompteur);
            int indexM3Before = 1000;
            var indexCompteur = new IndexCompteur(IdIndex.Parse(0), newCompteur.IdCompteur, indexM3Before, 0, Horloge.Instance.GetDateTime().AddDays(-1));
            référentielIndexesCompteurs.EnregistreIndex(indexCompteur);

            //Exercise
            poserCompteurCommand.PoseCompteur(référentielCompteurs, référentielIndexesCompteurs);

            //Verify
            var compteur = référentielCompteurs.GetCompteur(newCompteur.IdCompteur);
            Check.That(initialState).IsFalse();
            Check.That(newCompteur.CompteurPosé).IsTrue();
            var indexesSorted = référentielIndexesCompteurs.GetIndexesOfCompteur(compteur.IdCompteur).OrderBy(i => i.DateIndex).ToList();
            Check.That(indexesSorted[0].IndexM3).Equals(indexM3Before);
            Check.That(indexesSorted[1].IndexM3).Equals(0);
            Check.That((int)indexesSorted[0].IdIndex > 0).IsTrue();
            Check.That((int)indexesSorted[1].IdIndex > 0).IsTrue();
        }

        [Test]
        public void raise_exception_when_pose_compteur_already_posé()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("poser_compteur");
            var référentielCompteurs = new RéférentielCompteursOnEfCore(dbContextFactory);
            var référentielIndexesCompteurs = new RéférentielIndexesCompteursOnEfCore(dbContextFactory);
            var créerCompteurCommand = new EnregistreCompteurDomainCommand(IdCompteur.Parse(0), new ChampLibre("AAA"));
            var newCompteur = créerCompteurCommand.EnregistreCompteur(référentielCompteurs);
            bool initialState = newCompteur.CompteurPosé;
            var poserCompteurCommand = new PoseCompteurDomainCommand(newCompteur);
            poserCompteurCommand.PoseCompteur(référentielCompteurs, référentielIndexesCompteurs);

            //Exercise && Verify
            Check.ThatCode(() => { poserCompteurCommand.PoseCompteur(référentielCompteurs, référentielIndexesCompteurs); }).Throws<BusinessException>();
        }
    }
}
