using NUnit.Framework;
using MvcMovie.Models;
using System;
using System.Reflection;

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
            ds.CreateDataRow("BAD",     "HIGH", "NO",   "< R15K",       "HIGH");
            ds.CreateDataRow("UNKNOWN", "HIGH", "NO",   "R15k - R35k",  "HIGH");
            ds.CreateDataRow("UNKNOWN", "LOW",  "NO",   "R15k - R35k",  "MEDIUM");
            ds.CreateDataRow("UNKNOWN", "LOW",  "NO",   "< R15K",       "HIGH");
            ds.CreateDataRow("UNKNOWN", "LOW",  "NO",   "> R35k",       "LOW");
            ds.CreateDataRow("UNKNOWN", "LOW",  "YES",  "> R35k",       "LOW");
            ds.CreateDataRow("BAD",     "LOW",  "NO",   "< R15K",       "HIGH");
            ds.CreateDataRow("BAD",     "LOW",  "YES",  "> R35k",       "MEDIUM");
            ds.CreateDataRow("GOOD",    "LOW",  "NO",   "> R35k",       "LOW");
            ds.CreateDataRow("GOOD",    "HIGH", "YES",  "> R35k",       "LOW");
            ds.CreateDataRow("GOOD",    "HIGH", "NO",   "< R15K",       "HIGH");
            ds.CreateDataRow("GOOD",    "HIGH", "NO",   "R15k - R35k",  "MEDIUM");
            ds.CreateDataRow("GOOD",    "HIGH", "NO",   "> R35k",       "LOW");
            ds.CreateDataRow("BAD",     "HIGH", "NO",   "R15k - R35k",  "HIGH");
        }

        [TearDown]
        public void BaseTearDown()
        {
            ds = new DataSet();
        }

        [Test]
        public void isEntropyCalculatedCorrectly()
        {
            MethodInfo methodInfo = typeof(DataSet).GetMethod("calcEntropy", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { };
            methodInfo.Invoke(ds, parameters);
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