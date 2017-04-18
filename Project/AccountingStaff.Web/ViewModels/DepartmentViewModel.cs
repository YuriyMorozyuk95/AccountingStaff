using AccountingStaff.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AccountingStaff.Enums;
using AccountingStaff.Web.Util;

namespace AccountingStaff.Web.ViewModels
{
    public class DepartmentViewModel : ViewModel
    {
        [Display(Name = "Назва відділення")]
        public string DepartmentName { get; set; }

        [Display(Name = "Керівник відділення")]
        public M_Person Director { get; set; }

        [Display(Name = "Працівники")]
        public ICollection<M_Person> Employers { get; set; }
        public string NameDirector { get; internal set; }

        public DepartmentViewModel()
        {  }

        public DepartmentViewModel(M_Department model) : base(model)
        {  }

        public override void SetModel(M_Base modelBase)
        {
            M_Department modelEntity = modelBase as M_Department;
            _model = modelEntity;
            Id = modelEntity.Id;
            DepartmentName = modelEntity.DepartmentName;
            Director = modelEntity.Director;
            Employers = modelEntity.Employee;
        }

        public override M_Base GetModel()
        {
            if(_model != null)
            {
                return _model;
            }
            else
            {
                return new M_Department
                {
                    Id = this.Id,
                    DepartmentName = this.DepartmentName,
                    Director = this.Director,
                    Employee = this.Employers
                };
            }
        }

    }
}