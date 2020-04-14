using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Extensions
{
    public static class Extensions
    {
        public static string eGetDaySuffix(this DateTime dateTime)
        {
            int day = dateTime.Day;
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        public static string eGetLongDateWithSuffixAsHtml(this DateTime dateTime)
        {
            return dateTime.ToString($"d'<sup>{dateTime.eGetDaySuffix()}</sup>' MMMM yyyy");
        }
    }
}
