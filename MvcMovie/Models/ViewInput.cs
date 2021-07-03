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
        public List<String> cells;

        public ViewInput()
        {
            cells = new List<String>();
            ListExtras.Resize(cells, 10000, "");
        }
    }
}