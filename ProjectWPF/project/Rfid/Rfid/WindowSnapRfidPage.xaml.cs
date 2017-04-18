using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rfid.Helpers;
using Rfid.Helpers.PageHelpers;

namespace Rfid
{
    public partial class WindowSnapRfidPage
    {
        public WindowSnapRfidPage(int id)
        {
            InitializeComponent();
            ID = id;
        }

        private readonly WindowSnapHelper _snapHelper = new WindowSnapHelper();
        public int ID { get; }
        public string RfidNumber { get; private set; }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(button, e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var p = new LinkingRfidPage(ID);

            try
            {
                RfidNumber = TSnapRFID.Text;
                p.RFIDCodeTagsWriter = RfidNumber;
            }
            catch (FormatException)
            {
                MessageBox.Show((string) Application.Current.Resources[StringsMapper.EnterRfid],
                    (string) Application.Current.Resources[StringsMapper.ErrorHeader], MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            _snapHelper.Snap(ID, RfidNumber);
            Singelton.Frame.NavigationService.Navigate(p);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
            Keyboard.Focus(TSnapRFID);
        }

        private void TSnapRFID_TextChanged(object sender, TextChangedEventArgs e)
        {
            button.IsEnabled = Regex.Match(TSnapRFID.Text, StringsMapper.RfidRegExp).Success;
        }
    }
}
