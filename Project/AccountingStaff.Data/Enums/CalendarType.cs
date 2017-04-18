using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Enums
{
    public enum CalendarType
    {
        [Display(Name = "Відгул")]
        TimeOff,
        [Display(Name = "Прогул")]
        Truancy,
        [Display(Name = "Вихідний день")]
        Holideys,
        [Display(Name = "Відпустка")]
        Vacation
    }
}
