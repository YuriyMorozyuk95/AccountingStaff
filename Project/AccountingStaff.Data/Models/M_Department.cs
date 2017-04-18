using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_Department : M_Base
    {
        public M_Department()
        {
            Employee = new List<M_Person>();
        }

        public string DepartmentName { get; set; }

        public int? Director_Id { get; set; }

        [ForeignKey("Director_Id")]
        public virtual M_Person Director { get; set; }
        public virtual ICollection<M_Person> Employee { get; set; }
    }
}
