using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiments.Helpers
{

    public static class ExperimentSerialisationHelper {
        public static void SaveJsonSettings<T>(T settingsClass, string name)
        {
            string jsonString = JsonConvert.SerializeObject(settingsClass);
            string FilePath = Application.dataPath + "/ExperimentSettings/";

            //creates a folder for the patient, if it doesnt exist
            Directory.CreateDirectory(FilePath);

            File.WriteAllText(@FilePath + name, jsonString);
        }
        public static void SaveJsonSettings(string json, string name)
        {
            string FilePath = Application.dataPath + "/ExperimentSettings/";

            //creates a folder for the patient, if it doesnt exist
            Directory.CreateDirectory(FilePath);

            File.WriteAllText(@FilePath+name, json);
        }
    }
}
