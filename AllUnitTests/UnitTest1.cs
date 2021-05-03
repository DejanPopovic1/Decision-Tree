using NUnit.Framework;
using MvcMovie.Models;
using System;
using System.Reflection;
using System.Data;

namespace AllUnitTests
{
    [TestFixture]
    public class Tests
    {
        MvcMovie.Models.DataSet ds = new MvcMovie.Models.DataSet();//This is where namespaces work
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

 

            //foreach (DataRow dataRow in ds.dt.Rows)//Changing this to var causes an error
            //{
            //    foreach (var item in dataRow.ItemArray)
            //    {
            //        Console.Write(item + " ");
            //    }
            //    Console.WriteLine();
            //}
            
        }

        [TearDown]
        public void BaseTearDown()
        {
            ds = new MvcMovie.Models.DataSet();
        }

        [Test]
        public void isEntropyCalculatedCorrectly()
        {
            Assert.That(ds.calcEntropy(), Is.EqualTo(1.531).Within(0.001));
            Assert.Pass();
        }

        [Test]
        public void calcEntropyTerm()
        {

            Assert.That(ds.calcEntropyTerm(3, 4), Is.EqualTo(0.31).Within(0.01));
            Assert.Pass();
        }

        [Test]
        public void isAttribute1EntropyGainCalculatedCorrectly()
        {

            Assert.That(ds.calcAttributeEntropyGain(ds.dt, 0), Is.EqualTo(0.266).Within(0.001));
            Assert.Pass();
        }

    }
}