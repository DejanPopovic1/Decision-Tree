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
    public class TreeNode<T>
    {
        public T ds;
        //private Dictionary<String, TreeNode<T>> branchNode = new Dictionary<String, TreeNode<T>>();
        private Dictionary<String, TreeNode<T>> children = new Dictionary<String, TreeNode<T>>();

        public TreeNode(T value)
        {
            ds = value;//Defining ds as DataSet type would result in compile time error on this line, but setting ds to T works
        }

        //Using a C# facility called "indexers": (I like to think of it as operator overloading
        public KeyValuePair<String, TreeNode<T>> this[int i]
        {
            get { return children.ElementAt(i);}
        }

        //Parent is a "Property" which is kind of like a half method and half variable. More specifically, it is written in an "auto" format
        public TreeNode<T> Parent { get; private set; }

        //A non-auto property
        public T Value { get { return ds; } }

        public ImmutableDictionary <String, TreeNode<T>> Children
        {
            get { return children.ToImmutableDictionary(); }
        }

        //We are returning a reference to a child
        public TreeNode<T> AddChild(String branch, T value)
        {
            TreeNode<T> node = new TreeNode<T>(value) { Parent = this };
            //branchNode.Add(branch, node);
            children.Add(branch, node);
            return node;
        }

        public bool RemoveChild(String key)
        {
            return children.Remove(key);
        }

        public void Traverse(/*Action action*/)//Action<T>
        {
            //printNode();
            //action();
            foreach (var child in children)
            {
                child.Value.Traverse(/*action*/);
               
            }
            Console.WriteLine("Testing 1 2 3");
        }

        public void printNode()
        {
            //System.Data.DataTable test = T as System.Data.DataTable;
            //System.Data.DataTable ds = t as System.Data.DataTable;

            DataSet dx = ds as DataSet;

            foreach (DataRow dataRow in dx.dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.Write(item + "testing ");
                }
               // Console.WriteLine();
            }
            Console.WriteLine("=x0x0x0x0x===x0x0x0x0x0====x0x0x0x0====");
        }


        public void makeDecisionTree() { 
        
        
        
        }

    }
}