using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    public class M_Role : M_Base
    {
        public M_Role()
        {
            Persons = new List<M_Person>();
        }
        //Role must be,ContactPerson, Employee ,Admin,Director
        public string Role { get; set; }

        public virtual ICollection<M_Person> Persons { get; set; }
    }
}
