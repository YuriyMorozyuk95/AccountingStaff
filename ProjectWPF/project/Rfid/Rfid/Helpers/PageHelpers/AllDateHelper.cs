using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Rfid.Context;

namespace Rfid.Helpers.PageHelpers
{
    internal class AllDateHelper
    {
        public string WeekMonthYearInside(DateTime dateFrom, DateTime dateTo, int? id)
        {
            var db = new RfidContext();
            try
            {
                IQueryable<dynamic> queryAllDepartaments;
                if (id == null)
                {
                    queryAllDepartaments = from cust in db.C_UserTime
                        where cust.SingleDate >= dateFrom && cust.SingleDate < dateTo
                        select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
                }
                else
                {
                    queryAllDepartaments = from cust in db.C_UserTime
                        where cust.P_Users.ID == id && cust.SingleDate >= dateFrom && cust.SingleDate < dateTo
                        select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
                }

                var sumDate = DateTime.MinValue;

                foreach (var temp in queryAllDepartaments)
                {
                    if (temp.LengthOfInside != null)
                    {
                        sumDate = sumDate.Add(((DateTime?) temp.LengthOfInside).Value.TimeOfDay);
                    }
                }
                var timeRes = new TimeSpan(sumDate.Ticks);
                return $" {Math.Truncate(timeRes.TotalHours):####}h {timeRes.Minutes}m";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            return string.Empty;
        }

        public string WeekMonthYearOutside(DateTime dateFrom, DateTime dateTo, int? id)
        {
            var db = new RfidContext();
            try
            {
                IQueryable<dynamic> queryAllDepartaments;
                if (id == null)
                {
                    queryAllDepartaments = from cust in db.C_UserTime
                        where cust.SingleDate >= dateFrom && cust.SingleDate < dateTo
                        select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
                }
                else
                {
                    queryAllDepartaments = from cust in db.C_UserTime
                        where cust.P_Users.ID == id && cust.SingleDate >= dateFrom && cust.SingleDate < dateTo
                        select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
                }
                var sumDate = DateTime.MinValue;

                foreach (var temp in queryAllDepartaments)
                {
                    if (temp.LengthOfOutside != null)
                    {
                        sumDate = sumDate.Add(((DateTime?) temp.LengthOfOutside).Value.TimeOfDay);
                    }
                }
                var timeRes = new TimeSpan(sumDate.Ticks);
                return $" {Math.Truncate(timeRes.TotalHours):####}h {timeRes.Minutes}m";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
                return "Exception";
            }
        }

        public void GetData(Calendar calendarFrom, Calendar calendarTo, int? id, RfidContext db, Label timeInsideLb,
            Label timeOutsideLb)
        {
            var dateFrom = calendarFrom.SelectedDate.Value;
            var dateTo = calendarTo.SelectedDate.Value;

            IQueryable<dynamic> queryAllDepartaments;
            if (id == null)
            {
                queryAllDepartaments = from cust in db.C_UserTime
                    where cust.SingleDate >= dateFrom && cust.SingleDate <= dateTo
                    select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
            }
            else
            {
                queryAllDepartaments = from cust in db.C_UserTime
                    where cust.P_Users.ID == id && cust.SingleDate >= dateFrom && cust.SingleDate <= dateTo
                    select new {cust.SingleDate, cust.LengthOfInside, cust.LengthOfOutside};
            }

            var sumDateInside = DateTime.MinValue;
            var sumDateOutside = DateTime.MinValue;

            foreach (var temp in queryAllDepartaments)
            {
                if (temp.LengthOfInside != null)
                    sumDateInside = sumDateInside.Add(((DateTime?) temp.LengthOfInside).Value.TimeOfDay);

                if (temp.LengthOfOutside != null)
                    sumDateOutside = sumDateOutside.Add(((DateTime?) temp.LengthOfOutside).Value.TimeOfDay);
            }

            var timeInside = new TimeSpan(sumDateInside.Ticks);
            var timeOutside = new TimeSpan(sumDateOutside.Ticks);
            timeInsideLb.Content = Application.Current.Resources[StringsMapper.TimePeriod] +
                                   $" {Math.Truncate(timeInside.TotalHours):####}h {timeInside.Minutes}m";
            timeOutsideLb.Content = Application.Current.Resources[StringsMapper.TimePeriod] +
                                    $" {Math.Truncate(timeOutside.TotalHours):####}h {timeOutside.Minutes}m";
        }
    }
}
