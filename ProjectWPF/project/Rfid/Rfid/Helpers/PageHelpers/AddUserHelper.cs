using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using Rfid.Context;
using Rfid.Models;

namespace Rfid.Helpers
{
    internal class AddUserHelper
    {
        public static M_Users GetNewUserInfo(Grid userInfoGrid, Grid workTimeInfoGrid)
        {
            var user = new M_Users
            {
                P_Names = new List<M_Names>
                {
                    new M_Names
                    {
                        NameFirst = ((TextBox) userInfoGrid.FindName("TB_UserFN")).Text,
                        NameLast = ((TextBox) userInfoGrid.FindName("TB_UserSN")).Text,
                        NameThird = ((TextBox) userInfoGrid.FindName("TB_UserTN")).Text
                    }
                },
                Address = ((TextBox) userInfoGrid.FindName("TB_UsesrAddr")).Text,
                D_Birth = ((DateTimePicker) userInfoGrid.FindName("DP_UserBithday")).SelectedDate,
                HourRate = Convert.ToDouble(((TextBox) workTimeInfoGrid.FindName("TB_HourRate")).Text),
                OvertimeRate = Convert.ToDouble(((TextBox) workTimeInfoGrid.FindName("TB_OverTimerate")).Text),
                IsAdmin = ((CheckBox) userInfoGrid.FindName("CB_IsAdmin")).IsChecked,
                IsDirector = ((CheckBox) userInfoGrid.FindName("CB_IsDirecor")).IsChecked,
                IsAccountant = ((CheckBox) userInfoGrid.FindName("CB_IsAccountant")).IsChecked,
                IsUser = true,
                P_Phones = GetNewPhones((TextBox) userInfoGrid.FindName("PhoneNumber1"),
                    (TextBox) userInfoGrid.FindName("PhoneNumber2"), (TextBox) userInfoGrid.FindName("PhoneNumber3"),
                    (TextBox) userInfoGrid.FindName("TB_UserPhone"))
            };

            return user;
        }

        public static void UpdateUserInfo(Grid userInfoGrid, Grid workTimeInfoGrid, M_Users user)
        {
            user.P_Names.FirstOrDefault().NameFirst = ((TextBox) userInfoGrid.FindName("TB_UserFN")).Text;
            user.P_Names.FirstOrDefault().NameLast = ((TextBox) userInfoGrid.FindName("TB_UserSN")).Text;
            user.P_Names.FirstOrDefault().NameThird = ((TextBox) userInfoGrid.FindName("TB_UserTN")).Text;
            user.Address = ((TextBox) userInfoGrid.FindName("TB_UsesrAddr")).Text;
            user.D_Birth = ((DateTimePicker) userInfoGrid.FindName("DP_UserBithday")).SelectedDate;
            user.IsAdmin = ((CheckBox) userInfoGrid.FindName("CB_IsAdmin")).IsChecked;
            user.IsDirector = ((CheckBox) userInfoGrid.FindName("CB_IsDirecor")).IsChecked;
            user.HourRate = Convert.ToDouble(((TextBox) workTimeInfoGrid.FindName("TB_HourRate")).Text);
            user.OvertimeRate = Convert.ToDouble(((TextBox) workTimeInfoGrid.FindName("TB_OverTimerate")).Text);
            var newPhones = GetNewPhones((TextBox) userInfoGrid.FindName("PhoneNumber1"),
                (TextBox) userInfoGrid.FindName("PhoneNumber2"), (TextBox) userInfoGrid.FindName("PhoneNumber3"),
                (TextBox) userInfoGrid.FindName("TB_UserPhone"));
            var oldPhones = user.P_Phones.ToList();
            for (var i = 0; i < user.P_Phones.Count; i++)
            {
                oldPhones[i].PhoneNumber = newPhones[i].PhoneNumber;
            }
            user.P_Phones = oldPhones;
        }

