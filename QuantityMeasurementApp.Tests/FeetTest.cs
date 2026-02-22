using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class FeetTest
    {
        [TestMethod]
        public void GivenSameValue_WhenCompared_ShouldReturnTrue()
        {
            Feet f1 = new Feet(1.0);
            Feet f2 = new Feet(1.0);

            bool result = f1.Equals(f2);

            Assert.IsTrue(result, "Expected values to be equal");
        }

        [TestMethod]
        public void GivenDifferentValues_WhenCompared_ShouldReturnFalse()
        {
            Feet f1 = new Feet(1.0);
            Feet f2 = new Feet(2.0);

            bool result = f1.Equals(f2);

            Assert.IsFalse(result, "Expected values to be different");
        }

        [TestMethod]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            Feet f1 = new Feet(1.0);

            bool result = f1.Equals(null);

            Assert.IsFalse(result, "Expected comparison with null to be false");
        }

        [TestMethod]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            Feet f1 = new Feet(1.0);

            bool result = f1.Equals(f1);

            Assert.IsTrue(result, "Expected same reference to be equal");
        }

        [TestMethod]
        public void GivenDifferentType_WhenCompared_ShouldReturnFalse()
        {
            Feet f1 = new Feet(1.0);

            bool result = f1.Equals("not a number");

            Assert.IsFalse(result, "Expected different types to not be equal");
        }
    }
}