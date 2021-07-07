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
            dtn.node = dtn.ds.dt.Columns[index].ColumnName;
            dtn.branches = dtn.ds.distinctValues(dtn.ds.dt, index);
            //End
            //ds = temp.ds;
            if (dtn.ds.isSingleDecision()){
                //System.Environment.Exit(-1);
                return;
            }
            int i = 0;
            foreach (var b in dtn.branches) {
                DataSet newDataSet = new DataSet(dtn.ds.filterTable(dtn.node, b));
                DecisionTreeNode newChildNode = new DecisionTreeNode(newDataSet);
                //AddDecisionChild(branch, node, dtn.ds);
                dtn.decisionChildren.Add(newChildNode);
                dtn.recursivelyConstructDecisionTreeLevels(dtn.decisionChildren[i]);
                i++;
            }
        }

        //This function assumes that a tree is already built
        public String determineResult(DecisionTreeNode dtn, Dictionary<String, String> conditions) {

            if (dtn.decisionChildren.Count() == 0)
            {
                //return dtn.node;
                return dtn.ds.dt
            }

            //Find child index
            String subRootNode = dtn.node;
            String requiredBranchToTake = conditions[subRootNode];
            int i = 0;
            foreach (String branch in dtn.branches) {
                if (branch == requiredBranchToTake){
                    break;
                }
                i++;
            }






            return determineResult(dtn.decisionChildren[i], conditions);
        }
    }
}