using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Helpers.PageHelpers;
using Rfid.Models;
using Rfid.Setting;
using Rfid.Sql;

namespace Rfid
{
    /// <summary>
    ///     Interaction logic for GetRFIDIdWindow.xaml
    /// </summary>
    public partial class GetRFIDIPage
    {
        public LimitInsideSetting Watcher { get; set; }
        public string RfidNumber { get; private set; }
        public M_Users AuthorithationUser { get; private set; }
        private readonly GetRfidHelper _rfidHelper = new GetRfidHelper();

        public GetRFIDIPage()
        {
            InitializeComponent();
            Watcher = Singelton.WatcherSetting;
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(BOK, e);
        }

        private void BOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RfidNumber = TBGetRfidId.Text;
                var db = new RfidContext();
                var s = Convert.ToInt64(RfidNumber);
                var user =
                    db.Database.SqlQuery<M_Users>(new SqlSearchUserForRfid().SQl_SearchUserForRfid, s).SingleOrDefault();
                var dsd = db.C_Users.Single(z => z.ID == user.ID);
                AuthorithationUser = dsd;
                _rfidHelper.WriteById(dsd);
                db.SaveChanges();
                var p = new AboutUserPage(AuthorithationUser.ID);

                Singelton.Frame.NavigationService.Navigate(p);
            }
            catch
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorEnterRfid],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader], MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
            Keyboard.Focus(TBGetRfidId);
        }

        private void TBGetRfidId_TextChanged(object sender, TextChangedEventArgs e)
        {
            BOK.IsEnabled = Regex.Match(TBGetRfidId.Text, StringsMapper.RfidRegExp).Success;
        }
    }
}

