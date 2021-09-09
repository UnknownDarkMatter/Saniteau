using NFluent;
using NUnit.Framework;
using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Tests
{
    [TestFixture]
    public class IdPDL_should
    {
        [Test]
        public void compare_successfully_equal_values()
        {
            //Setup
            var value1 = IdPDL.Parse(1);
            var value2 = IdPDL.Parse(1);

            //Exercise
            bool areEqual = value1 == value2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void compare_unsuccessfully_not_equal_values()
        {
            //Setup
            var value1 = IdPDL.Parse(1);
            var value2 = IdPDL.Parse(2);

            //Exercise
            bool areEqual = value1 == value2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_different_values()
        {
            //Setup
            var value1 = IdPDL.Parse(1);
            var value2 = IdPDL.Parse(2);

            //Exercise
            bool areEqual = value1 != value2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void uncompare_unsuccessfully_equal_values()
        {
            //Setup
            var value1 = IdPDL.Parse(1);
            var value2 = IdPDL.Parse(1);

            //Exercise
            bool areEqual = value1 != value2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void compare_unsuccessfully_to_null_value()
        {
            //Setup
            var value1 = IdPDL.Parse(1);

            //Exercise
            bool areEqual = value1 == null;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_to_null_value()
        {
            //Setup
            var value1 = IdPDL.Parse(1);

            //Exercise
            bool areEqual = value1 != null;

            //Verify
            Check.That(areEqual).IsTrue();
        }

    }
}
