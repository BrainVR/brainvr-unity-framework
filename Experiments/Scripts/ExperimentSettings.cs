using System.Collections.Generic;
using Assets.GeneralScripts.Serialisation;
using BrainVR.UnityFramework.Experiment.Helpers;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiment
{
    public class ExperimentSettings : ScriptableObject
    {
        [JsonIgnore]
        public bool ShouldLog = true;
        [JsonIgnore]
        public string ExperimentName = "SomeTask";
        [JsonIgnore]
        public string LevelName;
        public Dictionary<string, string> Messages = new Dictionary<string, string>();
        #region Serialisation
        public string SerialiseSettings()
        {
            string strJson = JsonConvert.SerializeObject(this, SerialisationConstants.SerialisationSettings());
            return strJson;
        }
        public void DeserialiseSettings(string path)
        {
            var settings = JSONDeserialiser.GetArrayField(path, "Settings", 0);
            //TODO - checking for correct json
            JsonConvert.PopulateObject(settings, this);
            //TODO - throwing exception if wrong
        }

        public string SerialiseOut()
        {
            var serialised = new SerialisedExperiment
            {
                Settings = this,
                LevelName = LevelName,
                ExperimentName = ExperimentName
            };
            return JsonConvert.SerializeObject(serialised, SerialisationConstants.SerialisationSettings());
        }
        #endregion
        #region Message helpers
        public string Message(string key)
        {
            string value;
            if (!Messages.TryGetValue(key, out value))
            {
                Debug.Log("There is no key \"" + key + "\" in the Settings Messages.");
            }
            return value;
        }
        #endregion
    }
    public class SerialisedExperiment
    {
        public string ExperimentName;
        public string LevelName;
        public ExperimentSettings Settings;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(ExperimentSettings),true)]
    public class SettingsEditor : Editor
    {
        string serialisedSettings = "Settings have not been serialised yet";
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var myScript = target as ExperimentSettings;
            serialisedSettings = myScript.SerialiseOut();
            GUILayout.TextArea(serialisedSettings);
        }
    }
#endif
}