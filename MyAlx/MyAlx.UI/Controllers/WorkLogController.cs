using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAlx.UI.Controllers
{
    public class WorkLogController : Controller
    {
        //
        // GET: /WorkLog/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WorkLog/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WorkLog/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /WorkLog/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /WorkLog/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WorkLog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WorkLog/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WorkLog/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
