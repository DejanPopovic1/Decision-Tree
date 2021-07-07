using NUnit.Framework;
using MvcMovie.Models;
using System;
using System.Reflection;
using System.Data;
using System.Linq;
using System.Collections.Generic;

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
            Console.Write("Constructed node b is: "); a.decisionChildren[0].ds.printDataSet();
            Console.Write("Manual node b is: "); bD.printDataSet();
            Assert.IsTrue(a.decisionChildren[0].ds.isDataSetSame(bD));
            Assert.IsTrue(a.decisionChildren[1].ds.isDataSetSame(cD));
            Assert.IsTrue(a.decisionChildren[2].ds.isDataSetSame(dD));
            Assert.IsTrue(a.decisionChildren[1].decisionChildren[0].ds.isDataSetSame(gD));
            Assert.IsTrue(a.decisionChildren[1].decisionChildren[1].ds.isDataSetSame(fD));
            Assert.IsTrue(a.decisionChildren[1].decisionChildren[2].ds.isDataSetSame(eD));
            Assert.IsTrue(a.decisionChildren[2].decisionChildren[0].ds.isDataSetSame(jD));
            Assert.IsTrue(a.decisionChildren[2].decisionChildren[1].ds.isDataSetSame(hD));
            Assert.IsTrue(a.decisionChildren[2].decisionChildren[2].ds.isDataSetSame(iD));
            Assert.IsTrue(a.decisionChildren[1].decisionChildren[0].decisionChildren[0].ds.isDataSetSame(kD));
            Assert.IsTrue(a.decisionChildren[1].decisionChildren[0].decisionChildren[1].ds.isDataSetSame(lD));
        }

        [Test]
        public void viewInput()
        {
            ViewInput vi = new ViewInput();
            vi.createEmptyInput(5, 5);
            Assert.AreEqual(vi.cells.Count(), 5);
            Assert.AreEqual(vi.cells[0].Count(), 5);
            Assert.AreEqual(vi.cells[1].Count(), 5);
            Assert.AreEqual(vi.cells[2].Count(), 5);
            Assert.AreEqual(vi.cells[3].Count(), 5);
            Assert.AreEqual(vi.cells[4].Count(), 5);

            Assert.AreEqual(vi.cells[0][0], "");
            Assert.AreEqual(vi.cells[0][2], "");
            Assert.AreEqual(vi.cells[1][3], "");
            Assert.AreEqual(vi.cells[0][0], "");
            Assert.AreEqual(vi.cells[0][0], "");
            Assert.AreEqual(vi.cells[4][1], "");
            Assert.AreEqual(vi.cells[4][0], "");
        }

        [Test]
        public void constructDataSetFromListOfLists()
        {
            //Given
            List<List<String>> listOfLists = new List<List<String>>();
            List<String> l1 = new List<String> { "Credit history", "Debt", "Collateral", "Income", "Risk" };
            List<String> l2 = new List<String> { "BAD", "HIGH", "NO", "< R15k", "HIGH" };
            List<String> l3 = new List<String> { "UNKNOWN", "HIGH", "NO", "R15k - R35k", "HIGH" };
            List<String> l4 = new List<String> { "UNKNOWN", "LOW", "NO", "R15k - R35k", "MEDIUM" };
            List<String> l5 = new List<String> { "UNKNOWN", "LOW", "NO", "< R15k", "HIGH" };
            List<String> l6 = new List<String> { "UNKNOWN", "LOW", "NO", "> R35k", "LOW" };
            List<String> l7 = new List<String> { "UNKNOWN", "LOW", "YES", "> R35k", "LOW" };
            List<String> l8 = new List<String> { "BAD", "LOW", "NO", "< R15k", "HIGH" };
            List<String> l9 = new List<String> { "BAD", "LOW", "YES", "> R35k", "MEDIUM" };
            List<String> l10 = new List<String> { "GOOD", "LOW", "NO", "> R35k", "LOW" };
            List<String> l11 = new List<String> { "GOOD", "HIGH", "YES", "> R35k", "LOW" };
            List<String> l12 = new List<String> { "GOOD", "HIGH", "NO", "< R15k", "HIGH" };
            List<String> l13 = new List<String> { "GOOD", "HIGH", "NO", "R15k - R35k", "MEDIUM" };
            List<String> l14 = new List<String> { "GOOD", "HIGH", "NO", "> R35k", "LOW" };
            List<String> l15 = new List<String> { "BAD", "HIGH", "NO", "R15k - R35k", "HIGH" };
            listOfLists.Add(l1);
            listOfLists.Add(l2);
            listOfLists.Add(l3);
            listOfLists.Add(l4);
            listOfLists.Add(l5);
            listOfLists.Add(l6);
            listOfLists.Add(l7);
            listOfLists.Add(l8);
            listOfLists.Add(l9);
            listOfLists.Add(l10);
            listOfLists.Add(l11);
            listOfLists.Add(l12);
            listOfLists.Add(l13);
            listOfLists.Add(l14);
            listOfLists.Add(l15);
            //When
            MvcMovie.Models.DataSet testDS = new MvcMovie.Models.DataSet(listOfLists);
            //Then
            Assert.AreEqual(l1[0], testDS.dt.Columns[0].ColumnName);
            Assert.AreEqual(l1[4], testDS.dt.Columns[4].ColumnName);
            Assert.AreEqual(listOfLists[1][0], testDS.dt.Rows[0][0]);
            Assert.AreEqual(listOfLists[1][3], testDS.dt.Rows[0][3]);
            Assert.AreEqual(listOfLists[3][0], testDS.dt.Rows[2][0]);
            Assert.AreEqual(listOfLists[5][1], testDS.dt.Rows[4][1]);
        }

        [Test]
        public void determineResult()
        {
            //Given
            DecisionTreeNode a = new DecisionTreeNode(ds);
            a.recursivelyConstructDecisionTreeLevels(a);
            Dictionary<String, String> conditions = new Dictionary<String, String>
            {
                {"CreditHistory", "UNKNOWN"},
                {"Debt", "HIGH"},
                {"Collateral", "NO"},
                {"Income", "R15k - R35k"}
            };
            //When
            String result = a.determineResult(a, conditions);
            //Then
            Assert.AreEqual(result, "HIGH");
            //ADD ANOTHER CASE WHERE COLLATOERAL IS YES AND ANOTHER 2 CASES
        }
    }
}