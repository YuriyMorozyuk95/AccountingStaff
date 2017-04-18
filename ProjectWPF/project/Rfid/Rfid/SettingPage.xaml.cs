using System.Windows;
using System.Windows.Controls;

namespace Rfid
{
    public partial class SettingPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(TilePersonalization, e);
            ToolTipService.SetIsEnabled(TileSendingEmail, e);
            ToolTipService.SetIsEnabled(TileExcelSetting, e);
            ToolTipService.SetIsEnabled(TileLimitSetting, e);
            ToolTipService.SetIsEnabled(LanguageSetting, e);

        }

        private void TilePersonalization_Click(object sender, RoutedEventArgs e)
        {
            Singelton.MainWindow.OpenFlayout();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
        }
        private void TileSendingEmail_Click(object sender, RoutedEventArgs e)
        {
            var p = new EmailSettingPage();
            Singelton.Frame.Navigate(p);
        }
        private void TileExcelSetting_Click(object sender, RoutedEventArgs e)
        {
            var p = new ExcelSettingPage();
            Singelton.Frame.Navigate(p);
        }
        private void TileLimitSetting_Click(object sender, RoutedEventArgs e)
        {
            var p = new LimitSetting();
            Singelton.Frame.Navigate(p);
        }
        private void LanguageSetting_Click(object sender, RoutedEventArgs e)
        {
            var p = new LanguageSettingPage();
            Singelton.Frame.Navigate(p);
        }
    }
}
