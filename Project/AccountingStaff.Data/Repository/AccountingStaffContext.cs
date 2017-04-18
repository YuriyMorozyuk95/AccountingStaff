using AccountingStaff.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Repository
{
    public class AccountingStaffContext : DbContext
    {

        public AccountingStaffContext() : base("AccountingStaffConnection")
        { }

        public DbSet<M_CalendarDate> C_CalendarDate { get; set; }
        public DbSet<M_Department> C_Department { get; set; }
        public DbSet<M_PersonInfo> C_Names { get; set; }
        public DbSet<M_Person> C_Person { get; set; }
        public DbSet<M_Phones> C_Phones { get; set; }
        public DbSet<M_Role> C_Role { get; set; }
        public DbSet<M_Rfids> C_Rfids { get; set; }
        public DbSet<M_Setting> C_Setting { get; set; }
        public DbSet<M_SettingEmail> C_SettingEmail { get; set; }
        public DbSet<M_SettingExcel> C_SettingExcel { get; set; }
        public DbSet<M_SettingLanguage> C_SettingLanguage { get; set; }
        public DbSet<M_SettingLimitTimer> C_SettingLimitTimer { get; set; }
        public DbSet<M_WorkDayTimes> C_WorkDayTimes { get; set; }
        public DbSet<M_WorkSchedule> C_WorkSchedule { get; set; }
        public DbSet<M_Report> C_Report { get; set; }
        public DbSet<M_ReportType> C_ReportType { get; set; }
    }
}
