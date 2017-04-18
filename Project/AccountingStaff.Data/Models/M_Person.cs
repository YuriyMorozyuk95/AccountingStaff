using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    //TO DO
    public class M_Person : M_Base
    {
        public M_Person()
        {
            WorkDayTimes = new List<M_WorkDayTimes>();
            CalendarDates = new List<M_CalendarDate>();
            ContactPersons = new List<M_PersonInfo>();
        }
        [Required]
        public virtual M_PersonInfo PersonInfo { get; set; }
        [Required]
        public virtual M_Rfids Rfid { get; set; }
        [Required]
        public virtual M_Department Department { get; set; }

        public virtual ICollection<M_PersonInfo> ContactPersons { get; set; }
        public virtual ICollection<M_Role> Roles { get; set; }
        public virtual ICollection<M_WorkDayTimes> WorkDayTimes { get; set; }
        public virtual ICollection<M_CalendarDate> CalendarDates { get; set; }

        

    }
}
