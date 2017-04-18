using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Models;
using Metro = MahApps.Metro.Controls;

namespace Rfid
{
    public partial class AddOrUpdateUserPage
    {
        private readonly List<M_Users> _contactMan = new List<M_Users>();
        private readonly RfidContext _db = new RfidContext();
        private readonly int? _iD;
        private string _imagePath;

        public bool ChangeUserFalg { get; set; } = false;
        public bool IsImageExist { get; private set; }
        public M_Users CurrentUser { get; }
        public DirectorSearcherHelper AutoCompliteDirectorHelper { get; private set; }

        public AddOrUpdateUserPage()
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.UpdateImage]);
            _db.C_Users.ToList();
            InitializeComponent();

            if (StartUpPage.CheckedPort.Length == 0)
            {
                AddRfid.IsEnabled = false;
            }
        }

        public AddOrUpdateUserPage(int id)
        {
            InitializeComponent();
            _iD = id;
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.UpdateImage]);

            CurrentUser = (from x in _db.C_Users
                where x.ID == id
                select x).Single();

            var dir = new DirectoryInfo(Singelton.PathToPhoto);
            var str = dir.FullName + Path.AltDirectorySeparatorChar + CurrentUser.Photo;

            try
            {
                imgPhoto.Source = ImageLoaderHelper.GetImageFromFolder(str);
                IsImageExist = false;
            }
            catch (Exception)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserPhoto],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            //UserInformationSelection
            var nameCurrentUser = CurrentUser.P_Names.Single();
            TB_UserFN.Text = nameCurrentUser.NameFirst;
            TB_UserSN.Text = nameCurrentUser.NameLast;
            TB_UserTN.Text = nameCurrentUser.NameThird;
            TB_UsesrAddr.Text = CurrentUser.Address;
            DP_UserBithday.SelectedDate = CurrentUser.D_Birth;
            var phoneCurrentUser = CurrentUser.P_Phones.ToList();

            AddUserHelper.SetPhones(PhoneNumber1, PhoneNumber2, PhoneNumber3, phoneCurrentUser);

            var cmUsers = CurrentUser.P_ManForContact.ToList();

            if (cmUsers.Count > 0)
            {
                CMFirstShow.Visibility = Visibility.Visible;
                AddUserHelper.SetContactMan(CMFirstName1, CMSecondName1, CMThirdName1,
                    CMPhone11, CMPhone12, CMPhone13, cmUsers[0]);
            }
            if (cmUsers.Count > 1)
            {
                CMSecondShow.Visibility = Visibility.Visible;
                AddUserHelper.SetContactMan(CMFirstName2, CMSecondName2, CMThirdName2,
                    CMPhone21, CMPhone22, CMPhone23, cmUsers[1]);
            }
            if (cmUsers.Count > 2)
            {
                CMThirdShow.Visibility = Visibility.Visible;
                AddUserHelper.SetContactMan(CMFirstName3, CMSecondName3, CMThirdName3,
                    CMPhone31, CMPhone32, CMPhone33, cmUsers[2]);
            }

            var depCurrentUser = CurrentUser.P_Departments;
            TB_DepName.Text = depCurrentUser.Name;
            TB_DepCode.Text = depCurrentUser.CodeFull;
            var derName = depCurrentUser.DepartmentDirectorName.Single();
            TB_DepFN.Text = derName.NameFirst;
            TB_DepSN.Text = derName.NameLast;
            TB_DepTN.Text = derName.NameThird;
            AddUserHelper.SetPhones(DepartmentPhoneNumber1, DepartmentPhoneNumber2, DepartmentPhoneNumber3,
                depCurrentUser.DepartmentDirectorPhone.ToList());
            //Rfid
            ShowRfid.Text = CurrentUser.P_Rfids.Last().RfidID.ToString();
            //  WorkTime
            var timeCurrentUser = CurrentUser.P_InOutValidTimes;
            TB_Start.SelectedTime = timeCurrentUser.Start.TimeOfDay;
            TB_End.SelectedTime = timeCurrentUser.End.TimeOfDay;
            TB_Valid.SelectedTime = timeCurrentUser.Valid.TimeOfDay;

            TB_HourRate.Text = CurrentUser.HourRate.ToString();
            TB_OverTimerate.Text = CurrentUser.OvertimeRate.ToString();

            if (timeCurrentUser.Dinner == null)
            {
                TB_Dinner.SelectedTime = DateTime.Now.TimeOfDay;
            }

            TB_Dinner.SelectedTime = ((DateTime) timeCurrentUser.Dinner).TimeOfDay;
            CB_IsAdmin.IsChecked = CurrentUser.IsAdmin;
            CB_IsDirecor.IsChecked = CurrentUser.IsDirector;
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(UploadImageSelection, e);
            ToolTipService.SetIsEnabled(UserInformationSelection, e);
            ToolTipService.SetIsEnabled(ContactManSelection, e);
            ToolTipService.SetIsEnabled(TimeWork, e);
            ToolTipService.SetIsEnabled(DepartmentInfoSelection, e);
            ToolTipService.SetIsEnabled(RfiNumbertSelection, e);
            ToolTipService.SetIsEnabled(upload, e);
            ToolTipService.SetIsEnabled(SaveChanges, e);
            ToolTipService.SetIsEnabled(Button_AddUserPhone, e);
            ToolTipService.SetIsEnabled(Button_AddUserPhone, e);
            ToolTipService.SetIsEnabled(Button_AddContacnManPhone, e);
            ToolTipService.SetIsEnabled(Button_AddContacnMan, e);
            ToolTipService.SetIsEnabled(Button_AddDirectorPhone, e);
            ToolTipService.SetIsEnabled(AddRfid, e);
        }

        public void HideGrids()
        {
            UploadImageGrid.Visibility = Visibility.Collapsed;
            UserInformationGrid.Visibility = Visibility.Collapsed;
            ContactManGrid.Visibility = Visibility.Collapsed;
            WorkTimeGrid.Visibility = Visibility.Collapsed;
            DepatmantInformationGrid.Visibility = Visibility.Collapsed;
            RfidNumberGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            //basic fields cheking
            if (string.IsNullOrEmpty(TB_UserFN.Text) || string.IsNullOrEmpty(TB_UserSN.Text) ||
                string.IsNullOrEmpty(TB_UserTN.Text))
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorEmptyUserNames],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TB_DepName.Text) || string.IsNullOrEmpty(TB_DepCode.Text))
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorEmptyDepartment],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddUserHelper.SaveInfo(_iD, _db, IsImageExist, _imagePath, UserInfoGrid, WorkTimeInfoGrid, ContactManInfoGrid,
                RfidGrid, DepartmentInfoGrid);
        }

        private void Button_UploadImage(object sender, RoutedEventArgs e)
        {
            var op = new OpenFileDialog
            {
                Title = "Select a photo",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = ImageLoaderHelper.GetImageFromFolder(op.FileName);
                _imagePath = op.FileName;
                IsImageExist = true;
            }
        }

        private void Button_AddUserPhone_Click(object sender, RoutedEventArgs e)
        {
            AddUserHelper.AddPhoneNumber(PhoneNumber1, PhoneNumber2, PhoneNumber3, TB_UserPhone);
        }

        private void Button_AddContactManPhone_Click(object sender, RoutedEventArgs e)
        {
            AddUserHelper.AddPhoneNumber(ContactPhonNumber1, ContactPhonNumber2, ContactPhonNumber3, TB_ContacnManPhone);
        }

        private void Button_AddContactMan_Click(object sender, RoutedEventArgs e)
        {
            AddUserHelper.CreateContactMan(_contactMan, ContactManInfoGrid);
            TB_FN.Text = string.Empty;
            TB_SN.Text = string.Empty;
            TB_TN.Text = string.Empty;
            ContactPhonNumber1.Text = string.Empty;
            ContactPhonNumber2.Text = string.Empty;
            ContactPhonNumber3.Text = string.Empty;
        }

        private void Button_AddDirectPhone_Click(object sender, RoutedEventArgs e)
        {
            AddUserHelper.AddPhoneNumber(DepartmentPhoneNumber1, DepartmentPhoneNumber2, DepartmentPhoneNumber3,
                TB_DirectorPhone);
        }

        private void Click_AddRfid(object sender, RoutedEventArgs e)
        {
            var portDataReceived = new PortDataReceivedHelper();
            ShowRfid.Text = portDataReceived.ParseInData.ToString();
        }

        private void UploadImageSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.UpdateImage]);
            HideGrids();
            UploadImageGrid.Visibility = Visibility.Visible;
        }

        private void UserInformationSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.UserInformation]);
            HideGrids();
            UserInformationGrid.Visibility = Visibility.Visible;
        }

        private void ContactManSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.ContactPeople]);
            HideGrids();
            ContactManGrid.Visibility = Visibility.Visible;
        }

        private void TimeWork_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.WorkTime]);
            HideGrids();
            WorkTimeGrid.Visibility = Visibility.Visible;
        }

        private void DepartmentInfoSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.DepartmentInfo]);
            HideGrids();
            DepatmantInformationGrid.Visibility = Visibility.Visible;
        }

        private void RfiNumbertSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.RfidNumber]);
            HideGrids();
            RfidNumberGrid.Visibility = Visibility.Visible;
        }

        private void ResetButton1_Click(object sender, RoutedEventArgs e)
        {
            ContactPhonNumber1.Text = string.Empty;
        }

        private void ResetButton2_Click(object sender, RoutedEventArgs e)
        {
            ContactPhonNumber2.Text = string.Empty;
        }

        private void UserResetButton1_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumber1.Text = string.Empty;
        }

        private void UserResetButton2_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumber2.Text = string.Empty;
        }

        private void DepartamentRemoveButton1_Click(object sender, RoutedEventArgs e)
        {
            DepartmentPhoneNumber1.Text = string.Empty;
        }

        private void DepartamentRemoveButton2_Click(object sender, RoutedEventArgs e)
        {
            DepartmentPhoneNumber2.Text = string.Empty;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AutoCompliteDirectorHelper = new DirectorSearcherHelper();
            AutoCompliteDirectorHelper.FactoryDirectors();
            AutoCompliteDirectorHelper.SetAutoComplite(TB_Search);

            SetEnableToolTip();

            if (_iD == null)
            {
                Singelton.MainWindow.ChangeStringAddres(Title);
                //Clear and Hide all fields
                CMFirstShow.Visibility = Visibility.Collapsed;
                CMSecondShow.Visibility = Visibility.Collapsed;
                CMThirdShow.Visibility = Visibility.Collapsed;
                CMFirstName1.Text = string.Empty;
                CMFirstName2.Text = string.Empty;
                CMFirstName3.Text = string.Empty;
                CMSecondName1.Text = string.Empty;
                CMSecondName2.Text = string.Empty;
                CMSecondName3.Text = string.Empty;
                CMThirdName1.Text = string.Empty;
                CMThirdName2.Text = string.Empty;
                CMThirdName3.Text = string.Empty;
                CMPhone11.Text = string.Empty;
                CMPhone12.Text = string.Empty;
                CMPhone13.Text = string.Empty;
                CMPhone21.Text = string.Empty;
                CMPhone22.Text = string.Empty;
                CMPhone23.Text = string.Empty;
                CMPhone31.Text = string.Empty;
                CMPhone32.Text = string.Empty;
                CMPhone33.Text = string.Empty;
                DP_UserBithday.SelectedDate = new DateTime(1990, 1, 1, 12, 0, 0);
                TB_Start.SelectedTime = new TimeSpan(9, 0, 0);
                TB_End.SelectedTime = new TimeSpan(17, 0, 0);
                TB_Valid.SelectedTime = new TimeSpan(0, 15, 0);
                TB_Dinner.SelectedTime = new TimeSpan(13, 0, 0);
            }
        }

        private void ResetButton3_Click(object sender, RoutedEventArgs e)
        {
            ContactPhonNumber3.Text = string.Empty;
        }

        private void DepartamentRemoveButton3_Click(object sender, RoutedEventArgs e)
        {
            DepartmentPhoneNumber3.Text = string.Empty;
        }

        private void UserResetButton3_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumber3.Text = string.Empty;
        }

        private void TB_Search_ItemSelected(object sender, EventArgs e)
        {
            var directorInfo = (from dir in AutoCompliteDirectorHelper.Directors
                where dir.FullSearchString == TB_Search.SelectedItem
                select dir).FirstOrDefault();
            TB_DepName.Text = directorInfo.NameDepart;
            TB_DepCode.Text = directorInfo.CodeDepart;
            TB_DepFN.Text = directorInfo.First;
            TB_DepSN.Text = directorInfo.Last;
            TB_DepTN.Text = directorInfo.Third;
            TB_DirectorPhone.Text = directorInfo.PhoneNumber;
        }

        public void AutoTestToSaveChanges(string i = "14")
        {
            //UserInformationSelection
            TB_UserFN.Text = "B4dasfdsfs";
            TB_UserSN.Text = "43fdsfds3C";
            TB_UserTN.Text = "User Third Name";
            TB_UsesrAddr.Text = "User Addres";
            PhoneNumber1.Text = "097111111";
            PhoneNumber2.Text = "097111112";
            PhoneNumber3.Text = "097111113";

            //cm
            CMFirstShow.Visibility = Visibility.Visible;
            CMSecondShow.Visibility = Visibility.Visible;
            CMThirdShow.Visibility = Visibility.Visible;

            CMFirstName1.Text = "Contact man1 first name" + i;
            CMSecondName1.Text = "Contact man1 second name" + i;
            CMThirdName1.Text = "Contact man1 second name" + i;
            CMPhone11.Text = "097111111" + i;
            CMPhone12.Text = "097111112" + i;
            CMPhone13.Text = "097111113" + i;
            CMFirstName2.Text = "Contact man2 first name" + i;
            CMSecondName2.Text = "Contact man2 second name" + i;
            CMThirdName2.Text = "Contact man2 second name" + i;
            CMPhone21.Text = "097111121" + i;
            CMPhone22.Text = "097111122" + i;
            CMPhone23.Text = "097111123" + i;
            CMFirstName3.Text = "Contact man3 first name" + i;
            CMSecondName3.Text = "Contact man3 second name" + i;
            CMThirdName3.Text = "Contact man3 second name" + i;
            CMPhone31.Text = "097111131" + i;
            CMPhone32.Text = "097111132" + i;
            CMPhone33.Text = "09711113" + i;
            //
            TB_DepName.Text = "DepartmentName" + i;
            TB_DepCode.Text = "11111" + i;
            TB_DepFN.Text = "Director First Name" + i;
            TB_DepSN.Text = "Director Second Name" + i;
            TB_DepTN.Text = "Director Third Name" + i;
            DepartmentPhoneNumber1.Text = "097111111" + i;
            DepartmentPhoneNumber2.Text = "097111112" + i;
            DepartmentPhoneNumber3.Text = "097111113" + i;
            //
            ShowRfid.Text = "1111111";
        }

        private void TB_End_SelectedTimeChanged(object sender,
            Metro.TimePickerBaseSelectionChangedEventArgs<TimeSpan?> e)
        {
            if (string.Compare(TB_End.SelectedTime.ToString(), TB_Start.SelectedTime.ToString()) != 1 &&
                TB_End.SelectedTime != null && TB_Start.SelectedTime != null)
            {
                MessageBox.Show(Application.Current.Resources[StringsMapper.ErrorWorkTime].ToString(),
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Error);
                TB_Start.SelectedTime = new TimeSpan(8, 0, 0);
                TB_End.SelectedTime = new TimeSpan(18, 0, 0);
            }
        }

        private void TB_UserPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddUserHelper.CheckPhoneNum(TB_UserPhone, Button_AddUserPhone);
        }

        private void TB_ContacnManPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddUserHelper.CheckPhoneNum(TB_ContacnManPhone, Button_AddContacnManPhone);
        }

        private void TB_DirectorPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddUserHelper.CheckPhoneNum(TB_DirectorPhone, Button_AddDirectorPhone);
        }
    }
}

