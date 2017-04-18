using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    //Delete
    public class M_SettingExcel : M_Base
    {
        public string Path { get; set; }

        public virtual M_Setting Setting { get; set; }
    }
}