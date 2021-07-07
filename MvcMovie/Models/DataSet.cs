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

        public DataSet(List<List<String>> l)
        {
            List<List<String>> lCpy = new List<List<String>>(l);
            dt = new DataTable();
            List<String> headingLine = lCpy[0];
            dt = new DataTable();
            foreach (String item in headingLine)
            {
                dt.Columns.Add(new DataColumn(item, typeof(String)));
            }
            lCpy.RemoveAt(0);
            foreach (List<String> line in lCpy)
            {
                DataRow dr = dt.NewRow();
                int j = 0;
                foreach (String item in line) {
                    dr[dt.Columns[j].ColumnName] = item;
                    j++;
                }
                dt.Rows.Add(dr);
            }
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
            if (arr.Length != dt.Columns.Count) {
                throw new Exception("Size mismatch between new row arguments and number of actual table arguments");
            }
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; ++i) {
                dr[i] = arr[i];
            }
            addEntry(dr);
        }

        public DataTable filterTable(String attribute, String attributeValue)
        {
            DataTable dtCpy = dt.Copy();
            DataView dv = new DataView(dtCpy);
            String var1 = attribute;
            String var2 = attributeValue;
            String filterString = String.Format("[{0}] = '{1}'", var1, var2);//Added square brackets around {0} to handle white space in headings
            dv.RowFilter = filterString;
            dtCpy = dv.ToTable();
            return dtCpy;//At this point, dt is a new instance and not the filtered version of this classed instance of dt
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
                }
                sumOuter += (attributeValueOccurances[distinctAttributeValue]) * sumInner;
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
            if (dt.Rows.Count == 0) {
                Console.WriteLine("Empty Table");
                return;
            }
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

        public bool isDataSetSame(DataSet ds) {
            DataTable t = ds.dt;
            if (dt == null){
                return false;
            }
            if (t == null){
                return false;
            }
            if (dt.Rows.Count != t.Rows.Count){
                return false;
            }
            if (dt.Columns.Count != t.Columns.Count) {
                return false;
            }
            if (dt.Columns.Cast<DataColumn>().Any(dc => !t.Columns.Contains(dc.ColumnName))){
                return false;
            }
            for (int i = 0; i <= dt.Rows.Count - 1; i++) {
                if (dt.Columns.Cast<DataColumn>().Any(dc1 => dt.Rows[i][dc1.ColumnName].ToString() != t.Rows[i][dc1.ColumnName].ToString())) {
                    return false;
                }
            }
            return true;
        }
    }
}