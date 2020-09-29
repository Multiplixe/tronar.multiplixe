using System;
using System.Runtime.CompilerServices;

namespace adduo.helper.extensionmethods
{
    public static class DateTimeExtensionMethod
    {
        public static DateTime AjustNow(this DateTime dt, int hoursAjust = 4)
        {
            return dt.AddHours(hoursAjust);
        }

        public static string ToMySQL(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        }

    }
}
