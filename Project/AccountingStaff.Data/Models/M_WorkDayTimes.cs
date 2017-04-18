using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_WorkDayTimes : M_Base
    {
        public DateTime? SingleDate { get; set; }
        //Must Realize in ViewModel
        //public string Day
        //{
        //    get
        //    {
        //        return SingleDate?.DayOfWeek.ToString();
        //    }

        //}
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        // //Must Realize in ViewModel
        //public TimeSpan? LengthOfInside
        //{
        //    get
        //    {
        //        if (TimeIn != null && TimeOut != null)
        //        {
        //            return TimeOut.Value - TimeIn.Value;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public int? EmployeeId { get; set; }

        //[ForeignKey("EmployeeId")]
        public virtual M_Person Employee { get; set; }
    }
}
