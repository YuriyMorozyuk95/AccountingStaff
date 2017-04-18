using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Rfid.Context;

namespace Rfid
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            try
            {
                db.C_Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            InitializeComponent();
            var s = new Singelton();
            Singelton.Frame = RfitFrame;
            Singelton.MainWindow = this;
            db.C_Setting.Single().ImportInBdSetting();
            Singelton.WatcherSetting.StartWatching();
            db.SaveChanges();
            var p = new StartUpPage();
            Singelton.Frame.NavigationService.Navigate(p);
        }

        private readonly RfidContext db = new RfidContext();

        private bool IsOpenFlayout
        {
            get { return SettingThemeFlyout.IsOpen; }
            set { SettingThemeFlyout.IsOpen = value; }
        }

        public void OpenFlayout()
        {
            IsOpenFlayout = !IsOpenFlayout;
        }

        public void SaveSetting()
        {
            db.C_Setting.SingleOrDefault().ExportInBdSetting();
            db.SaveChanges();
        }

        public void ChangeStringAddres(string Addres)
        {
            if (Addres?.Count() > 45)
            {
                Addres = Addres.Remove(45) + "...";
            }

            TitleAdress.Content = Addres;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            Singelton.Frame.NavigationService.Navigate(new StartUpPage());
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (Singelton.Frame.CanGoForward)
            {
                Singelton.Frame.GoForward();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Singelton.MainWindow.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Singelton.Frame.CanGoBack)
            {
                Singelton.Frame.GoBack();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaxRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MaxRestoreButton.Content = char.ConvertFromUtf32(0xE922);
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaxRestoreButton.Content = char.ConvertFromUtf32(0xE923);
            }
        }

        private void RedTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Red;
            SaveSetting();
        }

        private void GreenTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Green;
            SaveSetting();
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Blue;
            SaveSetting();
        }

        private void PurpleTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Purple;
            SaveSetting();
        }

        private void OrangeTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Orange;
            SaveSetting();
        }

        private void LimeTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Lime;
            SaveSetting();
        }

        private void TealTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Teal;
            SaveSetting();
        }

        private void CyanTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Cyan;
            SaveSetting();
        }

        private void IndigoTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Indigo;
            SaveSetting();
        }

        private void VioletTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Violet;
            SaveSetting();
        }

        private void PinkTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Pink;
            SaveSetting();
        }

        private void MagentaTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Magenta;
            SaveSetting();
        }

        private void CrimsonTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Crimson;
            SaveSetting();
        }

        private void YellowTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Yellow;
            SaveSetting();
        }

        private void BrownTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Brown;
            SaveSetting();
        }

        private void OliveTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Olive;
            SaveSetting();
        }

        private void SiennaTheme_Click(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppAccent = AccentTheme.Sienna;
            SaveSetting();
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppTheme = AppTheme.BaseDark;
            SaveSetting();
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            AppColors.CurrentAppTheme = AppTheme.BaseLight;
            SaveSetting();
        }

        private void ToolTipSetting_Checked(object sender, RoutedEventArgs e)
        {
            Singelton.IsToolTipEnabled = true;
            Singelton.Frame.Navigate(new SettingPage());
        }

        private void ToolTipSetting_Unchecked(object sender, RoutedEventArgs e)
        {
            Singelton.IsToolTipEnabled = false;
            Singelton.Frame.Navigate(new SettingPage());
        }
    }
}