        public static List<M_Users> GetAllContactsInfo(Grid contactManInfoGrid, M_Users newUser)
        {
            var contacts = new List<M_Users>();

            if (((Grid) contactManInfoGrid.FindName("CMFirstShow")).Visibility == Visibility.Visible)
            {
                contacts.Add(GetContactMan(
                    (TextBox) contactManInfoGrid.FindName("CMFirstName1"),
                    (TextBox) contactManInfoGrid.FindName("CMSecondName1"),
                    (TextBox) contactManInfoGrid.FindName("CMThirdName1"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone11"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone12"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone13"), newUser));
            }

            if (((Grid) contactManInfoGrid.FindName("CMSecondShow")).Visibility == Visibility.Visible)
            {
                contacts.Add(GetContactMan(
                    (TextBox) contactManInfoGrid.FindName("CMFirstName2"),
                    (TextBox) contactManInfoGrid.FindName("CMSecondName2"),
                    (TextBox) contactManInfoGrid.FindName("CMThirdName2"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone21"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone22"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone23"), newUser));
            }

            if (((Grid) contactManInfoGrid.FindName("CMThirdShow")).Visibility == Visibility.Visible)
            {
                contacts.Add(GetContactMan(
                    (TextBox) contactManInfoGrid.FindName("CMFirstName3"),
                    (TextBox) contactManInfoGrid.FindName("CMSecondName3"),
                    (TextBox) contactManInfoGrid.FindName("CMThirdName3"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone31"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone32"),
                    (TextBox) contactManInfoGrid.FindName("CMPhone33"), newUser));
            }

            if (contacts.Count == 0)
            {
                contacts.Add(GetContactMan(
                    (TextBox) contactManInfoGrid.FindName("TB_FN"), (TextBox) contactManInfoGrid.FindName("TB_SN"),
                    (TextBox) contactManInfoGrid.FindName("TB_TN"),
                    (TextBox) contactManInfoGrid.FindName("TB_ContacnManPhone"),
                    new TextBox {Text = string.Empty}, new TextBox {Text = string.Empty}, newUser));
            }

            return contacts;
        }

        public static void UpdateContactsInfo(Grid contactManInfoGrid, M_Users User)
        {
            var newContactsList = GetAllContactsInfo(contactManInfoGrid, User);
            var baseContactsList = User.P_ManForContact.ToList();

            for (var i = 0; i < baseContactsList.Count; i++)
            {
                baseContactsList[i].P_Names.FirstOrDefault().NameFirst =
                    newContactsList[i].P_Names.FirstOrDefault().NameFirst;
                baseContactsList[i].P_Names.FirstOrDefault().NameLast =
                    newContactsList[i].P_Names.FirstOrDefault().NameLast;
                baseContactsList[i].P_Names.FirstOrDefault().NameThird =
                    newContactsList[i].P_Names.FirstOrDefault().NameThird;
                var basePhones = baseContactsList[i].P_Phones.ToList();
                var newPhones = newContactsList[i].P_Phones.ToList();
                for (var j = 0; j < basePhones.Count; j++)
                {
                    basePhones[i].PhoneNumber = newPhones[i].PhoneNumber;
                }
            }
            User.P_ManForContact = baseContactsList;
        }

        public static M_Departments GetDepartmentInfo(Grid departmentInfoGrid, M_Users user)
        {
            var department = new M_Departments
            {
                Name = ((TextBox) departmentInfoGrid.FindName("TB_DepName")).Text,
                CodeFull = ((TextBox) departmentInfoGrid.FindName("TB_DepCode")).Text
            };

            if (user.IsDirector != null && (bool) user.IsDirector)
            {
                department.DepartmentDirectorName = user.P_Names;
                department.DepartmentDirectorPhone = user.P_Phones;
            }
            else
            {
                department.DepartmentDirectorName = new List<M_Names>
                {
                    new M_Names
                    {
                        NameFirst = ((TextBox) departmentInfoGrid.FindName("TB_DepFN")).Text,
                        NameLast = ((TextBox) departmentInfoGrid.FindName("TB_DepSN")).Text,
                        NameThird = ((TextBox) departmentInfoGrid.FindName("TB_DepTN")).Text
                    }
                };
                department.DepartmentDirectorPhone =
                    GetNewPhones((TextBox) departmentInfoGrid.FindName("DepartmentPhoneNumber1"),
                        (TextBox) departmentInfoGrid.FindName("DepartmentPhoneNumber2"),
                        (TextBox) departmentInfoGrid.FindName("DepartmentPhoneNumber3"),
                        (TextBox) departmentInfoGrid.FindName("TB_DirectorPhone"));
                department.P_Users = new List<M_Users>
                {
                    new M_Users
                    {
                        IsDirector = true
                    }
                };
            }

            return department;
        }

        public static void UpdateDepartmentInfo(Grid departmentInfoGrid, M_Users user)
        {
            user.P_Departments.CodeFull = ((TextBox) departmentInfoGrid.FindName("TB_DepCode")).Text;
            user.P_Departments.Name = ((TextBox) departmentInfoGrid.FindName("TB_DepName")).Text;
            user.P_Departments.DepartmentDirectorName.FirstOrDefault().NameFirst =
                ((TextBox) departmentInfoGrid.FindName("TB_DepFN")).Text;
            user.P_Departments.DepartmentDirectorName.FirstOrDefault().NameLast =
                ((TextBox) departmentInfoGrid.FindName("TB_DepSN")).Text;
            user.P_Departments.DepartmentDirectorName.FirstOrDefault().NameThird =
                ((TextBox) departmentInfoGrid.FindName("TB_DepTN")).Text;
        }

        public static M_InOutValidTimes GetInOutValidTimes(Grid workTimeInfoGrid)
        {
            var validInOutTime = new M_InOutValidTimes
            {
                Start = GetValidTime((TimePicker) workTimeInfoGrid.FindName("TB_Start")),
                End = GetValidTime((TimePicker) workTimeInfoGrid.FindName("TB_End")),
                Valid = GetValidTime((TimePicker) workTimeInfoGrid.FindName("TB_Valid")),
                Dinner = GetValidTime((TimePicker) workTimeInfoGrid.FindName("TB_Dinner"))
            };

            return validInOutTime;
        }

        public static void UpdateInOutTimes(Grid workTimeInfoGrid, M_Users user)
        {
            var newTimes = GetInOutValidTimes(workTimeInfoGrid);

            user.P_InOutValidTimes.Start = newTimes.Start;
            user.P_InOutValidTimes.End = newTimes.End;
            user.P_InOutValidTimes.Valid = newTimes.Valid;
            user.P_InOutValidTimes.Dinner = newTimes.Dinner;
        }

        public static M_Rfids GetRfidInfo(Grid rfidGrid)
        {
            var rfid = new M_Rfids
            {
                RfidID = Convert.ToInt64(((TextBox) rfidGrid.FindName("ShowRfid")).Text),
                Date = DateTime.Now
            };

            return rfid;
        }

        public static string GetPhotoPath(M_Users user, bool isImageExists, string img)
        {
            if (!isImageExists) return null;
            if (string.IsNullOrEmpty(img)) return null;
            var imageFile = new FileInfo(img);
            var imageName = string.Empty;
            imageName = string.Format("{0}_{1}_{2}.{3}",
                user.ID,
                user.P_Names.FirstOrDefault().NameFirst.Replace(" ", string.Empty),
                user.P_Names.FirstOrDefault().NameLast.Replace(" ", string.Empty),
                imageFile.ToString().Split('.').Last()
                );

            var dir = new DirectoryInfo(Singelton.PathToPhoto);

            if (!dir.Exists)
                dir.Create();
            var source = img;
            var dest = Path.Combine(dir.FullName, imageName);
            //File.Delete(dest);
            File.Copy(img, dest, true);
            //imageFile.CopyTo(Path.Combine(dir.FullName, imageName), true);
            return imageName;
        }

        private static List<M_Phones> GetNewPhones(TextBox textBox1, TextBox textBox2, TextBox textBox3,
            TextBox textBox4)
        {
            var phonesList = new List<M_Phones>();
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    phonesList.Add(new M_Phones {PhoneNumber = textBox1.Text});
                }

                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    phonesList.Add(new M_Phones {PhoneNumber = textBox2.Text});
                }

                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    phonesList.Add(new M_Phones {PhoneNumber = textBox3.Text});
                }
            }
            else
            {
                phonesList.Add(new M_Phones {PhoneNumber = textBox4.Text});
            }

            return phonesList;
        }

        private static DateTime GetValidTime(TimePicker tp)
        {
            var standartDate = new DateTime(2000, 01, 01);
            var time1 = (TimeSpan) tp.SelectedTime;
            return (standartDate + time1);
        }

        private static M_Users GetContactMan(TextBox firstName, TextBox secondName, TextBox thirdName,
            TextBox contactPhone1, TextBox contactPhone2, TextBox contactPhone3, M_Users newUser)
        {
            var newContact = new M_Users
            {
                P_Names = new List<M_Names>
                {
                    new M_Names
                    {
                        NameFirst = firstName.Text,
                        NameLast = secondName.Text,
                        NameThird = thirdName.Text
                    }
                },
                IsUser = false,
                IsDirector = false,
                isInside = true,
                Address = "",
                Photo = "",
                D_Birth = null,
                P_Rfids = null,
                P_ManForContact = null,
                P_Phones = GetNewPhones(contactPhone1, contactPhone2, contactPhone3, new TextBox {Text = string.Empty}),
                P_UserTime = null,
                P_InOutValidTimes = null
            };
            //Рядок в юзерах, що вказує на нейм
            newContact.P_Names.FirstOrDefault().P_Users = newContact;

            //для якого юзера цей контакт
            newContact.P_Users = newUser;
            return newContact;
        }

        public static void CheckPhoneNum(TextBox boxToCheck, Button addPhone)
        {
            if (Regex.Match(boxToCheck.Text, StringsMapper.PhonesRegExp).Success)
            {
                boxToCheck.Background = Brushes.Green;
                addPhone.IsEnabled = true;
            }
            else
            {
                boxToCheck.Background = Brushes.LightCoral;
                addPhone.IsEnabled = false;
            }
        }

        public static void SetPhones(TextBox tb1, TextBox tb2, TextBox tb3, List<M_Phones> listPhones)
        {
            if (listPhones.Count > 0)
            {
                tb1.Text = listPhones[0].PhoneNumber;
            }

            if (listPhones.Count > 1)
            {
                tb2.Text = listPhones[1].PhoneNumber;
            }

            if (listPhones.Count > 2)
            {
                tb3.Text = listPhones[2].PhoneNumber;
            }
        }

        public static void SetContactMan(TextBox fn, TextBox sn, TextBox tn, TextBox tb1, TextBox tb2, TextBox tb3,
            M_Users cmUser)
        {
            var cmUserName = cmUser.P_Names.Single();
            fn.Text = cmUserName.NameFirst;
            sn.Text = cmUserName.NameLast;
            tn.Text = cmUserName.NameThird;
            SetPhones(tb1, tb2, tb3, cmUser.P_Phones.ToList());
        }

        public static void AddPhoneNumber(TextBox out1, TextBox out2, TextBox out3, TextBox source)
        {
            Func<TextBox, bool> SetTextBoxNumber = outp =>
            {
                bool b;
                if (b = (outp.Text == string.Empty))
                {
                    outp.Text = source.Text;
                }
                return b;
            };


            if (SetTextBoxNumber(out1))
            {
            }
            else if (SetTextBoxNumber(out2))
            {
            }
            else if (SetTextBoxNumber(out3))
            {
            }

            source.Text = string.Empty;
        }

        public static void CreateContactMan(List<M_Users> contactManList, Grid contactManInfoGrid)
        {
            string firstName;
            string secondName;
            string thirdName;

            if (((TextBox) contactManInfoGrid.FindName("TB_FN")).Text != string.Empty &&
                ((TextBox) contactManInfoGrid.FindName("TB_SN")).Text != string.Empty &&
                ((TextBox) contactManInfoGrid.FindName("TB_TN")).Text != string.Empty)
            {
                firstName = ((TextBox) contactManInfoGrid.FindName("TB_FN")).Text;
                secondName = ((TextBox) contactManInfoGrid.FindName("TB_SN")).Text;
                thirdName = ((TextBox) contactManInfoGrid.FindName("TB_TN")).Text;
            }
            else
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserPID],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader], MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Action<Grid, TextBox, TextBox, TextBox> showAndSet = (grid, tb1, tb2, tb3) =>
            {
                grid.Visibility = Visibility.Visible;
                tb1.Text = firstName;
                tb2.Text = secondName;
                tb3.Text = thirdName;
            };

            Action<TextBox, TextBox, TextBox> setNumbers = (tb1, tb2, tb3) =>
            {
                if (((TextBox) contactManInfoGrid.FindName("TB_ContacnManPhone")).Text == string.Empty)
                {
                    if (((TextBox) contactManInfoGrid.FindName("ContactPhonNumber1")).Text != string.Empty)
                    {
                        tb1.Text = ((TextBox) contactManInfoGrid.FindName("ContactPhonNumber1")).Text;
                    }

                    if (((TextBox) contactManInfoGrid.FindName("ContactPhonNumber2")).Text != string.Empty)
                    {
                        tb2.Text = ((TextBox) contactManInfoGrid.FindName("ContactPhonNumber2")).Text;
                    }

                    if (((TextBox) contactManInfoGrid.FindName("ContactPhonNumber3")).Text != string.Empty)
                        tb3.Text = ((TextBox) contactManInfoGrid.FindName("ContactPhonNumber3")).Text;
                }
                else
                {
                    tb1.Text = ((TextBox) contactManInfoGrid.FindName("TB_ContacnManPhone")).Text;
                }
            };

            if (((Grid) contactManInfoGrid.FindName("CMFirstShow")).Visibility == Visibility.Collapsed)
            {
                showAndSet(((Grid) contactManInfoGrid.FindName("CMFirstShow")),
                    ((TextBox) contactManInfoGrid.FindName("CMFirstName1")),
                    ((TextBox) contactManInfoGrid.FindName("CMSecondName1")),
                    ((TextBox) contactManInfoGrid.FindName("CMThirdName1")));
                setNumbers(((TextBox) contactManInfoGrid.FindName("CMPhone11")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone12")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone13")));
            }
            else if (((Grid) contactManInfoGrid.FindName("CMSecondShow")).Visibility == Visibility.Collapsed)
            {
                showAndSet(((Grid) contactManInfoGrid.FindName("CMSecondShow")),
                    ((TextBox) contactManInfoGrid.FindName("CMFirstName2")),
                    ((TextBox) contactManInfoGrid.FindName("CMSecondName2")),
                    ((TextBox) contactManInfoGrid.FindName("CMThirdName2")));
                setNumbers(((TextBox) contactManInfoGrid.FindName("CMPhone21")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone22")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone23")));
            }
            else if (((Grid) contactManInfoGrid.FindName("CMThirdShow")).Visibility == Visibility.Collapsed)
            {
                showAndSet(((Grid) contactManInfoGrid.FindName("CMThirdShow")),
                    ((TextBox) contactManInfoGrid.FindName("CMFirstName3")),
                    ((TextBox) contactManInfoGrid.FindName("CMSecondName3")),
                    ((TextBox) contactManInfoGrid.FindName("CMThirdName3")));
                setNumbers(((TextBox) contactManInfoGrid.FindName("CMPhone31")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone32")),
                    ((TextBox) contactManInfoGrid.FindName("CMPhone33")));
            }
        }

