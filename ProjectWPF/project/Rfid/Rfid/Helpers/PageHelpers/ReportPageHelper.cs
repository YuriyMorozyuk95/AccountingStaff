using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rfid.Context;
using Rfid.Models;
using Rfid.Sql;

namespace Rfid.Helpers.PageHelpers
{
    internal class ReportPageHelper
    {
        public M_Users GetCurrentUser(int? selectedId, DataGrid monthlyReportGrid, DataGrid basicInformationGrid)
        {
            var db = new RfidContext();
            var selectedUser = (from d in db.C_Users
                where d.ID == selectedId
                select d).Single();
            var culture = App.Language;

            if (selectedUser.P_UserTime.ToList().Count != 0)
            {
                var queryAllCustomers = (from cust in db.C_UserTime
                    where cust.P_Users.ID == selectedId
                    select cust)
                    .AsEnumerable()
                    .Select(cust => new
                    {
                        cust.ID,
                        cust.SingleDate,
                        Day = culture.DateTimeFormat.GetDayName(((DayOfWeek) Enum.Parse(typeof (DayOfWeek), cust.Day))),
                        cust.TimeIn,
                        cust.TimeOut,
                        cust.LengthOfInside,
                        cust.LengthOfOutside
                    });

                monthlyReportGrid.ItemsSource = queryAllCustomers.ToList();
                monthlyReportGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserTimeNFound],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Error);
                monthlyReportGrid.ItemsSource = null;
            }

            // get your 'Uploaded' folder
            var dir = new DirectoryInfo(Singelton.PathToPhoto);
            var queryPhoto = from cust in db.C_Users
                where cust.ID == selectedId
                select new {cust.Photo};
            var names = db.Database
                .SqlQuery<M_Names>(new SqlGetUserName().SQl_GetUserFirstName, (object) selectedId)
                .SingleOrDefault();
            var firstName = names.NameFirst;
            var lastName = names.NameLast;
            var z = queryPhoto.ToList();
            ((TextBlock) basicInformationGrid.FindName("NameUser")).Text = firstName + " " + lastName;
            var str = dir.FullName + Path.AltDirectorySeparatorChar + z[0].Photo;
            

            try
            {
                var NameImage = z[0].Photo;
                var words = NameImage.Split('_');
                var indexdot = words[2].IndexOf('.');
                ((TextBlock) basicInformationGrid.FindName("NameUser")).Text = words[1] + " " + words[2].Remove(indexdot);
                ((ImageBrush) basicInformationGrid.FindName("imageReport")).ImageSource =
                    ImageLoaderHelper.GetImageFromFolder(str);
            }
            catch (Exception)
            {
                ((ImageBrush) basicInformationGrid.FindName("imageReport")).ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/Image/ProfileIcon.png"));
                var message = (string) Application.Current.Resources[StringsMapper.ErrorUserPhoto];
                MessageBox.Show(message, (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


            db.Dispose();
            return selectedUser;
        }
    }
}
