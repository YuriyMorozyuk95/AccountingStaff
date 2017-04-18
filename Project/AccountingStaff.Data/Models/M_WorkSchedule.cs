using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingStaff.Data.Models
{
    public class M_WorkSchedule : M_Base
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; } 
        public TimeSpan Valid { get; set; }
        public TimeSpan? TimeDinner { get; set; }

        public virtual M_Person Employee { get; set; }
    }
}