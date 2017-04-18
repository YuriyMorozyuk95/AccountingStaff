using AccountingStaff.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        private EFGenericRepository<M_CalendarDate> _repositoryCalendarDate;
        private EFGenericRepository<M_Department> _repositoryDepartment;
        private EFGenericRepository<M_PersonInfo> _repositoryNames;
        private EFGenericRepository<M_Person> _repositoryPerson;
        private EFGenericRepository<M_Role> _repositorRole;
        private EFGenericRepository<M_Phones> _repositoryPhones;
        private EFGenericRepository<M_Rfids> _repositoryRfids;
        private EFGenericRepository<M_Setting> _repositorySetting;
        private EFGenericRepository<M_SettingEmail> _repositorySettingEmail;
        private EFGenericRepository<M_SettingExcel> _repositorySettingExcel;
        private EFGenericRepository<M_SettingLanguage> _repositorySettingLanguage;
        private EFGenericRepository<M_SettingLimitTimer> _repositorySettingLimitTime;
        private EFGenericRepository<M_WorkDayTimes> _repositoryWorkDayTimes;
        private EFGenericRepository<M_WorkSchedule> _repositoryWorkSchedule;
        private EFGenericRepository<M_Report> _repositorReport;
        private EFGenericRepository<M_ReportType> _repositorReportType;

        public DbContext Context
        {
            get
            {
                return _context ?? new AccountingStaffContext();
            }
            set
            {
                _context = value;
            }
        }     

        public EFGenericRepository<M_CalendarDate> RepositoryCalendarDate
        {
            get
            {
                return _repositoryCalendarDate ?? new EFGenericRepository<M_CalendarDate>(Context);
            }
        }
        public EFGenericRepository<M_Department> RepositoryDepartment
        {
            get
            {
                return _repositoryDepartment ?? new EFGenericRepository<M_Department>(Context);
            }
        }
        public EFGenericRepository<M_PersonInfo> RepositoryNames
        {
            get
            {
                return _repositoryNames ?? new EFGenericRepository<M_PersonInfo>(Context);
            }
        }
        public EFGenericRepository<M_Person> RepositoryPerson
        {
            get
            {
                return _repositoryPerson ?? new EFGenericRepository<M_Person>(Context);
            }
        }
        public EFGenericRepository<M_Phones> RepositoryPhones
        {
            get
            {
                return _repositoryPhones ?? new EFGenericRepository<M_Phones>(Context);
            }
        }
        public EFGenericRepository<M_Rfids> RepositoryRfids
        {
            get
            {
                return _repositoryRfids ?? new EFGenericRepository<M_Rfids>(Context);
            }
        }
        public EFGenericRepository<M_Setting> RepositorySetting
        {
            get
            {
                return _repositorySetting ?? new EFGenericRepository<M_Setting>(Context);
            }
        }
        public EFGenericRepository<M_SettingEmail> RepositorySettingEmail
        {
            get
            {
                return _repositorySettingEmail ?? new EFGenericRepository<M_SettingEmail>(Context);
            }
        }
        public EFGenericRepository<M_SettingExcel> RepositorySettingExcel
        {
            get
            {
                return _repositorySettingExcel ?? new EFGenericRepository<M_SettingExcel>(Context);
            }
        }
        public EFGenericRepository<M_SettingLanguage> RepositorySettingLanguage
        {
            get
            {
                return _repositorySettingLanguage ?? new EFGenericRepository<M_SettingLanguage>(Context);
            }
        }
        public EFGenericRepository<M_SettingLimitTimer> RepositorySettingLimitTime
        {
            get
            {
                return _repositorySettingLimitTime ?? new EFGenericRepository<M_SettingLimitTimer>(Context);
            }
        }
        public EFGenericRepository<M_WorkDayTimes> RepositoryWorkDayTimes
        {
            get
            {
                return _repositoryWorkDayTimes ?? new EFGenericRepository<M_WorkDayTimes>(Context);
            }
        }
        public EFGenericRepository<M_WorkSchedule> RepositoryWorkSchedule
        {
            get
            {
                return _repositoryWorkSchedule ?? new EFGenericRepository<M_WorkSchedule>(Context);
            }
        }
        public EFGenericRepository<M_Report> RepositorReport
        {
            get
            {
                return _repositorReport ?? new EFGenericRepository<M_Report>(Context);
            }
        }
        public EFGenericRepository<M_ReportType> RepositorReportType
        {
            get
            {
                return _repositorReportType ?? new EFGenericRepository<M_ReportType>(Context);
            }
        }
        public EFGenericRepository<M_Role> RepositoryRole
        {
            get
            {
                return _repositorRole ?? new EFGenericRepository<M_Role>(Context);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
