using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    public class M_PersonInfo : M_Base
    {
        public M_PersonInfo()
        {
            Phones = new List<M_Phones>();
        }

        public string NameFirst { get; set; }
        public string NameSecond { get; set; }
        public string NameThird { get; set; }
        public string Photo { get; set; }
        public DateTime DateBirth { get; set; }
       
        public virtual M_Person Person { get; set; }
        public virtual ICollection<M_Phones> Phones { get; set; }
    }
}