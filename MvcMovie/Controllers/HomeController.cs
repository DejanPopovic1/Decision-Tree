using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DecisionTree()
        {
            ViewBag.Message = "How to manage a decision tree";
            ViewInput vi = new ViewInput();
            List<String> l1 = new List<String>();
            List<String> l2 = new List<String>();
            l1.Add("Item1");
            l1.Add("Item2");
            l1.Add("Item3");
            l2.Add("Item4");
            l2.Add("Item5");
            l2.Add("Item6");
            //vi.cells.Add(l1);
            //vi.cells.Add(l2);
            return View(vi);
        }

        [HttpPost]
        public String Sizing(ViewInput vi)
        {
            vi.createEmptyInput(vi.rows, vi.columns);
            String result = vi.cells[0].Count().ToString();
            return result;
        }

        [HttpPost]
        public String DecisionTree(ViewInput vi)
        {
            //System.Environment.Exit(vi.cells.Count);

            //if (vi.rows == 3)
            //{
            //   System.Environment.Exit(vi.rows);
            //}
            //ViewBag.Message = "How to manage a decision tree";

            return "Hello there";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to contact the author and contribute to the project";

            return View();
        }

        //Use this to download a CSV version of the input table. Also do the opposite where a CSV table may be uploaded
        //public FileResult Download()
        //{
        //    return File(Url.Content("~/TEST.txt"), "text/plain", "testFile.txt");
        //}

    }
}