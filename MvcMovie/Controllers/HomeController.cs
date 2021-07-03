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
            ViewInput vi = new ViewInput() { rows= 3, columns = 3};
            return View(vi);
        }

        [HttpPost]
        public ActionResult DecisionTree(ViewInput vi)
        {
            if (vi.rows == 3)
            {
                System.Environment.Exit(vi.rows);
            }
            //ViewBag.Message = "How to manage a decision tree";

            return View();
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