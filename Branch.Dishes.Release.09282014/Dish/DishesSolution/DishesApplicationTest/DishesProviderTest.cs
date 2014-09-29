using DishesApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesApplicationTest
{
    [TestClass]
    public class DishesProviderTest
    {
        [TestMethod]
        public void DishesProviderInstanceTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void NightDishTypeTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            Assert.IsInstanceOfType(provider.DishTypeProvider, typeof(NightDish));
        }

        [TestMethod]
        public void MorningDishTypeTest()
        {
            DishProvider provider = new DishProvider("morning", new DishTypeFactory());
            Assert.IsInstanceOfType(provider.DishTypeProvider, typeof(MorningDish));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidNightDishTypeTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "5" });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void MultipleSteakDishTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "1","1" });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void MultipleEggDishTest()
        {
            DishProvider provider = new DishProvider("morning", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "1", "1" });
        }

        [TestMethod]
        public void ValidNightDishTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "1" });
            Assert.AreEqual("steak", provider.Report());
        }

        [TestMethod]
        public void ValidMorningDishTest()
        {
            DishProvider provider = new DishProvider("Morning", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "1" });
            Assert.AreEqual("eggs", provider.Report());
        }

        [TestMethod]
        public void ValidMultiplePotatoDishTest()
        {
            DishProvider provider = new DishProvider("night", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "2" ,"2"});
        }

        [TestMethod]
        public void ValidMulipleCoffeeDishTest()
        {
            DishProvider provider = new DishProvider("Morning", new DishTypeFactory());
            provider.GetAvailableDish(new List<string> { "3","3" });
        }
    }
}
