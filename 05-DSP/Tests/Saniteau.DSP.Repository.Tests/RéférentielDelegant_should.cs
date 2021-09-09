using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;
using Saniteau.Common;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Saniteau.DSP.Repository.Tests
{
    [TestFixture]
    public class RéférentielDelegant_should
    {
        [Test]
        public void create_delegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("create_delegant");
            var référentielDelegant = new RéférentielDelegantOnEfCore(dbContextFactory);
            Delegant delegant = new Delegant(IdDelegant.Parse(0), new ChampLibre("Ville de Paris"), new ChampLibre("Hotel de ville, 75001, PARIS"), new Date(2019, 05, 20));

            //Exercise
            delegant = référentielDelegant.EnregistreDelegant(delegant);

            //Vérify
            var savedDelegant = référentielDelegant.GetDelegants()[0];
            Check.That((int)savedDelegant.IdDelegant).IsStrictlyGreaterThan(0);
            Check.That(savedDelegant.Nom.ToString()).IsEqualTo("Ville de Paris");
            Check.That(savedDelegant.Adresse.ToString()).IsEqualTo("Hotel de ville, 75001, PARIS");
            Check.That(savedDelegant.DateContrat.ToDateTime(0, 0, 0)).IsEqualTo(new DateTime(2019, 05, 20));
        }

        [Test]
        public void update_delegant()
        {
            //Setup
            IDbContextFactory dbContextFactory = new DbContextFactoryMock("update_delegant");
            var référentielDelegant = new RéférentielDelegantOnEfCore(dbContextFactory);
            Delegant delegant = new Delegant(IdDelegant.Parse(0), new ChampLibre("Ville de Paris"), new ChampLibre("Hotel de ville, 75001, PARIS"), new Date(2019, 05, 20));
            delegant = référentielDelegant.EnregistreDelegant(delegant);
            delegant.ChangeNom(new ChampLibre("Ville de Paris 2"));
            delegant.ChangeAdresse(new ChampLibre("Hotel de ville 2, 75001, PARIS"));
            delegant.ChangeDateContrat(new Date(2019, 05, 21));

            //Exercise
            delegant = référentielDelegant.EnregistreDelegant(delegant);

            //Vérify
            var savedDelegant = référentielDelegant.GetDelegants()[0];
            Check.That(savedDelegant.Nom.ToString()).IsEqualTo("Ville de Paris 2");
            Check.That(savedDelegant.Adresse.ToString()).IsEqualTo("Hotel de ville 2, 75001, PARIS");
            Check.That(savedDelegant.DateContrat.ToDateTime(0, 0, 0)).IsEqualTo(new DateTime(2019, 05, 21));
        }

    }
}
