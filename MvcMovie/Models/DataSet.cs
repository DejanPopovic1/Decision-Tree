﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class DataSet
    {
        public DataTable dt = new DataTable();

        //By convention, the last column in the table is the decision
        public void addAttribute(String attribute) {
            dt.Columns.Add(attribute, typeof(String));// We can also create a row object and Add it as that object in which case Add is overloaded with that signature
        }

        public void addEntry(DataRow dr) {
            dt.Rows.Add(dr);
        }

        public void CreateDataRow(params string[] arr)
        {
            //Console.WriteLine("String size is: ");
            //Console.WriteLine(arr.Length);
            //Console.WriteLine("Number of columns are: ");
            //Console.WriteLine(dt.Columns.Count);
            if (arr.Length != dt.Columns.Count) {
                throw new Exception("Size mismatch between new row arguments and number of actual table arguments");
            }
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; ++i) {
                dr[i] = arr[i];
            }
            addEntry(dr);

            //return dr;
        }

        public void generateDecisionTree()
        { 

        }





        private Dictionary<string, int> columnOccurances(int columnIndex)
        {
            string indexName = dt.Columns[columnIndex].ColumnName;
            //DataView view = new DataView(dt);
            //DataTable distinctColumnDT = view.ToTable(true, dt.Columns[columnIndex].ColumnName);
            //List<String> listOfDistinctItems = new List<String>();
            //foreach (DataRow r in distinctColumnDT.Rows)
            //{
            //    listOfDistinctItems.Add(r[indexName].ToString());
            //}
            List<string> listOfDistinctItems = distinctValues(dt, columnIndex);
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

        List<String> distinctValues(DataTable dt, int attributeIndex) 
        {
            string indexName = dt.Columns[attributeIndex].ColumnName;
            DataView view = new DataView(dt);
            DataTable distinctColumnDT = view.ToTable(true, dt.Columns[attributeIndex].ColumnName);
            List<String> listOfDistinctItems = new List<String>();
            foreach (DataRow r in distinctColumnDT.Rows)
            {
                listOfDistinctItems.Add(r[indexName].ToString());
            }
            return listOfDistinctItems;
        }

        private int numOfAttributeValueInDecision(DataTable dt, int attributeIndex, String distinctAttributeValue, String decision)
        {
            int result = 0;
            string keyIndexName = dt.Columns[attributeIndex].ColumnName;
            string valueIndexName = dt.Columns[dt.Columns.Count - 1].ColumnName;
            foreach (DataRow r in dt.Rows) 
            {
                if (r[keyIndexName].ToString() == distinctAttributeValue && r[valueIndexName].ToString() == decision)
                {
                    result++;
                }
            }
            return result;
        }

        public double calcAttributeEntropyGain(DataTable dt, int attributeindex)
        {
            int entryCount = dt.Rows.Count;
            List<String> distinctAttributeValues = distinctValues(dt, attributeindex);
            List<String> distinctDecisions = distinctValues(dt, dt.Columns.Count - 1);
            Dictionary<string, int> attributeValueOccurances = columnOccurances(attributeindex);
            Dictionary<int, double> attributeValueEntropies = new Dictionary<int, double>();
            int i = 0;
            double sumInner = 0;
            double sumOuter = 0;
            foreach (var distinctAttributeValue in distinctAttributeValues)
            {
                foreach (var distinctDecision in distinctDecisions)
                {
                    int c = numOfAttributeValueInDecision(dt, i, distinctAttributeValue, distinctDecision);
                    sumInner += calcEntropyTerm(c, attributeValueOccurances[distinctAttributeValue]);
                }
                sumOuter += (attributeValueOccurances[distinctAttributeValue]) * sumInner;
                attributeValueEntropies.Add(i, sumInner);
                sumInner = 0;
                i++;
            }
            sumOuter /= entryCount;
            return calcEntropy() + sumOuter;
        }

        public double calcEntropy()
        {
            int entryCount = dt.Rows.Count;

            Dictionary<string, int> decisionCounts = columnOccurances(dt.Columns.Count - 1);
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