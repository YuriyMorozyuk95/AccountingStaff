using AccountingStaff.Data.Models;
using AccountingStaff.Data.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AccountingStaff.Web.ViewModels
{
    public abstract class ViewModel : IViewModel
    {
        protected M_Base _model;
        
        public ViewModel() { }
        public ViewModel(M_Base model)
        {
            SetModel(model);
        }

        public abstract M_Base GetModel();
        public abstract void SetModel(M_Base model);

        public int Id { get; set; }

        public static string GetName(M_PersonInfo info)
        {
            if (info == null)
            {
                return "TestUser";
            }
            else
            {
                return $"{info?.NameSecond} {info?.NameFirst}";
            }
        }

        public static void SetNamePerson(ref M_PersonInfo info, string namePerson)
        {
            try
            {
            string name, surname;
            string[] splitString = namePerson.Split(' ');

            
                name = splitString[1];
                surname = splitString[0];
            

            info.NameFirst = name;
            info.NameSecond = surname;
            }
            catch
            {
                info = null;
            }
        }

        //Потрібна оптимізація
        public M_Person FindEmployerWithName(string nameEmployer)
        {
            if(nameEmployer == null)
            {
                return null;
            }
            string name, surname;
            M_PersonInfo tempInfo = new M_PersonInfo();
            ViewModel.SetNamePerson(ref tempInfo, nameEmployer);
            name = tempInfo.NameFirst;
            surname = tempInfo.NameSecond;

            IGenericRepository<M_Person> repPerson = new UnitOfWork().RepositoryPerson;
            M_Person employer = repPerson
                .Get(emp => emp.PersonInfo.NameFirst == name && emp.PersonInfo.NameSecond == surname)
                .FirstOrDefault();
            return employer;
        }

    }

    
}