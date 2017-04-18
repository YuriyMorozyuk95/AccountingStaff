using System;
using System.Windows;
using System.Windows.Controls;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Helpers.PageHelpers;

namespace Rfid
{
    public partial class AllDatePage
    {
        private readonly int? _id;
        private readonly AllDateHelper _dateHelper = new AllDateHelper();

        public AllDatePage(int? a = null)
        {
            InitializeComponent();

            _id = a;

            labelWeekInside.Content = Application.Current.Resources[StringsMapper.Week] +
                                      _dateHelper.WeekMonthYearInside(DateTime.Now.AddDays(-7), DateTime.Now, _id);
            labelMonthInside.Content = Application.Current.Resources[StringsMapper.Month] +
                                       _dateHelper.WeekMonthYearInside(DateTime.Now.AddMonths(-1), DateTime.Now, _id);
            labelYearInside.Content = Application.Current.Resources[StringsMapper.Year] +
                                      _dateHelper.WeekMonthYearInside(DateTime.Now.AddYears(-1), DateTime.Now, _id);

            labelWeekOutside.Content = Application.Current.Resources[StringsMapper.Week] +
                                       _dateHelper.WeekMonthYearOutside(DateTime.Now.AddDays(-7), DateTime.Now, _id);
            labelMonthOutside.Content = Application.Current.Resources[StringsMapper.Month] +
                                        _dateHelper.WeekMonthYearOutside(DateTime.Now.AddMonths(-1), DateTime.Now, _id);
            labelYearOutside.Content = Application.Current.Resources[StringsMapper.Year] +
                                       _dateHelper.WeekMonthYearOutside(DateTime.Now.AddYears(-1), DateTime.Now, _id);
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(buttonGetData, e);
        }

        private void buttonGetData_Click(object sender, RoutedEventArgs e)
        {
            var db = new RfidContext();
            try
            {
                _dateHelper.GetData(CalendarFrom, CalendarTo, _id, db, labelSelectedTimeInside, labelSelectedTimeOutside);
            }
            catch (Exception)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorNoDateSel],
                    (string)Application.Current.Resources[StringsMapper.ErrorHeader],
            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
        }
    }
}
