using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Utils.Extentions
{
    public static class DatetimeExtentions
    {
        public const string StandardShortDateTimeFormat = "yyyy-MM-dd";

        public static string ToStringStandard(this DateTime? date)
        {
            if (!date.HasValue)
                return null;

            return ToStringStandard(date.Value);
        }

        public static string ToStringStandard(this DateTime date)
        {
            return date.ToString(StandardShortDateTimeFormat);
        }
    }
}