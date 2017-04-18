using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class WorkScheduleController : Controller
    {
        // GET: WorkSchedule
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorkSchedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkSchedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkSchedule/Create
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

        // GET: WorkSchedule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkSchedule/Edit/5
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

        // GET: WorkSchedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkSchedule/Delete/5
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
