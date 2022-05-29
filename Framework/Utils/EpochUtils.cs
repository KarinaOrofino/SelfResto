using System;

namespace RHSP.Framework.Utils
{
    public static class EpochUtils
    {
        public static long ConvertToEpoch(DateTime date)
        {
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;

            return secondsSinceEpoch;
        }

        public static DateTime ConvertFromEpoch(string timeStamp)
        {
            long timestamp = long.Parse(timeStamp);
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
            start = start.AddSeconds(timestamp); //add the seconds to the start DateTime

            return start;
        }
    }
}
