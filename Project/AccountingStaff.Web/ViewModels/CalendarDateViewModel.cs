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
    //change to abstract class
    public class CalendarDateViewModel : ViewModel
    {
        private M_Person _employer;

        public CalendarDateViewModel()
        { }
        public CalendarDateViewModel(M_CalendarDate model) : base(model)
        {  }

        public override void SetModel(M_Base modelBase)
        {
            M_CalendarDate modelEntity = modelBase as M_CalendarDate;
            _model = modelEntity;
            Id = modelEntity.Id;
            Start = modelEntity.Start;
            End = modelEntity.End;
            NameEmployer = GetName(modelEntity.Employee?.PersonInfo);
            TypeDate = modelEntity.TypeDate;
            _employer = modelEntity?.Employee;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Початок")]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Кінець")]
        public DateTime End { get; set; }

        [Display(Name = "Ім'я працівника")]
        public string NameEmployer { get; set; }

        [Display(Name = "Тип дати")]
        public CalendarType TypeDate { get; set; }

        public override M_Base GetModel()
        {
            if (_model != null)
            {
                return _model;
            }
            else
            {
                return new M_CalendarDate
                {
                    Id = this.Id,
                    TypeDate = this.TypeDate,
                    Start = this.Start,
                    End = this.End,
                    Employee = FindEmployerWithName(NameEmployer)
                };
            }
        }

        

    }
}