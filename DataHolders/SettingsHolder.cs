using System.Collections.Generic;
using System.IO;
using Assets.GeneralScripts.Serialisation;
using BrainVR.UnityFramework.Experiments.Helpers;

namespace BrainVR.UnityFramework.DataHolders
{
    public class SettingsHolder : Singleton<SettingsHolder>
    {
        public ExperimentInfo ExperimentInfo = new ExperimentInfo();
        public List<ExperimentSettings> ExperimentSettings = new List<ExperimentSettings>();
        public string LevelName;

        private int _currentExperiment;
        public int CurrentExperiment
        {
            get { return _currentExperiment; }
            set { _currentExperiment = value >= 0 && value < ExperimentSettings.Count? value : 0; }
        }
        public void AddExperimentSettings(string path)
        {
            var text = File.ReadAllText(path);
            object obj = NestedDeserialiser.Deserialize(text);
            Dictionary<string, object> dict = (Dictionary<string, object>) obj;
            var expSettings = ExperimentLoader.PopulateExperimentSettings(dict["ExperimentName"].ToString(), path);
            expSettings.LevelName = dict["LevelName"].ToString();
            expSettings.ExperimentName = dict["ExperimentName"].ToString();
            ExperimentSettings.Add(expSettings);
        }
        public ExperimentSettings CurrentExperimentSettings()
        {
            if (ExperimentSettings.Count <= _currentExperiment) return null;
            return ExperimentSettings[_currentExperiment];
        }
        public void SetSettings(int i)
        {
            CurrentExperiment = i;
        }
    }
}
