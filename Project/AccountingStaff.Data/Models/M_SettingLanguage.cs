using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    //Delete
    public class M_SettingLanguage : M_Base
    {
        public Lanuage SelectedLanguage { get; set; }
        
        public virtual M_Setting P_Setting { get; set; }
    }
}
