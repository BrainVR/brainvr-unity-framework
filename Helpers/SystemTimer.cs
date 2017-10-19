using System;

namespace BrainVR.UnityFramework.Helpers
{
    public static class SystemTimer
    {
        private static readonly DateTime MStartTime;
        private static readonly DateTime Midnight = DateTime.Now.Date;

        static SystemTimer()
        {
            MStartTime = DateTime.Now;
        }

        public static float RealtimeSinceStartup
        {
            get
            {
                var timeSpan = DateTime.Now.Subtract(MStartTime);
                return (float)timeSpan.TotalSeconds;
            }
        }

        public static double TimeSinceMidnight
        {
            get
            {
                var timeSpan = DateTime.Now.Subtract(Midnight);
                return timeSpan.TotalSeconds;
            }
        }
    }
}
