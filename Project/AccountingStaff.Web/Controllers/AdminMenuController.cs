using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Controllers
{
    public class AdminMenuController : Controller
    {
        // GET: AdminMenu
        public ActionResult Index()
        {
            return View();
        }
    }
}