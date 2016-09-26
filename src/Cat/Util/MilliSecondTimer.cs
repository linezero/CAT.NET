using System;

namespace Com.Dianping.Cat.Util
{
    ///<summary>
    ///  This timer provides milli-second precise system time.
    ///</summary>
    public class MilliSecondTimer
    {
        public static long CurrentTimeMicros()
        {
            //return HighResTicksProvider.GetTickCount () / 10L; // it's microsecond precise
            return DateTime.Now.Ticks/10L; // it's millisecond precise
        }

        public static long CurrentTimeHoursForJava()
        {
            DateTime baseline = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - baseline.Ticks);

            return ((long) ts.TotalMilliseconds/3600000L);
        }
    }
}