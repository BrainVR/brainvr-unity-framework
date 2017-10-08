using System;
using UnityEngine;

namespace BrainVR.UnityLogger
{
    public class SystemTimer : MonoBehaviour
    {

        private static DateTime m_StartTime;
        private static DateTime midnight = DateTime.Now.Date;

        static SystemTimer()
        {
            m_StartTime = DateTime.Now;
        }

        public static float realtimeSinceStartup
        {
            get
            {
                var timeSpan = DateTime.Now.Subtract(m_StartTime);
                return (float) timeSpan.TotalSeconds;
            }
        }

        public static double timeSinceMidnight
        {
            get
            {
                var timeSpan = DateTime.Now.Subtract(midnight);
                return timeSpan.TotalSeconds;
            }
        }
    }
}