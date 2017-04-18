using Rfid.Context;
using Rfid.Helpers;
using System.Windows;
using System.Windows.Controls;


namespace Rfid
{
    /// <summary>
    /// Interaction logic for UserInfo_Find.xaml
    /// </summary>
    public partial class FindUserPage
    {
        public FindUserPage()
        {        
            InitializeComponent();
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(Button, e);
            ToolTipService.SetIsEnabled(ClearButton, e);
        }
        
        private void Button_FindUser(object sender, RoutedEventArgs e)
        {
            using (var rfid = new RfidContext())
            {
                var find = new UserFinderHelper()
                {
                    FirstName = TB_User_FirstName.Text,
                    SecondName = TB_User_SecondName.Text,
                    ThirdName = TB_User_ThirdName.Text,
                    PhoneNumber = TB_User_PhoneNumber.Text,
                    DepartmentName = TB_User_DepartmentName.Text,
                    DepartmentDirector = TB_User_DepartmentDirector.Text,
                    Address = TB_User_Address.Text,
                    Context = rfid
                };

                if (!find.TryFind())
                {
                    NotFoundMessage.Visibility = Visibility.Visible;
                    return;
                }    
                      
            var p = new FindResultPage(find);
            Singelton.Frame.Navigate(p);
            }
           
           
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            NotFoundMessage.Visibility = Visibility.Collapsed;
            Singelton.MainWindow.ChangeStringAddres(Title);
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TB_User_FirstName.Text = string.Empty;
            TB_User_SecondName.Text = string.Empty;
            TB_User_ThirdName.Text = string.Empty;
            TB_User_PhoneNumber.Text = string.Empty;
            TB_User_DepartmentName.Text = string.Empty;
            TB_User_DepartmentDirector.Text = string.Empty;
            TB_User_Address.Text = string.Empty;
        }
    }
}
