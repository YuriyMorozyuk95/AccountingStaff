using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Helpers.PageHelpers;
using Rfid.Models;
using Rfid.Sql;

namespace Rfid
{
    public partial class ReportPage
    {
        public ReportPage()
        {
            InitializeComponent();
            var db = new RfidContext();
            GridBasicInformation.EnableColumnVirtualization = true;
            GridBasicInformation.Items.Clear();
            GridBasicInformation.CanUserAddRows = false;
            GridBasicInformation.ItemsSource = db.
                Database.
                SqlQuery<BasicInformation>((new SqlBasicInformation()).SQl_BasicInformation).
                ToList();
            GridBasicInformation.EnableColumnVirtualization = true;
            GridBasicInformation.Items.Refresh();
            var queryAllDepartamentsNames = from cust in db.C_Departments
                where cust.ID != 1
                select new {cust.Name};
            var listDepInfo = new List<DepartamentsData>();

            foreach (var depName in queryAllDepartamentsNames.ToList())
            {
                var temp = getDepartamentClientData(depName.Name);
                listDepInfo.Add(temp);
            }

            GridMonthlyReport.ItemsSource = new List<TimeTableItem>();
            GridDepartamentReport.ItemsSource = listDepInfo;
            GridDepartamentReport.Items.Refresh();
            SelectedDepartamentReport_MouseLeftButtonUp(null, null);
            SelectedMonthlyReport_MouseLeftButtonUp(null, null);
            SelectedBasicInformation_MouseLeftButtonUp(null, null);
        }

        public ReportPage(int selected) : this()
        {
            SelectedId = selected;
        }

        private ReportPageHelper reportHelper = new ReportPageHelper();
        private int? SelectedId;
        private M_Users SelectedUser;
        private bool _isSearch;
        public CalendarType CalendarTypeSetting { get; set; }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(SelectedBasicInformation, e);
            ToolTipService.SetIsEnabled(SelectedMonthlyReport, e);
            ToolTipService.SetIsEnabled(SelectedDepartamentReport, e);
            ToolTipService.SetIsEnabled(SelectedAllTime, e);
            ToolTipService.SetIsEnabled(GenerationExcel, e);
            ToolTipService.SetIsEnabled(CalendarWeekend, e);
        }

        private DepartamentsData getDepartamentClientData(string name)
        {
            var db = new RfidContext();

            var queryAllDepartaments = from cust in db.C_Names
                where cust.P_Users.P_Departments.Name.Equals(name)
                select new {cust.P_Users.IsUser, cust.P_Users.isInside};
            var t1 = queryAllDepartaments.Count();
            var t2 = queryAllDepartaments.Count(x => x.isInside);
            var t3 = queryAllDepartaments.Count(x => x.isInside == false);

            var date = DateTime.Now;

            var departParam = new DepartamentsData(date.ToString("dd.MM.yyyy"), name, t1, t2, t3);

            return departParam;
        }

        public void HideGrids()
        {
            ViewBasicInformation.Visibility = Visibility.Collapsed;
            ViewMonthlyReport.Visibility = Visibility.Collapsed;
            ViewDepartamentReport.Visibility = Visibility.Collapsed;
            GridAllTime.Visibility = Visibility.Collapsed;
            SVGenerationExcel.Visibility = Visibility.Collapsed;
            AllExcelFiles.Visibility = Visibility.Collapsed;
            CalendarAndWeekend.Visibility = Visibility.Collapsed;
        }

