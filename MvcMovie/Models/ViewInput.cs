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
        public List<List<String>> cells;

        public ViewInput()
        {
            cells = new List<List<String>>();
            //ListExtras.Resize(cells, 10000, "");
        }

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
    }
}