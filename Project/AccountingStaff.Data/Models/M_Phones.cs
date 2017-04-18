using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    public class M_Phones : M_Base
    {
        public string Number { get; set; }

        public virtual M_PersonInfo Person { get; set; }

    }
}