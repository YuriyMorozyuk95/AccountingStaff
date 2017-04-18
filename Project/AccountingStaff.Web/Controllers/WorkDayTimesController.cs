using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class WorkDayTimesController : Controller
    {
        // GET: WorkDayTimes
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorkDayTimes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkDayTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkDayTimes/Create
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

        // GET: WorkDayTimes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkDayTimes/Edit/5
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

        // GET: WorkDayTimes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkDayTimes/Delete/5
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
