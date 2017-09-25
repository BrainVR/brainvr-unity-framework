using System;
using System.IO;
using System.Linq;
using BrainVR.UnityFramework.DataHolders;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.UI.MainMenu
{
    public class LoadSettingsUI : MonoBehaviour
    {
        public Dropdown SettingsFileToLoadPath;
        public GameObject DataLoadedGameObject;
        public string SettingsDirectory;

        private string _settingsPath;
        private SettingsHolder _settingsHolder;

        void Start()
        {
            if (_settingsHolder == null) _settingsHolder = SettingsHolder.Instance;
            if (string.IsNullOrEmpty(SettingsDirectory))
                SettingsDirectory = Application.dataPath + "/../ExperimentSettings/";
            ReloadQuests();
        }
        public void LoadSettingsData()
        {
            try
            {
                SettingsHolder.Instance.AddExperimentSettings(_settingsPath);
                DataLoadedGameObject.GetComponent<Text>().text = "Settings sucessfully loaded";
                DataLoadedGameObject.GetComponent<Text>().color = Color.green;
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
        public void SettingsFileChanged(int i)
        {
            _settingsPath = SettingsDirectory + SettingsFileToLoadPath.captionText.text;
        }
        //editor manipulation
        public void ReloadQuests()
        {
            // get all valid files
            var info = new DirectoryInfo(SettingsDirectory);
            var settingsPaths = info.GetFiles()
                .Where(f => IsValidFileTypeJson(f.Name))
                .ToList();
            foreach (var qp in settingsPaths)
            {
                Debug.Log(qp.Name);
                SettingsFileToLoadPath.options.Add(new Dropdown.OptionData() { text = qp.Name });
            }
        }
        bool IsValidFileTypeJson(string fileName)
        {
            return ".json" == (Path.GetExtension(fileName));
        }
    }
}
