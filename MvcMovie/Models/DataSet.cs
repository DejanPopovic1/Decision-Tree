using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class DataSet
    {
        DataTable dt = new DataTable();

        //By convention, the last column in the table is the decision
        public void addAttribute(String attribute) {
            dt.Columns.Add(attribute, typeof(String));// We can also create a row object and Add it as that object in which case Add is overloaded with that signature
        }

        public void addEntry(DataRow dr) {
            dt.Rows.Add(dr);
        }

        public DataRow CreateDataRow(params string[] arr)
        {
            Console.WriteLine("String size is: ");
            Console.WriteLine(arr.Length);
            Console.WriteLine("Number of columns are: ");
            Console.WriteLine(dt.Columns.Count);
            if (arr.Length != dt.Columns.Count) {
                throw new Exception("Size mismatch between new row arguments and number of actual table arguments");
            }
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; ++i) {
                dr[i] = arr[i];
            }
            return dr;
        }

        public void generateDecisionTree()
        { 

        }



        private Dictionary<string, int> countColumnOccurances(int columnIndex)
        {
            string indexName = dt.Columns[columnIndex].ColumnName;
            DataView view = new DataView(dt);
            DataTable distinctColumnDT = view.ToTable(true, dt.Columns[columnIndex].ColumnName);
            List<String> listOfDistinctItems = new List<String>();
            foreach (DataRow r in distinctColumnDT.Rows)
            {
                listOfDistinctItems.Add(r[indexName].ToString());
            }

            var itemsAndTheirCount = new Dictionary<String, int>();
            int count = 0;
            foreach (var l in listOfDistinctItems)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (l == r[indexName].ToString())
                    {
                        count++;
                    }
                }
                itemsAndTheirCount.Add(l, count);
                count = 0;
            }
            return itemsAndTheirCount;
        }








        //private Dictionary<string, int> decisionCount()
        //{
        //    //Make a unique list of Items for the column of interest
        //    var decisionIndex = dt.Columns.Count - 1;
        //    string decisionIndexName = dt.Columns[decisionIndex].ColumnName;
        //    DataView view = new DataView(dt);
        //    DataTable distinctDecisionsDT = view.ToTable(true, dt.Columns[decisionIndex].ColumnName);
        //    List<String> listOfDecisions = new List<String>();
        //    foreach (DataRow r in distinctDecisionsDT.Rows)
        //    {
        //        listOfDecisions.Add(r[decisionIndexName].ToString());
        //    }
        //    //Associate each of these items with a count
        //    var decisionsAndTheirCount = new Dictionary<String, int>();
        //    int count = 0;
        //    foreach (var l in listOfDecisions)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            if (l == r[decisionIndexName].ToString())
        //            {
        //                count++;
        //            }
        //        }
        //        decisionsAndTheirCount.Add(l, count);
        //        count = 0;
        //    }
        //    return decisionsAndTheirCount;
        //}
        
        private double calcEntropy()
        {
            int entryCount = dt.Rows.Count;
            Dictionary<string, int> decisionCounts = countColumnOccurances(dt.Columns.Count - 1);
            double sum = 0;
            foreach (KeyValuePair<String, int> entry in decisionCounts) 
            {
                int v = entry.Value;
                sum += calcEntropyTerm(entryCount, v);
            }
            return sum;
        }

        private double calcEntropyTerm(int totalEntryCount, int specificDecisionCount) 
        {
            double r = (double)specificDecisionCount / totalEntryCount;
            return -r * Math.Log(r, (double)2);
        }





    }
}