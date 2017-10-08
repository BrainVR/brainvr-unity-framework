using Newtonsoft.Json;

namespace BrainVR.UnityLogger
{
    public class ExperimentInfoLog : MonoLog
    {
        private Log _log;
        //default instantiates without the player ID
        protected string LogName = "ExperimentInfo";

        public override void Instantiate(string timeStamp)
        {
            _log = new Log("NEO", LogName, timeStamp);
            WriteExperimentData();
        }
        public void Instantiate(string timeStamp, string id)
        {
            _log = new Log(id, LogName, timeStamp);
            WriteExperimentData();
        }
        private void WriteExperimentData()
        {
            _log.WriteLine("***EXPERIMENT INFO***");
            ExperimentInfo info = ExperimentInfo.Instance;
            info.PopulateInfo();
            var s = JsonConvert.SerializeObject(info, Formatting.Indented);
            _log.WriteLine(s);
            _log.WriteLine("---EXPERIMENT INFO---");
        }
    }
}