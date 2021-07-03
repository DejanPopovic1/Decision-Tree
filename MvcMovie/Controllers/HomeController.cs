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
                vi.createEmptyInput(Int16.Parse(r), Int16.Parse(c));
            }
            var test =  vi.cells.Count().ToString();
            return View(vi);
        }

        [HttpPost]
        public ActionResult Sizing(ViewInput vi)
        {
            vi.createEmptyInput(vi.rows, vi.columns);
            return RedirectToAction("DecisionTree", new { rows = vi.rows.ToString(), columns = vi.columns.ToString()});//  ; return RedirectToAction("Index", new { id = idString });
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