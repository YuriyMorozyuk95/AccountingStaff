using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class PersonInfoController : Controller
    {
        // GET: PersonInfo
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonInfo/Create
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

        // GET: PersonInfo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonInfo/Edit/5
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

        // GET: PersonInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonInfo/Delete/5
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
