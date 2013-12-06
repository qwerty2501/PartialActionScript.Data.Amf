using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal static class DateTimeOffsetExtention
    {
        internal static double ToUnixTime(this DateTimeOffset self)
        {
            return (self - unixEpoch).TotalMilliseconds;
        }

        internal static DateTimeOffset FromUnixTime(double unixTime)
        {
            return unixEpoch.AddMilliseconds(unixTime);
        }


        private static readonly DateTimeOffset unixEpoch = new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }
}
