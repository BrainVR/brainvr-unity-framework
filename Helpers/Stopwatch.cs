using System.Collections;

namespace BrainVR.UnityFramework.Helpers
{
    public class Stopwatch
    {
        private readonly float _time;
        public delegate void TimeHandler(Stopwatch stopwatch);
        public event TimeHandler TimeRanOut;

        public Stopwatch(float time)
        {
            _time = time;
        }
        
        public IEnumerator Countdown()
        {
            var startTime = SystemTimer.RealtimeSinceStartup;
            while (startTime + _time > SystemTimer.RealtimeSinceStartup)
            {
                yield return null;
            }
            if (TimeRanOut != null) TimeRanOut(this);
        }

    }
}
