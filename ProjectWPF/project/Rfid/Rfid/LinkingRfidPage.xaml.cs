using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Rfid.Context;
using Rfid.Helpers;
using Rfid.Models;

namespace Rfid
{
    public partial class LinkingRfidPage
    {
        public LinkingRfidPage(int Id)
        {
            InitializeComponent();
            ID = Id;
            run(Id);
        }

        private readonly int ID;

        public string RFIDCodeTagsWriter
        {
            set { RFIDCodeTags.Text = value; }
        }

        private void run(int Id)
        {
            var db = new RfidContext();
            var user = new M_Users();
            user = db.C_Users.Single(z => z.ID == Id);
            var prfids = new List<M_Rfids>();
            prfids = user.P_Rfids.ToList();
            var rfideCode = prfids.LastOrDefault(x => x.IsArhive == false);

            if (prfids.LastOrDefault(x => x.IsArhive == false) != null)
            {
                RFIDCodeTags.Text = rfideCode.RfidID.ToString();
            }
            else
            {
                RFIDCodeTags.Text = "";
            }

            var queryAllCustomers = from cust in db.C_Rfids
                where cust.IsArhive && cust.P_Users.ID == Id
                select new {cust.Date, cust.RfidID, cust.Comment};

            LAadded.Text = user.P_Rfids.ToList()[0].Date.ToShortDateString();

            if (queryAllCustomers.Any())
            {
                GridRegistryChangesRFIDtags.ItemsSource = queryAllCustomers.ToList();
            }

            if (GridRegistryChangesRFIDtags.Columns.Count > 0)
            {
                GridRegistryChangesRFIDtags.Columns[0].ClipboardContentBinding.StringFormat = "dd.MM.yyyy";
            }

            GridRegistryChangesRFIDtags.Items.Refresh();

            if (GridRegistryChangesRFIDtags.Columns.Count != 0)
            {
                GridRegistryChangesRFIDtags.Columns[0].Header =
                    Singelton.LanguageSetting.GetString(StringsMapper.AddingDate);
                GridRegistryChangesRFIDtags.Columns[1].Header =
                    Singelton.LanguageSetting.GetString(StringsMapper.RfidNum);
                GridRegistryChangesRFIDtags.Columns[2].Header =
                    Singelton.LanguageSetting.GetString(StringsMapper.Comment);
            }
        }

        public void SetEnableToolTip()
        {
            var e = Singelton.IsToolTipEnabled;
            ToolTipService.SetIsEnabled(BUnlinkRFIDTag, e);
            ToolTipService.SetIsEnabled(SnapRFIDtag, e);
        }

        private void ClickUlinkRFID(object sender, RoutedEventArgs e)
        {
            var db = new RfidContext();
            var usedRfid = db.C_Rfids.Where(z => z.P_Users.ID == ID).SingleOrDefault(x => x.IsArhive == false);

            if (usedRfid != null)
            {
                usedRfid.IsArhive = true;
                usedRfid.Comment = Application.Current.Resources[StringsMapper.Lost].ToString();
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show(Application.Current.Resources[StringsMapper.AllRfidArchive].ToString(),
                    Application.Current.Resources[StringsMapper.ErrorHeader].ToString(), MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            RFIDCodeTags.Text = "";
        }

        private void SnapRFIDTag(object sender, RoutedEventArgs e)
        {
            var p = new WindowSnapRfidPage(ID);
            Singelton.Frame.NavigationService.Navigate(p);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetEnableToolTip();
            Singelton.MainWindow.ChangeStringAddres(Title);
        }
    }
}
