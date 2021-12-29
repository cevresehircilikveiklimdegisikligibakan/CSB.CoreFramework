using System;

namespace CSB.Core.Utilities.Caching
{
    public record CacheDownOptions
    {
        public int Wait1st { get; set; }
        public int Wait2nd { get; set; }
        public int Wait3rd { get; set; }
        public int Wait4th { get; set; }

        public int GetNext(int? lastWaitTime)
        {
            if (lastWaitTime.HasValue == false)
                lastWaitTime = 0;
            return GetNext(lastWaitTime.Value);
        }
        public int GetWaitTimeShouldBe(DateTime time)
        {
            int difference = Convert.ToInt32((DateTime.Now - time).TotalSeconds);
            if (difference < 0)
                difference = 0;
            int waitTime = GetWaitTimeShouldBe(difference);
            return waitTime;
        }
        private int GetWaitTimeShouldBe(int difference)
        {
            if (difference <= 0)
                return 0;
            else if (difference < Wait1st)
                return Wait1st;
            else if (difference < Wait2nd)
                return Wait2nd;
            else if (difference < Wait3rd)
                return Wait3rd;
            else if (difference < Wait4th)
                return Wait4th;
            else
                return Wait4th;
        }
        private int GetNext(int waitTime)
        {
            if (waitTime == 0)
                return Wait1st;
            else if (waitTime == Wait1st)
                return Wait2nd;
            else if (waitTime == Wait2nd)
                return Wait3rd;
            else if (waitTime == Wait3rd)
                return Wait4th;
            else if (waitTime == Wait4th)
                return Wait4th;
            else
                return Wait1st;
        }
    }
}