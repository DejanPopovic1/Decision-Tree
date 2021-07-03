﻿using System;
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

        [HttpPost]
        public ActionResult ExampleDataSet() {
            ViewInput vi = new ViewInput();
            List<String> l1 = new List<String> {"Credit history", "Debt", "Collateral", "Income", "Risk"};
            List<String> l2 = new List<String> {"BAD", "HIGH", "NO", "< R15k", "HIGH"};
            List<String> l3 = new List<String> {"UNKNOWN", "HIGH", "NO", "R15k - R35k", "HIGH"};
            List<String> l4 = new List<String> {"UNKNOWN", "LOW", "NO", "R15k - R35k", "MEDIUM"};
            List<String> l5 = new List<String> {"UNKNOWN", "LOW", "NO", "< R15k", "HIGH"};
            List<String> l6 = new List<String> {"UNKNOWN", "LOW", "NO", "> R35k", "LOW"};
            List<String> l7 = new List<String> {"UNKNOWN", "LOW", "YES", "> R35k", "LOW"};
            List<String> l8 = new List<String> {"BAD", "LOW", "NO", "< R15k", "HIGH"};
            List<String> l9 = new List<String> {"BAD", "LOW", "YES", "> R35k", "MEDIUM"};
            List<String> l10 = new List<String> {"GOOD", "LOW", "NO", "> R35k", "LOW"};
            List<String> l11 = new List<String> {"GOOD", "HIGH", "YES", "> R35k", "LOW"};
            List<String> l12 = new List<String> {"GOOD", "HIGH", "NO", "< R15k", "HIGH"};
            List<String> l13 = new List<String> {"GOOD", "HIGH", "NO", "R15k - R35k", "MEDIUM"};
            List<String> l14 = new List<String> {"GOOD", "HIGH", "NO", "> R35k", "LOW"};
            List<String> l15 = new List<String> { "BAD", "HIGH", "NO", "R15k - R35k", "HIGH"};
            vi.cells.Add(l1);
            vi.cells.Add(l2);
            vi.cells.Add(l3);
            vi.cells.Add(l4);
            vi.cells.Add(l5);
            vi.cells.Add(l6);
            vi.cells.Add(l7);
            vi.cells.Add(l8);
            vi.cells.Add(l9);
            vi.cells.Add(l10);
            vi.cells.Add(l11);
            vi.cells.Add(l12);
            vi.cells.Add(l13);
            vi.cells.Add(l14);
            vi.cells.Add(l15);
            return View("~/Views/Home/DecisionTree.cshtml", vi);
        }

        [HttpPost]
        public String GenerateDecisionTree( )
        {
            return "Heya";
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