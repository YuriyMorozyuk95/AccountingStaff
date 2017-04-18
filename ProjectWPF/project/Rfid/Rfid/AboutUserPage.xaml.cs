using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Models;

namespace Rfid
{
    /// <summary>
    ///     Interaction logic for AboutUser.xaml
    /// </summary>
    public partial class AboutUserPage : IProvideAccess
    {
        private readonly int _id;
        private bool _isInside;
        private readonly RfidContext _db;
        private readonly AboutUserHelper _aboutHelper = new AboutUserHelper();

        public bool IsInside
        {
            get { return _isInside; }
            set
            {
                if (value)
                {
                    StatusGrid.Background = new SolidColorBrush(Colors.Green);
                    StatusValue.Text = Singelton.LanguageSetting.GetString(StringsMapper.InOfficeStatus);
                    ((TextBlock) ((StackPanel) ChangeStatus.Content).Children[0]).Text =
                        Application.Current.Resources[StringsMapper.SetEmployeeOut].ToString();
                }
                else
                {
                    StatusGrid.Background = new SolidColorBrush(Colors.Red);
                    StatusValue.Text = Singelton.LanguageSetting.GetString(StringsMapper.OutOfficeStatus);
                    ((TextBlock) ((StackPanel) ChangeStatus.Content).Children[0]).Text =
                        Application.Current.Resources[StringsMapper.SetEmployeeIn].ToString();
                }

                _isInside = value;
            }
        }

        public M_Users CurrentUser { get; }

        public AboutUserPage(int id)
        {
            InitializeComponent();
            _db = new RfidContext();
            _id = id;
            CurrentUser = new M_Users();
            CurrentUser = _db.C_Users.Single(z => z.ID == id);
            IsInside = CurrentUser.isInside;
            _aboutHelper.Run(id, _db, CurrentUser, AboutUserGrid);
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(StatusGrid, e);
            ToolTipService.SetIsEnabled(button, e);
            ToolTipService.SetIsEnabled(EditUserButton, e);
            ToolTipService.SetIsEnabled(GoToReportButton, e);
            ToolTipService.SetIsEnabled(AddCommentExpander, e);
            ToolTipService.SetIsEnabled(ChangeStatus, e);
        }

        public void ProvideAccess()
        {
            EditUserButton.Visibility = Singelton.IsAthorizationAdmin ? Visibility.Visible : Visibility.Collapsed;
            button.Visibility = Singelton.IsAthorizationAdmin ? Visibility.Visible : Visibility.Collapsed;
            GoToReportButton.Visibility = Singelton.IsAthorizationAdmin ? Visibility.Visible : Visibility.Collapsed;
            ChangeStatus.Visibility = Singelton.IsAthorizationAdmin ? Visibility.Visible : Visibility.Collapsed;
        }


        private void ClickLinkingButton(object sender, RoutedEventArgs e)
        {
            var p = new LinkingRfidPage(_id);
            NavigationService.Navigate(p);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
            ProvideAccess();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new AddOrUpdateUserPage(_id);
            NavigationService.Navigate(p);
        }

        private void GoToReportButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new ReportPage(_id);
            Singelton.Frame.Navigate(p);
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show(((TextBlock) ((StackPanel) ChangeStatus.Content).Children[0]).Text + " ?", "",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _aboutHelper.ChangeStatus(CurrentUser);
                _db.SaveChanges();
                IsInside = CurrentUser.isInside;
            }
        }
    }
}
