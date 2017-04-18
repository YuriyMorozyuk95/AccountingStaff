using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingStaff.Data.Models
{
    //Delete
    public class M_SettingEmail : M_Base
    {
        public string MailFrom { get; set; }
        public string Password { get; set; }

        //public string PathAttachmentFile { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }
        //public string SmtpClient { get; set; }

        public virtual M_Setting Setting { get; set; }
    }
}
