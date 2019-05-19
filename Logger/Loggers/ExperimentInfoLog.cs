using Newtonsoft.Json;

namespace BrainVR.UnityFramework.Logger
{
    public class ExperimentInfoLog : MonoLog
    {
        //default instantiates without the player ID
        protected override string LogName => "ExperimentInfo";
        protected override void AfterLogSetup()
        {
            WriteExperimentData();
        }
        private void WriteExperimentData()
        {
            var info = ExperimentInfo.Instance;
            info.PopulateInfo();
            var s = JsonConvert.SerializeObject(info, Formatting.Indented);
            Log.WriteBlock("EXPERIMENT INFO", s);
        }
    }
}