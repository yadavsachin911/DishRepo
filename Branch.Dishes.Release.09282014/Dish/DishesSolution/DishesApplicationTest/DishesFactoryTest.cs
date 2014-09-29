using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DishesApplication;

namespace DishesApplicationTest
{
    [TestClass]
    public class DishesFactoryTest
    {
        [TestMethod]
        public void TestNightDishInstance()
        {
            IDishTypeFactory factory = new DishTypeFactory();
            DishType dishType = factory.SetDishTypeFor(null, "night");
            Assert.IsNotNull(dishType);
        }

        [TestMethod]
        public void TestMorningDishInstance()
        {
            IDishTypeFactory factory = new DishTypeFactory();
            DishType dishType = factory.SetDishTypeFor(null, "Morning");
            Assert.IsNotNull(dishType);
        }

        [TestMethod]
        public void TestInvalidDishInstance()
        {
            IDishTypeFactory factory = new DishTypeFactory();
            DishType dishType = factory.SetDishTypeFor(null, "abc");
            Assert.IsNull(dishType);
        }
    }
}
 