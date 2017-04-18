using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingStaff.Data.Models;
using AccountingStaff.Data.Repository;
using AccountingStaff.Web.ViewModels;
using AccountingStaff.Web.Util;

namespace AccountingStaff.Web.Controllers
{
    public class CalendarDateController : Controller
    {
        private IGenericRepository<M_CalendarDate> _repositoriyCalendar;
        private IEnumerable<M_Person> _employers;
        private IUnitOfWork _unit;

        public CalendarDateController(IUnitOfWork unit)
        {
            _unit = unit;
            _repositoriyCalendar = unit.RepositoryCalendarDate;
            _employers = unit.RepositoryPerson.Get();
        }

        // GET: CalendarDate
        public ActionResult Index()
        {
            IEnumerable<M_CalendarDate> calendarDateModel = _repositoriyCalendar.Get();
            ViewModelFactory<CalendarDateViewModel> factory =
                new ViewModelFactory<CalendarDateViewModel>();

            IEnumerable<CalendarDateViewModel> calendarDatesVM = 
                factory.GetListViewModels(calendarDateModel);


            return View(calendarDatesVM);
        }

        // GET: CalendarDate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_CalendarDate m_CalendarDate = _repositoriyCalendar.FindById(id.Value);
            

            if (m_CalendarDate == null)
            {
                return HttpNotFound();
            }

            CalendarDateViewModel calendarDetails = new CalendarDateViewModel(m_CalendarDate);
            return View(calendarDetails);
        }

        // GET: CalendarDate/Create
        public ActionResult Create()
        {
            ViewBag.EmployerNames = ViewModelFactory.GetSelectedListEmployersName(_employers);
            return View();
        }

        

        // POST: CalendarDate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create(CalendarDateViewModel createdDate)
        {
            if (ModelState.IsValid)
            {
                M_CalendarDate m_CalendarDate = createdDate.GetModel() as M_CalendarDate;
                _repositoriyCalendar.Create(m_CalendarDate);
                return RedirectToAction("Index");
            }

            return View(createdDate);
        }

        // GET: CalendarDate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_CalendarDate editableDate = _repositoriyCalendar.FindById(id.Value);

            if (editableDate == null)
            {
                return HttpNotFound();
            }

            CalendarDateViewModel editableDateVM = new CalendarDateViewModel(editableDate);
            var list = ViewModelFactory.GetSelectedListEmployersName(_employers);

            if (list.Count() != 0)
            {
                list.Where(li => li.Value == editableDateVM.NameEmployer)
                .FirstOrDefault()
                .Selected = true;
            }

            ViewBag.EmployerNames = list;
            return View(editableDateVM);
        }

        // POST: CalendarDate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CalendarDateViewModel editableDateVM)
        {
            M_CalendarDate editableDate = editableDateVM.GetModel() as M_CalendarDate;

            if (ModelState.IsValid)
            {
                _repositoriyCalendar.Update(editableDate);
                return RedirectToAction("Index");
            }

            return View(editableDateVM);
        }

        // GET: CalendarDate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_CalendarDate deletedDate = _repositoriyCalendar.FindById(id.Value);

            if (deletedDate == null)
            {
                return HttpNotFound();
            }

            CalendarDateViewModel deletedDateVM = new CalendarDateViewModel(deletedDate);

            return View(deletedDateVM);
        }

        // POST: CalendarDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            M_CalendarDate deletedDate = _repositoriyCalendar.FindById(id);
            _repositoriyCalendar.Remove(deletedDate);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