        private void CalendarWeekend_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HideGrids();
            CalendarAndWeekend.Visibility = Visibility.Visible;
            var p = new CalendarAndWeekendPage();
            CalendarFrame.Navigate(p);
            p.SetInfoAboutSelectedUser(SelectedId, SelectedUser);
            Singelton.MainWindow.ChangeStringAddres(Title + " > " + Application.Current.Resources[StringsMapper.Holidays]);
        }

        private void GridBasicInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridBasicInformation.SelectedIndex == -1)
            {
                return;
            }

            if (!_isSearch)
            {
                dynamic s = GridBasicInformation.CurrentCell.Item;
                SelectedId = s.ID;
            }

            SelectedUser = reportHelper.GetCurrentUser(SelectedId, GridMonthlyReport, GridBasicInformation);
        }

        private void ClickPhotoUser(object sender, MouseButtonEventArgs e)
        {
            dynamic s = GridBasicInformation.CurrentCell.Item;

            if (s == DependencyProperty.UnsetValue)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserNSel], (string)Application.Current.Resources[StringsMapper.ErrorHeader],
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int id = s.ID;
                var p = new AboutUserPage(id);
                NavigationService.Navigate(p);
            }
        }

        private void buttonAllTime_Click(object sender, RoutedEventArgs e)
        {
            HideGrids();
            AllDatePage p;

            if (SelectedId == null)
            {
                p = new AllDatePage();
            }
            else
            {
                p = new AllDatePage(SelectedId);
            }

            GridAllTime.NavigationService.Navigate(p);
            GridAllTime.Visibility = Visibility.Visible;
            Singelton.MainWindow.ChangeStringAddres(Title + " > " + Application.Current.Resources[StringsMapper.HoursWorked]);
        }

        private void GridBasicInformation_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Start")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm";
            }
            if (e.Column.Header.ToString() == "End")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm";
            }
            if (e.Column.Header.ToString() == "Valid")
            {
                e.Column.ClipboardContentBinding.StringFormat = "00:mm";
            }
        }

        private void SelectedBasicInformation_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.MainInfo]);
            HideGrids();
            ViewBasicInformation.Visibility = Visibility.Visible;
        }

        private void SelectedMonthlyReport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " +
                                                    Application.Current.Resources[StringsMapper.MonthReport]);
            HideGrids();
            ViewMonthlyReport.Visibility = Visibility.Visible;
        }

        private void SelectedDepartamentReport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Singelton.MainWindow.ChangeStringAddres(Title + " > " + Application.Current.Resources[StringsMapper.DepartmentReport]);
            HideGrids();
            ViewDepartamentReport.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            NameUser.Text = Application.Current.Resources[StringsMapper.FirstName] +
                            " " + Application.Current.Resources[StringsMapper.SecondName];
            Singelton.MainWindow.ChangeStringAddres(Title);
        }

        private void GridMonthlyReport_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "SingleDate")
            {
                e.Column.ClipboardContentBinding.StringFormat = "d/M/yyyy";
            }
            if (e.Column.Header.ToString() == "TimeIn")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm:ss";
            }
            if (e.Column.Header.ToString() == "TimeOut")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm:ss";
            }
            if (e.Column.Header.ToString() == "LengthOfInside")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm:ss";
            }
            if (e.Column.Header.ToString() == "LengthOfOutside")
            {
                e.Column.ClipboardContentBinding.StringFormat = "HH:mm:ss";
            }
        }

        private void UnselecctButton_Click(object sender, RoutedEventArgs e)
        {
            GridBasicInformation.SelectedIndex = -1;
            SelectedId = null;
            NameUser.Text = Application.Current.Resources[StringsMapper.FirstName] +
                            " " + Application.Current.Resources[StringsMapper.SecondName];
        }

        private void GenerationExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HideGrids();
            SVGenerationExcel.Visibility = Visibility.Visible;
            Singelton.MainWindow.ChangeStringAddres(Title + " > " + Application.Current.Resources[StringsMapper.Excel]);
            var p = new GenerationAndSendingExcel();
            p.SetGrids(GridBasicInformation, GridMonthlyReport, GridDepartamentReport);
            SVGenerationExcel.Navigate(p);
        }

        private void GridBasicInformation_AutoGeneratedColumns(object sender, EventArgs e)
        {
            GridBasicInformation.Columns[1].Header = Application.Current.Resources[StringsMapper.FirstName];
            GridBasicInformation.Columns[2].Header = Application.Current.Resources[StringsMapper.SecondName];
            GridBasicInformation.Columns[3].Header = Application.Current.Resources[StringsMapper.ThirdName];
            GridBasicInformation.Columns[4].Header = Application.Current.Resources[StringsMapper.DepartmentName];
            GridBasicInformation.Columns[5].Header = Application.Current.Resources[StringsMapper.StartTime];
            GridBasicInformation.Columns[6].Header = Application.Current.Resources[StringsMapper.EndTime];
            GridBasicInformation.Columns[7].Header = Application.Current.Resources[StringsMapper.TimeLate];
            GridBasicInformation.Columns[8].Header = Application.Current.Resources[StringsMapper.DinnerTime];
            GridBasicInformation.Columns[8].ClipboardContentBinding.StringFormat = "HH:mm";
            if (SelectedId != null)
            {
                foreach (var item in GridBasicInformation.Items)
                {
                    dynamic temp = item;

                    if (temp.ID == SelectedId)
                    {
                        _isSearch = true;
                        GridBasicInformation.SelectedItem = item;
                    }
                }
            }
        }

        private void GridMonthlyReport_AutoGeneratedColumns(object sender, EventArgs e)
        {
            GridMonthlyReport.Columns[1].Header = Application.Current.Resources[StringsMapper.Date];
            GridMonthlyReport.Columns[2].Header = Application.Current.Resources[StringsMapper.Day];
            GridMonthlyReport.Columns[3].Header = Application.Current.Resources[StringsMapper.ComeIn];
            GridMonthlyReport.Columns[4].Header = Application.Current.Resources[StringsMapper.ComeOut];
            GridMonthlyReport.Columns[5].Header = Application.Current.Resources[StringsMapper.TimeIn];
            GridMonthlyReport.Columns[6].Header = Application.Current.Resources[StringsMapper.TimeOut];
        }

        private void GridDepartamentReport_AutoGeneratedColumns(object sender, EventArgs e)
        {
            GridDepartamentReport.Columns[0].Header = Application.Current.Resources[StringsMapper.Date];
            GridDepartamentReport.Columns[1].Header = Application.Current.Resources[StringsMapper.DepartmentName];
            GridDepartamentReport.Columns[2].Header = Application.Current.Resources[StringsMapper.Count];
            GridDepartamentReport.Columns[3].Header = Application.Current.Resources[StringsMapper.InCount];
            GridDepartamentReport.Columns[4].Header = Application.Current.Resources[StringsMapper.OutCount];
        }
    }
}