        public static void SaveInfo(int? iD, RfidContext db, bool isImageExist, string imagePath, Grid userInfoGrid,
            Grid workTimeInfoGrid, Grid contactManInfoGrid, Grid rfidGrid, Grid departmentInfoGrid)
        {
            //add or update user info
            if (iD != null)
            {
                var user = db.C_Users.Find(iD);
                UpdateUserInfo(userInfoGrid, workTimeInfoGrid, user);
                UpdateContactsInfo(contactManInfoGrid, user);
                UpdateInOutTimes(workTimeInfoGrid, user);
                user.Photo = GetPhotoPath(user, isImageExist, imagePath);

                //rfid checks and update 
                var newRfid = Convert.ToInt64(((TextBox) userInfoGrid.FindName("ShowRfid")).Text);
                if (!(user.P_Rfids.FirstOrDefault().RfidID == newRfid))
                {
                    foreach (var item in db.C_Rfids)
                    {
                        if (item.RfidID == newRfid && item.IsArhive)
                        {
                            item.IsArhive = false;
                            user.P_Rfids = new List<M_Rfids> {item};
                        }
                        else if (item.RfidID == newRfid && !item.IsArhive)
                        {
                            MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorBindingRfid],
                                (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            user.P_Rfids = new List<M_Rfids> {GetRfidInfo(rfidGrid)};
                        }
                    }
                }

                //departments checks and update
                var newDepName = ((TextBox) departmentInfoGrid.FindName("TB_DepName")).Text;
                var newDepCode = ((TextBox) departmentInfoGrid.FindName("TB_DepCode")).Text;
                var isNoFind = true;

                foreach (var dep in db.C_Departments)
                {
                    if (dep.Name == newDepName && dep.CodeFull == newDepCode)
                    {
                        var userDepartmentDirector = user.P_Departments.DepartmentDirectorName.FirstOrDefault();
                        var userDepartmentPhone = user.P_Departments.DepartmentDirectorPhone.FirstOrDefault();
                        var departmentDirector = dep.DepartmentDirectorName.FirstOrDefault();
                        var departmentPhone = dep.DepartmentDirectorPhone.FirstOrDefault();

                        user.P_Departments.CodeFull = dep.CodeFull;
                        user.P_Departments.Name = dep.Name;

                        userDepartmentDirector.NameFirst = departmentDirector?.NameFirst;
                        userDepartmentDirector.NameLast = departmentDirector?.NameLast;
                        userDepartmentDirector.NameThird = departmentDirector?.NameThird;
                        userDepartmentPhone.PhoneNumber = departmentPhone?.PhoneNumber;

                        isNoFind = false;
                        break;
                    }
                }

                if (isNoFind)
                {
                    user.P_Departments = GetDepartmentInfo(departmentInfoGrid, user);
                }
            }
            else
            {
                //create new user with input info
                var newUser = GetNewUserInfo(userInfoGrid, workTimeInfoGrid);
                newUser.P_ManForContact = GetAllContactsInfo(contactManInfoGrid, newUser);
                newUser.P_InOutValidTimes = GetInOutValidTimes(workTimeInfoGrid);
                newUser.Photo = GetPhotoPath(newUser, isImageExist, imagePath);

                //department adding & checking for existing departments
                var isNoFind = true;
                foreach (var dep in db.C_Departments)
                {
                    if (dep.Name == ((TextBox) departmentInfoGrid.FindName("TB_DepName")).Text &&
                        dep.CodeFull == ((TextBox) departmentInfoGrid.FindName("TB_DepCode")).Text)
                    {
                        dep.P_Users.Add(newUser);
                        isNoFind = false;
                        break;
                    }
                }
                if (isNoFind)
                {
                    newUser.P_Departments = GetDepartmentInfo(departmentInfoGrid, newUser);
                }

                //rfid checks and adding 
                var changeRfid = true;
                foreach (var itemRfid in db.C_Rfids)
                {
                    if (itemRfid.RfidID == Convert.ToInt64(((TextBox) userInfoGrid.FindName("ShowRfid")).Text) &&
                        itemRfid.IsArhive)
                    {
                        itemRfid.IsArhive = false;
                        itemRfid.P_Users = newUser;
                        changeRfid = false;
                        break;
                    }

                    if (itemRfid.RfidID == Convert.ToInt64(((TextBox) userInfoGrid.FindName("ShowRfid")).Text) &&
                        !itemRfid.IsArhive)
                    {
                        MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorBindingRfid],
                            (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (changeRfid)
                {
                    newUser.P_Rfids = new List<M_Rfids> {GetRfidInfo(rfidGrid)};
                }

                db.C_Users.Add(newUser);
            }

            db.SaveChanges();
            var p = new StartUpPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }
    }
}
