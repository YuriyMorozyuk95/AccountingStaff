using AccountingStaff.Data.Models;
using AccountingStaff.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Util
{
    public class ViewModelFactory
    {
        public IEnumerable<string> GetNamesEmployers(IEnumerable<M_Person> employers)
        {
            return employers.Select(emp => ViewModel.GetName(emp.PersonInfo)).ToList();
        }

        public static SelectList GetSelectedListEmployersName(IEnumerable<M_Person> employers)
        {
            ViewModelFactory factory = new ViewModelFactory();
            SelectList employerNames = new SelectList(factory.GetNamesEmployers(employers),
               employers.FirstOrDefault());
            return employerNames;
        }
    }
}