using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Tests
{
    [TestFixture]
    public class Montant_should
    {
        [Test]
        public void compare_successfully_equal_values()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.5M);
            var montant2 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 == montant2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void compare_unsuccessfully_not_equal_values()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.1M);
            var montant2 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 == montant2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_different_values()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.1M);
            var montant2 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 != montant2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void uncompare_unsuccessfully_equal_values()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.5M);
            var montant2 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 != montant2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void compare_unsuccessfully_to_null_value()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 == null;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_to_null_value()
        {
            //Setup
            var montant1 = Montant.FromDecimal(10.5M);

            //Exercise
            bool areEqual = montant1 != null;

            //Verify
            Check.That(areEqual).IsTrue();
        }

    }
}
