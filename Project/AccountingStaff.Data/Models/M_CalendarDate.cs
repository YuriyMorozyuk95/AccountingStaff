using AccountingStaff.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_CalendarDate : M_Base
    {

        public CalendarType TypeDate { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual M_Person Employee { get; set; }
    }
}
