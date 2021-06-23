﻿using System;
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
        public List<String> branch;
        public String node;
        public List<DecisionTreeNode> decisionChildren = new List<DecisionTreeNode>();

        public DecisionTreeNode(DataSet _ds) {
            ds = _ds;
        }

        public void AddDecisionChild(List<String> _branch, String _node, DataSet _ds) {
            DecisionTreeNode n = new DecisionTreeNode(_ds);
            n.branch = _branch;
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
        public String recursivelyConstructDecisionTreeLevels()
        {
            int index = ds.determineNode();
            node = ds.dt.Columns[index].ColumnName;
            branch = ds.distinctValues(ds.dt, index);
            List<DataSet> dsl = new List<DataSet>();
            foreach (var d in branch) {
                decisionChildren.Add(new DecisionTreeNode(new DataSet(ds.filterTable(node, d))));
                //dsl.Add(ds.filterTable(node, d));
            }
           

            

            //foreach (var d in branch) {
            //    Console.WriteLine(d);
            //}


            return node;
            //BranchNodePair bnp;
            //bnp.branch = "";
            //bnp.node = cn;



            //AddDecisionChild()
            //Console.WriteLine("=x0x0x0x0x===x0x0x0x0x0====x0x0x0x0====");
        }

    }
}