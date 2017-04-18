using AccountingStaff.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    //Delete
    public class M_Setting : M_Base
    {
        public ThemeApp ThemeApp { get; set; }
        public ThemeAccent ThemeAccent { get;set;}

        [Required]
        public virtual M_SettingEmail SettingEmail { get; set; }
        [Required]
        public virtual M_SettingExcel SettingExcel { get; set; }
        [Required]
        public virtual M_SettingLanguage SettingLanguage { get; set; }
        [Required]
        public virtual M_SettingLimitTimer SettingLimitTimer { get; set; }
    }
}