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

        public ActionResult DecisionTree(String r, String c)
        {
            ViewBag.Message = "How to manage a decision tree";
            ViewInput vi = new ViewInput();

            if (!String.IsNullOrEmpty(r) && !String.IsNullOrEmpty(c))
            {
                vi.columns = Int32.Parse(c);
                vi.rows = Int32.Parse(r);
                vi.createEmptyInput(Int16.Parse(r), Int16.Parse(c));
            }
            var test =  vi.cells.Count().ToString();
            return View(vi);
        }

        [HttpPost]
        public ActionResult Sizing(ViewInput vi)
        {
            //vi.createEmptyInput(vi.rows, vi.columns);
            var rs = vi.rows.ToString();
            var cs = vi.columns.ToString();
            return RedirectToAction("DecisionTree", new { r = rs, c = cs});//These variable names must match the names of parameters in DecisionTree
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