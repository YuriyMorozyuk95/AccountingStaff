using AccountingStaff.Data.Models;
using AccountingStaff.Data.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _unit;
        public HomeController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public ActionResult Index()
        {
            _unit.RepositorReport.Create(
               new M_Report
                {
                    ReportName = "rep",
                    TypeReport = new M_ReportType
                    {
                        TypeName = "baseReport"
                    },
                });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}