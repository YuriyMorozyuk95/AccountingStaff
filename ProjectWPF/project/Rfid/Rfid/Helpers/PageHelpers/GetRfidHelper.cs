using System;
using System.Linq;
using System.Windows;
using Rfid.Models;

namespace Rfid.Helpers.PageHelpers
{
    internal class GetRfidHelper
    {
        public void WriteById(M_Users dsd)
        {
            try
            {
                if (dsd.isInside == false)
                {
                    TimeSpan? lengthOfInside = new TimeSpan();

                    if (dsd.P_UserTime.Count != 0)
                    {
                        lengthOfInside = DateTime.Now - dsd.P_UserTime.Last().TimeOut;
                        var date = DateTime.Today.Add(lengthOfInside.Value);
                        DateTime? dtOutsige = DateTime.Today.Add(lengthOfInside.Value);
                        dsd.P_UserTime.Last().LengthOfOutside = dsd.P_UserTime.Last().Day ==
                                                                DateTime.Today.DayOfWeek.ToString()
                            ? dtOutsige
                            : null;
                    }

                    dsd.P_UserTime.Add(new M_UserTime {SingleDate = DateTime.Now.Date});
                    dsd.P_UserTime.Last().Day = DateTime.Now.DayOfWeek.ToString();
                    dsd.P_UserTime.Last().TimeIn = DateTime.Now;
                    dsd.isInside = true;
                }
                else
                {
                    dsd.P_UserTime.Last().TimeOut = DateTime.Now;
                    var lengthOfInside = dsd.P_UserTime.Last().TimeOut - dsd.P_UserTime.Last().TimeIn;

                    if (lengthOfInside > Singelton.WatcherSetting.MaxTimeInside)
                    {
                        lengthOfInside = Singelton.WatcherSetting.MaxTimeInside;
                    }

                    DateTime? dtInsige = DateTime.Today.Add(lengthOfInside.Value);
                    dsd.P_UserTime.Last().LengthOfInside = dtInsige;
                    dsd.isInside = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
