using NUnit.Framework;
using MvcMovie.Models;
using System;

namespace AllUnitTests
{
    [TestFixture]
    public class Tests
    {
        DataSet ds = new DataSet();
        [SetUp]
        public void BaseSetup()
        {
            ds.addAttribute("CreditHistory");
            ds.addAttribute("Debt");
            ds.addAttribute("Collateral");
            ds.addAttribute("Income");
            ds.addAttribute("Risk");
            ds.CreateDataRow("BAD", "HIGH", "NO", "< R15K", "HIGH");

        }

        [TearDown]
        public void BaseTearDown()
        {
            ds = new DataSet();
        }

            [Test]
        public void Test1()
        {
            //String[] ent1 = { "a", "a", "a", "a", "a" };

            //ds.CreateDataRow(ent1);
            //Assert.AreEqual(ds.Row);
            Assert.AreEqual(1, 1);
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(3, 3);
            Assert.Pass();
        }

    }
}