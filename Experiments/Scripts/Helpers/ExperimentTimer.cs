using System.Collections;
using BrainVR.UnityFramework.Helpers;

namespace Assets.Libraries.Experiments.Helpers
{
    public class ExperimentTimer
    {
        private readonly float _time;
        private float _startTime;

        public delegate void TimeHandler(ExperimentTimer timer);

        public event TimeHandler TimeRanOut;

        public ExperimentTimer(float time)
        {
            _time = time;
        }

        public void Start()
        {
            _startTime = SystemTimer.RealtimeSinceStartup;
        }

        private IEnumerator Stopwatch()
        {
            while (_time < 1)
            {
                yield return null;
            }
            if (TimeRanOut != null) TimeRanOut(this);
        }

    }
}
