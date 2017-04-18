using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_Rfids : M_Base
    {
        public string RfidCode { get; set; }
        public bool IsArhive { get; set; }

        public virtual M_Person Employee { get; set; }
    }
}
