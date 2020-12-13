using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDataViewer.Utilities
{
    public static class Helper
    {
        public static decimal GetPriceDecimal(this string str)
        {
            decimal ret = 0;
            if (string.IsNullOrEmpty(str)) return ret;
            string strValue =  new string(new string(str.ToCharArray()
                .Where(c => c >= 48 && c <= 57 || c == 46).ToArray()));
            if (string.IsNullOrEmpty(strValue)) return ret;
            decimal.TryParse(strValue, out ret);
           return ret;
        }

        public static DateTime GetFormatedDate(this string str,string format)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
             DateTime.TryParseExact(str,
                         format,
                         CultureInfo.InvariantCulture,
                         DateTimeStyles.None,
                         out DateTime dt);
            return dt;
        }

    }
}
