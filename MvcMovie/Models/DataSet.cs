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

        public void addAttribute(String attribute) {
            dt.Columns.Add(attribute, typeof(String));// We can also create a row object and Add it as that object in which case Add is overloaded with that signature
        }

        public void addEntry(DataRow dr) {
            dt.Rows.Add(dr);
        }

        public DataRow CreateDataRow(string[] arr)
        {
            if (arr.Length != dt.Columns.Count) {
                throw new Exception("Size mismatch between new row arguments and number of actual table arguments");
            }
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; ++i) {
                dr[i] = arr[i];
            }
            return dr;
        }


        public void generateDecisionTree() { 

        }
        public void calcEntropy() { }




        void addColumn(string attributeName) { }



    }
}