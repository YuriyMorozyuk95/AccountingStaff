using AccountingStaff.Data.Models;
using AccountingStaff.Data.Repository;
using AccountingStaff.Web.ViewModels;
using AccountingStaff.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private IGenericRepository<M_Department> _repositoriyDepartment;
        private IEnumerable<M_Person> _employers;
        private IUnitOfWork _unit;

        public DepartmentController(IUnitOfWork unit)
        {
            _unit = unit;
            _repositoriyDepartment = unit.RepositoryDepartment;
            _employers = unit.RepositoryPerson.Get();
        }
        // GET: Department
        public ActionResult Index()
        {
            IEnumerable<M_Department> departmentsModel = _repositoriyDepartment.Get();
            ViewModelFactory<DepartmentViewModel> factory = 
                new ViewModelFactory<DepartmentViewModel>();

            IEnumerable<DepartmentViewModel> departmentsVM = factory.GetListViewModels(departmentsModel);

            return View(departmentsVM);
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_Department departmentModel = _repositoriyDepartment.FindById(id.Value);

            if(departmentModel == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel departmentVM = new DepartmentViewModel(departmentModel);
            return View(departmentVM);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.EmployerNames = ViewModelFactory.GetSelectedListEmployersName(_employers);
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel createdDepartment)
        {
            if(ModelState.IsValid)
            {
                M_Department departmentModel = createdDepartment.GetModel() as M_Department;
                _repositoriyDepartment.Create(departmentModel);
                return RedirectToAction("Index");
            }

            return View(createdDepartment);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_Department editableDepartment = _repositoriyDepartment.FindById(id.Value);

            if(editableDepartment == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel departmentVM = new DepartmentViewModel(editableDepartment);
            var listDir = ViewModelFactory.GetSelectedListEmployersName(_employers);

            if(listDir.Count() != 0)
            {
                listDir.Where(li => li.Value == departmentVM.NameDirector)
                .FirstOrDefault()
                .Selected = true;
            }

            ViewBag.DirectorName = listDir;
            return View(departmentVM);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel departmentVM)
        {
            M_Department departmentModel = departmentVM.GetModel() as M_Department;

            if(ModelState.IsValid)
            {
                _repositoriyDepartment.Update(departmentModel);
                return RedirectToAction("Index");
            }

            return View(departmentVM);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            M_Department departmentModel = _repositoriyDepartment.FindById(id.Value);

            if (departmentModel == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel deletedDepartmentVM = new DepartmentViewModel(departmentModel);

            return View(deletedDepartmentVM);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            M_Department deletedDepartment = _repositoriyDepartment.FindById(id);
            _repositoriyDepartment.Remove(deletedDepartment);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
