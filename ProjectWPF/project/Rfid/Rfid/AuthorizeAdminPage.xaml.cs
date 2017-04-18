using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Models;
using Rfid.Sql;

namespace Rfid
{
    /// <summary>
    ///     Interaction logic for AuthorizeAdminPage.xaml
    /// </summary>
    public partial class AuthorizeAdminPage
    {
        public AuthorizeAdminPage()
        {
            InitializeComponent();
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(Authorize, e);
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new RfidContext())
                {
                    var rfid = Convert.ToInt64(TBGetRfidId.Text);
                    var dsd =
                        db.Database.SqlQuery<M_Users>(new SqlSearchUserForRfid().SQl_SearchUserForRfid, rfid)
                            .SingleOrDefault();
                    Singelton.AuthorizedUser = dsd;
                }
            }
            catch
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.ErrorUserNFound],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader], MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            var p = new StartUpPage();
            Singelton.Frame.Navigate(p);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
            Keyboard.Focus(TBGetRfidId);
        }

        private void TBGetRfidId_TextChanged(object sender, TextChangedEventArgs e)
        {
            Authorize.IsEnabled = Regex.Match(TBGetRfidId.Text, StringsMapper.RfidRegExp).Success;
        }
    }
}
