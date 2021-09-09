using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Tests
{
    [TestFixture]
    public class Date_should
    {
        [Test]
        public void compare_successfully_equal_values()
        {
            //Setup
            var date1 = new Date(2020, 02, 02);
            var date2 = new Date(2020, 02, 02);

            //Exercise
            bool areEqual = date1 == date2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void compare_unsuccessfully_not_equal_values()
        {
            //Setup
            var date1 = new Date(2020, 02, 02);
            var date2 = new Date(2019, 02, 02);

            //Exercise
            bool areEqual = date1 == date2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_different_values()
        {
            //Setup
            var date1 = new Date(2020, 02, 02);
            var date2 = new Date(2019, 02, 02);

            //Exercise
            bool areEqual = date1 != date2;

            //Verify
            Check.That(areEqual).IsTrue();
        }

        [Test]
        public void uncompare_unsuccessfully_equal_values()
        {
            //Setup
            var date1 = new Date(2020, 02, 02);
            var date2 = new Date(2020, 02, 02);

            //Exercise
            bool areEqual = date1 != date2;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void compare_unsuccessfully_to_null_value()
        {
            //Setup
            var value1 = new Date(2020, 02, 02);

            //Exercise
            bool areEqual = value1 == null;

            //Verify
            Check.That(areEqual).IsFalse();
        }

        [Test]
        public void uncompare_successfully_to_null_value()
        {
            //Setup
            var value1 = new Date(2020, 02, 02);

            //Exercise
            bool areEqual = value1 != null;

            //Verify
            Check.That(areEqual).IsTrue();
        }

    }
}
