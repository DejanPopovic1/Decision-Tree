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

        public ActionResult DecisionTreeEntry() {
            return View("~/Views/Home/DecisionTree.cshtml", new ViewInput());
        }
        //Important to note: Using Razor pages, submitting a form may change a model and you wont be able to see it in the controller
        [HttpPost]
        public ActionResult GenerateDecisionTree(ViewInput vi, String command)
        {
            switch (command) {
                case "submit1":
                    //vi.emptyCells();
                    //ViewBag.Message = "How to manage a decision tree";
                    //vi.createEmptyInput(vi.rows, vi.columns);
                    return RedirectToAction("EmptyDataSet", new {r = vi.rows, c = vi.columns});
                    break;
                case "submit2":
                    //vi.exampleDataSet();
                    return RedirectToAction("ExampleDataSet");
                    break;
                case "submit4":
                    Dictionary<String, String> conditionsList = new Dictionary<String, String>();
                    for (int j = 0; j < vi.cells[0].Count - 1; j++) {
                        conditionsList.Add(vi.cells[0][j], vi.conditions[j]);
                    }
                    DataSet ds = new DataSet(vi.cells);
                    DecisionTreeNode dtn = new DecisionTreeNode(ds);
                    dtn.recursivelyConstructDecisionTreeLevels(dtn);
                    vi.result = dtn.determineResult(dtn, conditionsList);
                    goto case "submit3";
                case "submit3":
                    vi.inputConditionsSelected = true;
                    int i = vi.cells[0].Count();
                    while (i > 1)
                    {
                        vi.conditions.Add("");
                        i--;
                    }
                    break;
                default:
                    break;
            }
            return View("~/Views/Home/DecisionTree.cshtml", vi);
            //return View("~/Views/Home/DecisionTree.cshtml", vi);
        }

        //It seems very controller has a binding that cannot be changed. So now we make a new binding. See case 2 in the primary controller
        public ActionResult ExampleDataSet()
        {
            ViewInput newVI = new ViewInput();
            newVI.exampleDataSet();
            return View("~/Views/Home/DecisionTree.cshtml", newVI);
        }

        //It seems very controller has a binding that cannot be changed. So now we make a new binding. See case 2 in the primary controller
        public ActionResult EmptyDataSet(int r, int c)
        {
            ViewInput newVI = new ViewInput();
            newVI.createEmptyInput(r, c);
            return View("~/Views/Home/DecisionTree.cshtml", newVI);
        }

        //======================================
        //The following controllers are not used
        //======================================

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
        public ActionResult ProvideInputConditions(ViewInput vi)
        {
            vi.inputConditionsSelected = true;
            var rs = vi.rows.ToString();
            var cs = vi.columns.ToString();
            return RedirectToAction("DecisionTree", new { r = rs, c = cs });//These variable names must match the names of parameters in DecisionTree
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