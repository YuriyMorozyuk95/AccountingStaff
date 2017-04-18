using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace Rfid
{
    public partial class StartUpPage : IProvideAccess
    {
        public StartUpPage()
        {
            InitializeComponent();       
            Title = "Rfid";
        }

        public static string[] CheckedPort = SerialPort.GetPortNames();

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(FindUserTile, e);
            ToolTipService.SetIsEnabled(AddUserTile, e);
            ToolTipService.SetIsEnabled(BigInfoTile, e);
            ToolTipService.SetIsEnabled(SettingTile, e);
            ToolTipService.SetIsEnabled(AuthorizeAdmin, e);
            ToolTipService.SetIsEnabled(StartTile, e);
            ToolTipService.SetIsEnabled(LogOutTile, e);
        }

        public void ProvideAccess()
        {
            if(Singelton.IsAthorizationAdmin)
            {
                FindUserTile.Visibility = Visibility.Visible;
                AddUserTile.Visibility = Visibility.Visible;
                BigInfoTile.Visibility = Visibility.Visible;
                SettingTile.Visibility = Visibility.Visible;
                LogOutTile.Visibility = Visibility.Visible;
                AuthorizeAdmin.Visibility = Visibility.Collapsed;
            }
            else if (Singelton.IsAuthorizationAccountant)
            {
                FindUserTile.Visibility =  Visibility.Visible;
                LogOutTile.Visibility = Visibility.Visible;
                BigInfoTile.Visibility = Visibility.Visible;
            }
            else
            {
                FindUserTile.Visibility = Visibility.Collapsed;
                AddUserTile.Visibility = Visibility.Collapsed;
                BigInfoTile.Visibility = Visibility.Collapsed;
                SettingTile.Visibility = Visibility.Collapsed;
                LogOutTile.Visibility = Visibility.Collapsed;
                AuthorizeAdmin.Visibility = Visibility.Visible;
            }            
        }

        private void Button_User_Find(object sender, RoutedEventArgs e)
        {
            var p = new FindUserPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }
        private void Button_User_Add(object sender, RoutedEventArgs e)
        {
            var p = new AddOrUpdateUserPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }
        private void Button_RfidStart(object sender, RoutedEventArgs e)
        {
            var p = new GetRFIDIPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }
        private void Button_BigInfo(object sender, RoutedEventArgs e)
        {
            var p = new ReportPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }

        private void SettingTile_Click(object sender, RoutedEventArgs e)
        {
            var p = new SettingPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
            ProvideAccess();
        }
        private void AuthorizeAdmin_Click(object sender, RoutedEventArgs e)
        {
            var p = new AuthorizeAdminPage();
            Singelton.Frame.Navigate(p);
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Singelton.AuthorizedUser = null;
            ProvideAccess();
        }
    }

}
