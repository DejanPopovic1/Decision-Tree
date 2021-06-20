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
            ds.CreateDataRow("BAD", "HIGH", "NO", "< R15k", "HIGH");
            ds.CreateDataRow("UNKNOWN", "HIGH", "NO", "R15k - R35k", "HIGH");
            ds.CreateDataRow("UNKNOWN", "LOW", "NO", "R15k - R35k", "MEDIUM");
            ds.CreateDataRow("UNKNOWN", "LOW", "NO", "< R15k", "HIGH");
            ds.CreateDataRow("UNKNOWN", "LOW", "NO", "> R35k", "LOW");
            ds.CreateDataRow("UNKNOWN", "LOW", "YES", "> R35k", "LOW");
            ds.CreateDataRow("BAD", "LOW", "NO", "< R15k", "HIGH");
            ds.CreateDataRow("BAD", "LOW", "YES", "> R35k", "MEDIUM");
            ds.CreateDataRow("GOOD", "LOW", "NO", "> R35k", "LOW");
            ds.CreateDataRow("GOOD", "HIGH", "YES", "> R35k", "LOW");
            ds.CreateDataRow("GOOD", "HIGH", "NO", "< R15k", "HIGH");
            ds.CreateDataRow("GOOD", "HIGH", "NO", "R15k - R35k", "MEDIUM");
            ds.CreateDataRow("GOOD", "HIGH", "NO", "> R35k", "LOW");
            ds.CreateDataRow("BAD", "HIGH", "NO", "R15k - R35k", "HIGH");





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
        public void isAttributeEntropyGainCalculatedCorrectly()
        {
            Assert.That(ds.calcAttributeEntropyGain(ds.dt, 0), Is.EqualTo(0.266).Within(0.001));
            Assert.That(ds.calcAttributeEntropyGain(ds.dt, 1), Is.EqualTo(0.063).Within(0.001));
            Assert.That(ds.calcAttributeEntropyGain(ds.dt, 2), Is.EqualTo(0.207).Within(0.001));
            Assert.That(ds.calcAttributeEntropyGain(ds.dt, 3), Is.EqualTo(0.967).Within(0.001));
            Assert.Pass();
        }

        [Test]
        public void numOfDecisionsPerAttritureIsCorrect()
        {
            Assert.AreEqual(ds.numOfAttributeValueInDecision(ds.dt, 0, "BAD", "HIGH"), 3);
            Assert.AreEqual(ds.numOfAttributeValueInDecision(ds.dt, 1, "HIGH", "HIGH"), 4);
            Assert.AreEqual(ds.numOfAttributeValueInDecision(ds.dt, 2, "NO", "MEDIUM"), 2);
            Assert.Pass();
        }

        [Test]
        public void determineNode()
        {
            Assert.AreEqual(ds.determineNode(), 3);
            Assert.Pass();
        }

        [Test]
        public void isTableFilteringCorrectly1()
        {
            ds.filterTable("Income", "< R15k");
        }
        [Test]
        public void isTableFilteringCorrectly2()
        {
            ds.filterTable("Debt", "LOW");
        }

        //public delegate void funcPtr(T x);
        //Refer to drawn diagram for numbering
        [Test]
        public void testTraverse()
        {
            //Manually constructing the decision tree
            //Level 0 and Level 1 construction
            TreeNode<MvcMovie.Models.DataSet> a = new TreeNode<MvcMovie.Models.DataSet>(ds);
            MvcMovie.Models.DataSet bD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "< R15k"));
            MvcMovie.Models.DataSet cD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "R15k - R35k"));
            MvcMovie.Models.DataSet dD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "> R35k"));
            //TreeNode<MvcMovie.Models.DataSet> b = new TreeNode<MvcMovie.Models.DataSet>(bD);
            //TreeNode<MvcMovie.Models.DataSet> c = new TreeNode<MvcMovie.Models.DataSet>(cD);
            //TreeNode<MvcMovie.Models.DataSet> d = new TreeNode<MvcMovie.Models.DataSet>(dD);
            TreeNode<MvcMovie.Models.DataSet> b = a.AddChild("< R15k", bD);
            TreeNode<MvcMovie.Models.DataSet> c = a.AddChild("R15k - R35k", cD);
            TreeNode<MvcMovie.Models.DataSet> d = a.AddChild("> R35k", dD);
            //Level 2 construction
            MvcMovie.Models.DataSet eD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "BAD"));
            MvcMovie.Models.DataSet fD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "GOOD"));
            MvcMovie.Models.DataSet gD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "UNKNOWN"));
            MvcMovie.Models.DataSet hD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "BAD"));
            MvcMovie.Models.DataSet iD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "GOOD"));
            MvcMovie.Models.DataSet jD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "UNKNOWN"));
            //TreeNode<MvcMovie.Models.DataSet> e = new TreeNode<MvcMovie.Models.DataSet>(eD);
            //TreeNode<MvcMovie.Models.DataSet> f = new TreeNode<MvcMovie.Models.DataSet>(fD);
            //TreeNode<MvcMovie.Models.DataSet> g = new TreeNode<MvcMovie.Models.DataSet>(gD);
            //TreeNode<MvcMovie.Models.DataSet> h = new TreeNode<MvcMovie.Models.DataSet>(hD);
            //TreeNode<MvcMovie.Models.DataSet> i = new TreeNode<MvcMovie.Models.DataSet>(iD);
            //TreeNode<MvcMovie.Models.DataSet> j = new TreeNode<MvcMovie.Models.DataSet>(jD);
            TreeNode<MvcMovie.Models.DataSet> e = c.AddChild("BAD", eD);
            TreeNode<MvcMovie.Models.DataSet> f = c.AddChild("GOOD", fD);
            TreeNode<MvcMovie.Models.DataSet> g = c.AddChild("UNKNOWN", gD);
            TreeNode<MvcMovie.Models.DataSet> h = d.AddChild("BAD", hD);
            TreeNode<MvcMovie.Models.DataSet> i = d.AddChild("GOOD", iD);
            TreeNode<MvcMovie.Models.DataSet> j = d.AddChild("UNKNOWN", jD);
            //a.Traverse();
            //Console.WriteLine("-----");
            //c.Traverse();
            //Console.WriteLine("-----");
            //e.Traverse();
            //Console.WriteLine("-----");
            a.Traverse();

            ////Level 3 construction
            //MvcMovie.Models.DataSet kD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "HIGH"));
            //MvcMovie.Models.DataSet lD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "LOW"));
            //TreeNode<MvcMovie.Models.DataSet> k = new TreeNode<MvcMovie.Models.DataSet>(kD.Copy());
            //TreeNode<MvcMovie.Models.DataSet> l = new TreeNode<MvcMovie.Models.DataSet>(lD.Copy());
            //g.AddChild("HIGH", k.ds);
            //g.AddChild("LOW", l.ds);













            //MvcMovie.Models.DataSet bD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "< R15k"));
            //MvcMovie.Models.DataSet cD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "R15k - R35k"));
            //MvcMovie.Models.DataSet dD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "> R35k"));
            //TreeNode<MvcMovie.Models.DataSet> b = new TreeNode<MvcMovie.Models.DataSet>(bD);
            //TreeNode<MvcMovie.Models.DataSet> c = new TreeNode<MvcMovie.Models.DataSet>(cD);
            //TreeNode<MvcMovie.Models.DataSet> d = new TreeNode<MvcMovie.Models.DataSet>(dD);
            //tn.AddChild("< R15k", b.ds);
            //tn.AddChild("R15k - R35k", c.ds);
            //tn.AddChild("> R35k", d.ds);


            ////




            //TreeNode<MvcMovie.Models.DataSet> a = tn.Children["< R15k"];
            //TreeNode<MvcMovie.Models.DataSet> b = tn.Children["R15k - R35k"];
            //TreeNode<MvcMovie.Models.DataSet> c = tn.Children["> R35k"];

            //a.printNode();
            //b.printNode();
            //c.printNode();



            //  Console.WriteLine("Consructing Tree:... ");

            //DataTable a = ;
            //DataTable b = ds.filterTable("Income", "R15k - R35k");
            //DataTable c = ds.filterTable("Income", "> R35k");

            //tn.AddChild("< R15k", a);

            //int i = ds.determineNode();
            // tn.AddChild();
            // ds.filterTable();


            //tn.ds.determineNode();
            //MvcMovie.Models.DataSet ds = new MvcMovie.Models.DataSet();

            //TreeNode<MvcMovie.Models.DataSet> tn2 = tn.AddChild("Test", tn);


            //Todo: Create tree
            //Todo: Test tree traversal by printing tree
            //MvcMovie.Models.funcPtr = tn.printNode;
            //funcPtr fPtr = tn.printNode;
            //MvcMovie.Models.DataSet dummy;

        }

    }
}