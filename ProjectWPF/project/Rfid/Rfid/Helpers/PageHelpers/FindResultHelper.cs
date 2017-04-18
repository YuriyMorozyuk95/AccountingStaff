using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Rfid.Context;
using Rfid.Models;
using Rfid.Sql;

namespace Rfid.Helpers.PageHelpers
{
    internal class FindResultHelper
    {
        public void GenereteColumns(DataGrid gridBasicInformation)
        {
            var idColumn = new DataGridTextColumn
            {
                Header = "Id",
                Binding = new Binding("Id")
            };


            var firstNameColumn = new DataGridTextColumn
            {
                Header = "First Name",
                Binding = new Binding("FirstName")
            };


            var lastNameColumn = new DataGridTextColumn
            {
                Header = "Last Name",
                Binding = new Binding("LastName")
            };


            var thridNameColumn = new DataGridTextColumn
            {
                Header = "Third Name",
                Binding = new Binding("ThirdName")
            };


            var departamentNameColumn = new DataGridTextColumn
            {
                Header = "Departament Name",
                Binding = new Binding("Departament")
            };


            var startColumn = new DataGridTextColumn
            {
                Header = "Start",
                Binding = new Binding("Start"),
                ClipboardContentBinding = {StringFormat = "HH:mm"}
            };


            var endColumn = new DataGridTextColumn
            {
                Header = "End",
                Binding = new Binding("End"),
                ClipboardContentBinding = {StringFormat = "HH:mm"}
            };


            var validColumn = new DataGridTextColumn
            {
                Header = "Valid",
                Binding = new Binding("Valid"),
                ClipboardContentBinding = {StringFormat = "HH:mm"}
            };

            var dinnerColumn = new DataGridTextColumn
            {
                Header = "Dinner",
                Binding = new Binding("Dinner"),
                ClipboardContentBinding = {StringFormat = "HH:mm"}
            };


            var phone = new DataGridTextColumn
            {
                Header = "Phone",
                Binding = new Binding("Phone")
            };

            if (Singelton.MainWindow.WindowState == WindowState.Maximized)
                phone.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            else
                phone.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

            gridBasicInformation.Columns.Add(idColumn);
            gridBasicInformation.Columns.Add(firstNameColumn);
            gridBasicInformation.Columns.Add(lastNameColumn);
            gridBasicInformation.Columns.Add(thridNameColumn);
            gridBasicInformation.Columns.Add(departamentNameColumn);
            gridBasicInformation.Columns.Add(startColumn);
            gridBasicInformation.Columns.Add(endColumn);
            gridBasicInformation.Columns.Add(validColumn);
            gridBasicInformation.Columns.Add(dinnerColumn);
            gridBasicInformation.Columns.Add(phone);
        }

        public void Initialize(DataGrid gridBasicInformation, UserFinderHelper find)
        {
            Func<ICollection<M_Phones>, string> genListPhones = cust =>
            {
                var str = string.Empty;
                foreach (var phone in cust)
                {
                    str += phone.PhoneNumber + ", ";
                }
                if (str != string.Empty)
                    str = str.Substring(0, str.Length - 2);
                return str;
            };

            using (var context = new RfidContext())
            {
                find.Context = context;
                var searchResult = (from cust in find.Find()
                    select new
                    {
                        cust.ID,
                        cust.P_Names.FirstOrDefault().NameFirst,
                        cust.P_Names.FirstOrDefault().NameLast,
                        cust.P_Names.FirstOrDefault().NameThird,
                        cust.P_Departments.Name,
                        cust.P_InOutValidTimes.Start,
                        cust.P_InOutValidTimes.End,
                        cust.P_InOutValidTimes.Valid,
                        cust.P_InOutValidTimes.Dinner,
                        cust.P_Phones
                    }).ToList();
                GenereteColumns(gridBasicInformation);

                foreach (var item in searchResult)
                {
                    var tableItem = new TableItem
                    {
                        Id = item.ID,
                        FirstName = item.NameFirst,
                        LastName = item.NameLast,
                        ThirdName = item.NameThird,
                        Departament = item.Name,
                        Start = item.Start,
                        End = item.End,
                        Dinner = (DateTime) item.Dinner,
                        Valid = item.Valid,
                        Phone = genListPhones(item.P_Phones)
                    };

                    gridBasicInformation.Items.Add(tableItem);
                }
            }
        }

        public M_Users GetUserInfo(int selectedId, TextBlock nameUser, ImageBrush imageReport)
        {
            var db = new RfidContext();
            var selectedUser = (from d in db.C_Users
                where d.ID == selectedId
                select d).Single();
            var dir = new DirectoryInfo(Singelton.PathToPhoto);

            var queryPhoto = from cust in db.C_Users
                where cust.ID == selectedId
                select new {cust.Photo};

            var names = db.Database.SqlQuery<M_Names>(new SqlGetUserName()
                .SQl_GetUserFirstName, selectedId).SingleOrDefault();
            var firstName = names.NameFirst;
            var lastName = names.NameLast;
            var z = queryPhoto.ToList();
            nameUser.Text = firstName + " " + lastName;
            var str = dir.FullName + Path.AltDirectorySeparatorChar + z[0].Photo;

            try
            {
                var nameImage = z[0].Photo;
                var words = nameImage.Split('_');
                var indexdot = words[2].IndexOf('.');
                nameUser.Text = words[1] + " " + words[2].Remove(indexdot);
                imageReport.ImageSource = ImageLoaderHelper.GetImageFromFolder(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserPhoto] + ex);
            }

            db.Dispose();
            return selectedUser;
        }
    }


    public class TableItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }
        public string Departament { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Valid { get; set; }
        public DateTime Dinner { get; set; }
        public string Phone { get; set; }
    }
}
