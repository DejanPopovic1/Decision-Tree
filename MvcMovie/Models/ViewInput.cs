using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class ViewInput
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public List<List<String>> cells { get; set; }
        public List<String> conditions { get; set; }
        public String result { get; set; }
        public bool inputConditionsSelected { get; set; }

        public ViewInput()
        {
            cells = new List<List<String>>();
            conditions = new List<String>();
            inputConditionsSelected = false;
        }

        //This ADDS to the existing list
        public void createEmptyInput(int r, int c) {
            List<String> line = new List<String>();
            while (c > 0) {
                line.Add("");
                c--;
            }
            while (r > 0)
            {
                List<String> lineCpy = new List<String>(line);
                cells.Add(lineCpy);
                r--;
            }
        }

        //This clears all cells but leaves the input rows and columns as is
        public void emptyCells()
        {
            //cells.Count();
            foreach (List<String> list in cells) {
                for (int i = 0; i < list.Count(); i++) {
                    list[i] = "";
                    i++;
                }
                list.Clear();
            }
            cells.Clear();
        }

        public void exampleDataSet()
        {
            emptyCells();
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
            cells.Add(l1);
            cells.Add(l2);
            cells.Add(l3);
            cells.Add(l4);
            cells.Add(l5);
            cells.Add(l6);
            cells.Add(l7);
            cells.Add(l8);
            cells.Add(l9);
            cells.Add(l10);
            cells.Add(l11);
            cells.Add(l12);
            cells.Add(l13);
            cells.Add(l14);
            cells.Add(l15);
            columns = 5;
            rows = 5;
        }





    }
}