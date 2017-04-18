using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    //Delete
    public class M_SettingLimitTimer : M_Base
    {
        public TimeSpan MaxTimeInside { get; set; }
        public DateTime IntervalCheck { get; set; }

        //public int? SettingId { get; set; }

        //[ForeignKey("SettingId")]
        public virtual M_Setting Setting { get; set; }
    }
}
