using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using Rfid.Context;
using Rfid.Models;

namespace Rfid.Helpers.PageHelpers
{
    class WindowSnapHelper
    {
        public void Snap(int id, string rfidNum)
        {
            try
            {
                var db = new RfidContext();
                var temp = db.C_Rfids.Where(z => z.P_Users.ID == id).Where(x => x.IsArhive == false);
                M_Users user;

                if (temp.Count() != 0)
                {
                    foreach (var rfid in db.C_Rfids.Where(z => z.P_Users.ID == id).Where(x => x.IsArhive == false))
                    {
                        rfid.IsArhive = true;
                    }

                    var addRfid = new M_Rfids
                    {
                        RfidID = Convert.ToInt64(rfidNum),
                        IsArhive = false,
                        Date = DateTime.Now
                    };
                    user = db.C_Users.Single(z => z.ID == id);
                    addRfid.P_Users = user;
                    user.P_Rfids.Add(addRfid);
                }
                else
                {
                    foreach (var rfid in db.C_Rfids.Where(z => z.P_Users.ID == id).Where(x => x.IsArhive == false))
                    {
                        rfid.IsArhive = true;
                    }

                    var addRfid = new M_Rfids
                    {
                        RfidID = Convert.ToInt64(rfidNum),
                        IsArhive = false,
                        Date = DateTime.Now
                    };
                    user = db.C_Users.Single(z => z.ID == id);
                    addRfid.P_Users = user;
                    user.P_Rfids.Add(addRfid);

                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
