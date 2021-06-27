using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using MvcMovie.Models;
using System.Collections.Immutable;
using System.Data;

namespace MvcMovie.Models
{
    public class DecisionTreeNode
    {
        public DataSet ds;
        public String node;
        public List<String> branches;
        public List<DecisionTreeNode> decisionChildren;// = new List<DecisionTreeNode>();

        public DecisionTreeNode(DataSet _ds) {
            decisionChildren = new List<DecisionTreeNode>();
            branches = new List<String>();
            ds = _ds;
        }

        public void AddDecisionChild(List<String> _branches, String _node, DataSet _ds) {
            DecisionTreeNode n = new DecisionTreeNode(_ds);
            n.branches = _branches;
            n.node = _node;
            decisionChildren.Add(n);
        }

        public void Traverse(/*Action action*/)//Action<T>
        {
            //printNode();
            //action();
            foreach (var child in decisionChildren)
            {
                child.Traverse(/*action*/);
            }
            Console.WriteLine("Testing 1 2 3");
        }

        //Use Traverse() and delegates to simplify this
        public void recursivelyConstructDecisionTreeLevels(DecisionTreeNode dtn)
        {
            dtn.ds.printDataSet();
            //DecisionTreeNode temp = new DecisionTreeNode(dtn.ds);
            //Start: Determine node and branches
            int index = dtn.ds.determineNode();
            node = dtn.ds.dt.Columns[index].ColumnName;
            branches = dtn.ds.distinctValues(dtn.ds.dt, index);
            //End
            //ds = temp.ds;
            if (dtn.ds.isSingleDecision()){
                //System.Environment.Exit(-1);
                return;
            }
            int i = 0;
            foreach (var b in branches) {
                DataSet newDataSet = new DataSet(dtn.ds.filterTable(node, b));
                DecisionTreeNode newChildNode = new DecisionTreeNode(newDataSet);
                //AddDecisionChild(branch, node, dtn.ds);
                decisionChildren.Add(newChildNode);
                recursivelyConstructDecisionTreeLevels(decisionChildren[i]);
                i++;
            }
        }

    }
}