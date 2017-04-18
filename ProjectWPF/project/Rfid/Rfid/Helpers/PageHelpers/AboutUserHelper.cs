using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Rfid.Context;
using Rfid.Models;

namespace Rfid.Helpers
{
    internal class AboutUserHelper
    {
        public void Run(int id, RfidContext db, M_Users currentUser, Grid aboutUserGrid)
        {
            var names = currentUser.P_Names.SingleOrDefault();
            var dir = new DirectoryInfo(Singelton.PathToPhoto);
            try
            {
                var str = dir.FullName + Path.AltDirectorySeparatorChar + currentUser.Photo;
                ((ImageBrush) aboutUserGrid.FindName("imageUser")).ImageSource =
                    ImageLoaderHelper.GetImageFromFolder(str);
            }
            catch
            {
            }

            ((Label) aboutUserGrid.FindName("LNameR")).Content = names.NameFirst;
            ((Label) aboutUserGrid.FindName("LMiddleNameR")).Content = names.NameLast;
            ((Label) aboutUserGrid.FindName("LSurnameR")).Content = names.NameThird;
            ((TextBlock) aboutUserGrid.FindName("NameUser")).Text = $"{names.NameFirst} {names.NameLast}";


            try
            {
                ((Label) aboutUserGrid.FindName("LDateOfBirthR")).Content =
                    currentUser.D_Birth.Value.ToShortDateString();
            }
            catch (Exception ex)
            {
            }

            ((Label) aboutUserGrid.FindName("LAddressR")).Content = currentUser.Address;
            var userPhones = new List<M_Phones>();
            userPhones = currentUser.P_Phones.ToList();

            foreach (var phone in userPhones)
            {
                ((Label) aboutUserGrid.FindName("LPhoneR")).Content += phone.PhoneNumber;

                if (phone != userPhones[userPhones.Count - 1])
                {
                    ((Label) aboutUserGrid.FindName("LPhoneR")).Content += ", ";
                }
            }

            ((Label) aboutUserGrid.FindName("LHourRateR")).Content = currentUser.HourRate;
            ((Label) aboutUserGrid.FindName("LOvertimeRateR")).Content = currentUser.OvertimeRate;


            var departmentsName = new M_Names();
            departmentsName = currentUser.P_Departments.DepartmentDirectorName.SingleOrDefault();

            ((Label) aboutUserGrid.FindName("LDepartmentR")).Content = currentUser.P_Departments.Name;

            if (departmentsName != null)
            {
                ((Label) aboutUserGrid.FindName("LHeadOfDepartmentR")).Content += departmentsName.NameFirst + " ";
                ((Label) aboutUserGrid.FindName("LHeadOfDepartmentR")).Content += departmentsName.NameLast + " ";
                ((Label) aboutUserGrid.FindName("LHeadOfDepartmentR")).Content += departmentsName.NameThird + " ";
            }

            var departmentsPhones = new List<M_Phones>();
            departmentsPhones = currentUser.P_Departments.DepartmentDirectorPhone.ToList();


            foreach (var phone in departmentsPhones)
            {
                ((Label) aboutUserGrid.FindName("LPhoneNumberR")).Content += phone.PhoneNumber;

                if (phone != departmentsPhones[departmentsPhones.Count - 1])
                {
                    ((Label) aboutUserGrid.FindName("LPhoneR")).Content += ", ";
                }
            }

            try
            {
                var contactUsers = new List<M_Users>();
                contactUsers = currentUser.P_ManForContact.ToList();

                var contactNames = new M_Names();
                var contactPhones = new List<M_Phones>();

                contactNames = contactUsers[0].P_Names.ToList()[0];
                contactPhones = contactUsers[0].P_Phones.ToList();

                ((Label) aboutUserGrid.FindName("LContactDetailsFace1R")).Content = contactNames.NameFirst + " " +
                                                                                    contactNames.NameLast + " " +
                                                                                    contactNames.NameThird + " ";

                foreach (var cPhones in contactPhones)
                {
                    ((Label) aboutUserGrid.FindName("LPhoneNumber1R")).Content += cPhones.PhoneNumber;

                    if (cPhones != contactPhones[contactPhones.Count - 1])
                    {
                        ((Label) aboutUserGrid.FindName("LPhoneR")).Content += ", ";
                    }
                }

                contactNames = contactUsers[1].P_Names.ToList()[0];
                contactPhones = contactUsers[1].P_Phones.ToList();

                ((Label) aboutUserGrid.FindName("LContactDetailsFace2R")).Content = contactNames.NameFirst + " " +
                                                                                    contactNames.NameLast + " " +
                                                                                    contactNames.NameThird + " ";

                foreach (var cPhones in contactPhones)
                {
                    ((Label) aboutUserGrid.FindName("LPhoneNumber2R")).Content += cPhones.PhoneNumber + " ";
                }
            }
            catch
            {
            }

            var prfids = new List<M_Rfids>();
            prfids = currentUser.P_Rfids.ToList();
            try
            {
                var rfideCode = prfids.Single(x => x.IsArhive == false);
                ((Label) aboutUserGrid.FindName("LRfidCode")).Content = rfideCode.RfidID.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorRfidNFound],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader
                        ],
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }


            if (currentUser.Comment != null)
            {
                ((TextBox) aboutUserGrid.FindName("TBComment")).Text = currentUser.Comment;
            }

            if (prfids[0].Comment != null)
            {
                ((TextBox) aboutUserGrid.FindName("TBRfidCOmment")).Text = prfids[0].Comment;
            }
        }

        public void ChangeStatus(M_Users currentUser)
        {
            var dsd = currentUser.P_Rfids.SingleOrDefault();

            if (dsd.P_Users.isInside == false)
            {
                TimeSpan? lengthOfInside = new TimeSpan();

                if (dsd.P_Users.P_UserTime.Count != 0)
                {
                    lengthOfInside = DateTime.Now - dsd.P_Users.P_UserTime.Last().TimeOut;
                    var date = DateTime.Today.Add(lengthOfInside.Value);
                    DateTime? dtOutsige = DateTime.Today.Add(lengthOfInside.Value);
                    dsd.P_Users.P_UserTime.Last().LengthOfOutside = dsd.P_Users.P_UserTime.Last().Day ==
                                                                    DateTime.Today.DayOfWeek.ToString()
                        ? dtOutsige
                        : null;
                }

                dsd.P_Users.P_UserTime.Add(new M_UserTime {SingleDate = DateTime.Now.Date});
                dsd.P_Users.P_UserTime.Last().Day = DateTime.Now.DayOfWeek.ToString();
                dsd.P_Users.P_UserTime.Last().TimeIn = DateTime.Now;
                dsd.P_Users.isInside = true;
            }
            else
            {
                dsd.P_Users.P_UserTime.Last().TimeOut = DateTime.Now;
                var lengthOfInside = dsd.P_Users.P_UserTime.Last().TimeOut - dsd.P_Users.P_UserTime.Last().TimeIn;

                if (lengthOfInside > Singelton.WatcherSetting.MaxTimeInside)
                {
                    lengthOfInside = Singelton.WatcherSetting.MaxTimeInside;
                }
                DateTime? dtInsige = DateTime.Today.Add(lengthOfInside.Value);
                dsd.P_Users.P_UserTime.Last().LengthOfInside = dtInsige;
                dsd.P_Users.isInside = false;
            }
        }
    }
}
