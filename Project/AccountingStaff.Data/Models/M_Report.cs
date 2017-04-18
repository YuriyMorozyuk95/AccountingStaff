using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_Report : M_Base
    {
        public string ReportName { get; set; }

        public string Path { get; set; }

        [Required]
        public M_ReportType TypeReport {get;set;}
    }
}
