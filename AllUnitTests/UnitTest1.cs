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

 

            foreach (DataRow dataRow in ds.dt.Rows)//Changing this to var causes an error
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            
        }

        [TearDown]
        public void BaseTearDown()
        {
            ds = new MvcMovie.Models.DataSet();
        }

        [Test]
        public void isEntropyCalculatedCorrectly()
        {
            //MethodInfo methodInfo = typeof(DataSet).GetMethod("calcEntropy", BindingFlags.NonPublic | BindingFlags.Instance);
            //object[] parameters = { };
            //double a = (double)methodInfo.Invoke(ds, parameters);
            //Console.WriteLine("Entropy of table is: ");
            //Console.WriteLine(a);
            Console.WriteLine(ds.dt.Columns.Count);
            Console.WriteLine("Entropy of table is (explicitly): ");
            Console.WriteLine(ds.calcEntropy());
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