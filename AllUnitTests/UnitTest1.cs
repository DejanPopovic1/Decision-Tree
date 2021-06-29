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

        ////public delegate void funcPtr(T x);
        ////Refer to drawn diagram for numbering
        //[Test]
        //public void testTraverse()
        //{
        //    //Level 0 and Level 1 construction
        //    TreeNode<MvcMovie.Models.DataSet> a = new TreeNode<MvcMovie.Models.DataSet>(ds);
        //    MvcMovie.Models.DataSet bD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "< R15k"));
        //    MvcMovie.Models.DataSet cD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "R15k - R35k"));
        //    MvcMovie.Models.DataSet dD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "> R35k"));
        //    TreeNode<MvcMovie.Models.DataSet> b = a.AddChild("< R15k", bD);
        //    TreeNode<MvcMovie.Models.DataSet> c = a.AddChild("R15k - R35k", cD);
        //    TreeNode<MvcMovie.Models.DataSet> d = a.AddChild("> R35k", dD);
        //    //Level 2 construction
        //    MvcMovie.Models.DataSet eD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "BAD"));
        //    MvcMovie.Models.DataSet fD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "GOOD"));
        //    MvcMovie.Models.DataSet gD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "UNKNOWN"));
        //    MvcMovie.Models.DataSet hD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "BAD"));
        //    MvcMovie.Models.DataSet iD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "GOOD"));
        //    MvcMovie.Models.DataSet jD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "UNKNOWN"));
        //    TreeNode<MvcMovie.Models.DataSet> e = c.AddChild("BAD", eD);
        //    TreeNode<MvcMovie.Models.DataSet> f = c.AddChild("GOOD", fD);
        //    TreeNode<MvcMovie.Models.DataSet> g = c.AddChild("UNKNOWN", gD);
        //    TreeNode<MvcMovie.Models.DataSet> h = d.AddChild("BAD", hD);
        //    TreeNode<MvcMovie.Models.DataSet> i = d.AddChild("GOOD", iD);
        //    TreeNode<MvcMovie.Models.DataSet> j = d.AddChild("UNKNOWN", jD);
        //    //Level 3 construction
        //    MvcMovie.Models.DataSet kD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "HIGH"));
        //    MvcMovie.Models.DataSet lD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "LOW"));
        //    TreeNode<MvcMovie.Models.DataSet> k = g.AddChild("HIGH", kD);
        //    TreeNode<MvcMovie.Models.DataSet> l = g.AddChild("LOW", lD);
        //    //Assertions
        //    Assert.AreEqual(a.children.Count, 3);
        //    Assert.AreEqual(b.children.Count, 0);
        //    Assert.AreEqual(c.children.Count, 3);
        //    Assert.AreEqual(d.children.Count, 3);
        //    Assert.AreEqual(g.children.Count, 2);
        //}

        [Test]
        public void testIsSingleDecision()
        {
            DecisionTreeNode a = new DecisionTreeNode(ds);
            MvcMovie.Models.DataSet d = new MvcMovie.Models.DataSet(a.ds.filterTable("Income", "< R15k"));
            Console.WriteLine("Single Decision Test: " + d.distinctValues(d.dt, d.dt.Columns.Count - 1).Count);
            Assert.IsTrue(d.isSingleDecision());
        }

        [Test]
        public void constructDecisionTree()
        {
            DecisionTreeNode a = new DecisionTreeNode(ds);
            MvcMovie.Models.DataSet bD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "< R15k"));
            MvcMovie.Models.DataSet cD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "R15k - R35k"));
            MvcMovie.Models.DataSet dD = new MvcMovie.Models.DataSet(ds.filterTable("Income", "> R35k"));
            MvcMovie.Models.DataSet eD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "BAD"));
            MvcMovie.Models.DataSet fD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "GOOD"));
            MvcMovie.Models.DataSet gD = new MvcMovie.Models.DataSet(cD.filterTable("CreditHistory", "UNKNOWN"));
            MvcMovie.Models.DataSet hD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "BAD"));
            MvcMovie.Models.DataSet iD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "GOOD"));
            MvcMovie.Models.DataSet jD = new MvcMovie.Models.DataSet(dD.filterTable("CreditHistory", "UNKNOWN"));
            MvcMovie.Models.DataSet kD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "HIGH"));
            MvcMovie.Models.DataSet lD = new MvcMovie.Models.DataSet(gD.Copy().filterTable("Debt", "LOW"));
            a.recursivelyConstructDecisionTreeLevels(a);
            //DecisionTreeNode constructedFNode = a.decisionChildren[1].decisionChildren[1];
            //Console.Write("Child 1, 1 is: "); Console.Write("Child Node is: " + a.decisionChildren[1].decisionChildren[1].node + " ");  a.decisionChildren[1].decisionChildren[1].ds.printDataSet();
            Console.Write("Constructed node b is: "); a.decisionChildren[0].ds.printDataSet();
            //MvcMovie.Models.DataSet ds_b = new MvcMovie.Models.DataSet(bD);
            Console.Write("Manual node b is: "); bD.printDataSet();

            Assert.AreEqual(a.decisionChildren[0].ds.dt, bD.dt);
            //Assert.AreEqual(bD.isDataSetSame(decisionChildren[0].ds);
            //Assert.AreEqual(constructedFNode.ds.dt, c.dt);
            //Assert.AreEqual(constructedFNode.ds.dt, d.dt);
            //Assert.AreEqual(constructedFNode.ds.dt, fD.dt);
        }
    }
}