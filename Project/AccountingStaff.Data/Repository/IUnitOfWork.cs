using AccountingStaff.Data.Models;
using System;
using System.Data.Entity;

namespace AccountingStaff.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; set; }

        EFGenericRepository<M_Report> RepositorReport { get; }
        EFGenericRepository<M_ReportType> RepositorReportType { get; }
        EFGenericRepository<M_CalendarDate> RepositoryCalendarDate { get; }
        EFGenericRepository<M_Department> RepositoryDepartment { get; }
        EFGenericRepository<M_PersonInfo> RepositoryNames { get; }
        EFGenericRepository<M_Person> RepositoryPerson { get; }
        EFGenericRepository<M_Role> RepositoryRole { get; }
        EFGenericRepository<M_Phones> RepositoryPhones { get; }
        EFGenericRepository<M_Rfids> RepositoryRfids { get; }
        EFGenericRepository<M_Setting> RepositorySetting { get; }
        EFGenericRepository<M_SettingEmail> RepositorySettingEmail { get; }
        EFGenericRepository<M_SettingExcel> RepositorySettingExcel { get; }
        EFGenericRepository<M_SettingLanguage> RepositorySettingLanguage { get; }
        EFGenericRepository<M_SettingLimitTimer> RepositorySettingLimitTime { get; }
        EFGenericRepository<M_WorkDayTimes> RepositoryWorkDayTimes { get; }
        EFGenericRepository<M_WorkSchedule> RepositoryWorkSchedule { get; }
    }
}