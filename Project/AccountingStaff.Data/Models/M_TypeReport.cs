using System.Collections;
using System.Collections.Generic;

namespace AccountingStaff.Data.Models
{
    public class M_ReportType : M_Base
    {
        public M_ReportType()
        {
            Reports = new List<M_Report>();
        }
        public string TypeName { get; set; }

        public ICollection<M_Report> Reports { get; set; }
    }
}