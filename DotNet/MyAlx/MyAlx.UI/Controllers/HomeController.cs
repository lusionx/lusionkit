using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyAlx.Core;
using MyAlx.Core.Entity;
using MyAlx.Core.Models;
using MyAlx.Tools;

namespace MyAlx.UI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            var db = new alxEntities();

            var model = new List<WorkLog>();
            WorkLog obj = null;
            foreach (var a in db.worklogs)
            {
                obj=new WorkLog();
                obj.InitFromEntityObject(a);
                model.Add(obj);
            }

            ViewData[ConstString.EntityKeyOfViewData] = model;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
