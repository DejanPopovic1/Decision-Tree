using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static System.Collections.IEnumerable;

namespace MvcMovie.Models
{
    public class DataSet
    {
        public DataTable dt;

        public DataSet() 
        {
            dt = new DataTable();
        }

        public DataSet(DataTable dtArg)
        {
            dt = dtArg;
        }

        public DataSet Copy() 
        {
            DataSet ans = new DataSet();
            ans.dt = dt.Copy();
            return ans;
        }

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

        public DataTable filterTable(String attribute, String attributeValue)
        {
            DataTable dtCpy = dt.Copy();
            DataView dv = new DataView(dtCpy);
            String var1 = attribute;
            String var2 = attributeValue;
            //dv.RowFilter = "{var1} = '{var2}'";
            String filterString = String.Format("{0} = '{1}'", var1, var2);
            //dv.RowFilter = "Income = '< R15k'";
            dv.RowFilter = filterString;
            dtCpy = dv.ToTable();
            //determineNode();

            //foreach (DataRow dataRow in dt.Rows)
            //{
            //    foreach (var item in dataRow.ItemArray)
            //    {
            //        Console.Write(item + " ");
            //    }
            //    Console.WriteLine();
            //}
            



            return dtCpy;//At this point, dt is a new instance and not the filtered version of this classed instance of dt
        }

        public void createDecicionTreeNode()
        {
            int n = determineNode();




        }

        private Dictionary<string, int> columnOccurances(int columnIndex)
        {
            string indexName = dt.Columns[columnIndex].ColumnName;
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

        //Rather use the dt value in this as opposed to a parameter
        public List<String> distinctValues(DataTable dt, int attributeIndex) 
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

        public int numOfAttributeValueInDecision(DataTable dt, int attributeIndex, String distinctAttributeValue, String decision)
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
            double sumInner = 0;
            double sumOuter = 0;
            foreach (var distinctAttributeValue in distinctAttributeValues)
            {
                foreach (var distinctDecision in distinctDecisions)
                {
                    int c = numOfAttributeValueInDecision(dt, attributeindex, distinctAttributeValue, distinctDecision);
                    sumInner += calcEntropyTerm(c, attributeValueOccurances[distinctAttributeValue]);
                   // Console.WriteLine("Sum inner: " + sumInner + "Input values: " + c + " " + attributeValueOccurances[distinctAttributeValue]);
                }
                sumOuter += (attributeValueOccurances[distinctAttributeValue]) * sumInner;
               // Console.WriteLine(sumOuter);
                sumInner = 0;
            }
            sumOuter /= entryCount;
            return calcEntropy() - sumOuter;
        }

        public double calcEntropy()
        {
            int entryCount = dt.Rows.Count;

            Dictionary<string, int> decisionCounts = columnOccurances(dt.Columns.Count - 1);
            double sum = 0;
            foreach (KeyValuePair<String, int> entry in decisionCounts) 
            {
                int v = entry.Value;
                sum += calcEntropyTerm(v, entryCount);
            }
            return sum;
        }

        public int determineNode()
        {
            double maxEntropyGain = 0;
            int maxEntropyGainIndex = 0;
            foreach (DataColumn dc in dt.Columns) 
            {
                if (dt.Columns.IndexOf(dc) == dt.Columns.Count - 1) {
                    break;
                }
                double attEntropyGain = calcAttributeEntropyGain(dt, dt.Columns.IndexOf(dc));
                //Console.WriteLine("Attribute entropy gain: " + attEntropyGain);//Put this back in to find entropy gain
                if (attEntropyGain > maxEntropyGain) 
                {
                    maxEntropyGain = attEntropyGain;
                    maxEntropyGainIndex = dt.Columns.IndexOf(dc);
                }
            }
            return maxEntropyGainIndex;
        }

        public double calcEntropyTerm(int specificDecisionCount, int totalEntryCount) 
        {
            if (specificDecisionCount == 0) {
                return 0;
            }
            double r = (double)specificDecisionCount / totalEntryCount;
            return -r * Math.Log(r, (double)2);
        }

        public void printDataSet() 
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (var item in dr.ItemArray)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool isSingleDecision() 
        {
            if (distinctValues(dt, dt.Columns.Count - 1).Count == 1) {
                return true; 
            }
            return false;
        }
    }
}