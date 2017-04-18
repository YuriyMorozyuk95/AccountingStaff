using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace AccountingStaff.Web.Helpers
{
    public static class EnumHelpers
    {
        public static string GetName(this Enum e)
        {
            var attributes = (DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.FirstOrDefault().Name;
        }
    }
}