using System.Collections.Generic;
using BrainVR.UnityFramework.DataHolders;
using BrainVR.UnityFramework.Logger;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.UI.MainMenu
{
    public class MainMenuController : Singleton<MainMenuController>
    {
        public Text LevelSelection;     
        public GameObject SetSettingsButtonGO;
        public GameObject LoadedSettingsGroup;
        public Text ExperimentName;

        private ExperimentInfo _experimentInfo;
        private List<SetSettingsButton> _buttons = new List<SetSettingsButton>();

        void OnEnable()
        {
            Cursor.visible = true;
            //Checks for instantiated stuff
            _experimentInfo = ExperimentInfo.Instance;
            UpdateSettings();
            if (_experimentInfo == null) return;
            var id = GameObject.Find("ParticipantSettings/ID/").GetComponentInChildren<InputField>();
            id.text = _experimentInfo.Participant.Id;
        }
        public void UpdateSettings()
        {
            //sets the level
            UpdateLevel();
            UpdateScroll();
            //sets the soud levels
            //sets keybindings?
        }
        public void StartExperiment()
        {
            if (!CanStart()) return;
            PrepareForStart();
            //DontDestroyOnLoad(GameObject.Find("Settings"));
            SceneManager.LoadScene(SettingsHolder.Instance.CurrentExperimentSettings().LevelName); //needs to ad one as dropdown starts at 0 and BVA is 1
        }
        private bool CanStart()
        {
            return SettingsHolder.Instance.CurrentExperimentSettings() != null;
        }
        public void QuitExperiment()
        {
            Application.Quit();
        }
        #region update settings
        private void UpdateLevel()
        {
            if (SettingsHolder.Instance.CurrentExperimentSettings() != null)
                LevelSelection.text = SettingsHolder.Instance.CurrentExperimentSettings().LevelName;
        }
        //works unfortunately based on ORDER - first button is tied to the FIRST 
        //TODO -cannot do removing of tests
        private void UpdateScroll()
        {
            var settings = SettingsHolder.Instance;
            for (var i = 0; i < settings.ExperimentSettings.Count; i++)
            {
                if (i >= _buttons.Count)
                {
                    var go = Instantiate(SetSettingsButtonGO);
                    go.transform.SetParent(LoadedSettingsGroup.transform);
                    var setSettigns = go.GetComponent<SetSettingsButton>();
                    setSettigns.Initialise(settings.ExperimentSettingsFilenames[i], i);
                    _buttons.Add(setSettigns);
                }
                _buttons[i].SettingsActive(settings.CurrentExperiment == i);
            }
            var currentSettings = settings.CurrentExperimentSettings();
            if (currentSettings != null) ExperimentName.text = currentSettings.ExperimentName;
        }
        private void PrepareForStart()
        {
            PopulateExperimentInfo();
        }
        private void PopulateExperimentInfo()
        {
            PopulateId();
            _experimentInfo.LevelName = SettingsHolder.Instance.CurrentExperimentSettings().LevelName;
            _experimentInfo.ProductName = Application.productName;
            _experimentInfo.UnityVersion = Application.unityVersion;
            _experimentInfo.VersionNumber = Application.version;
            _experimentInfo.Platform = Application.platform.ToString();
            _experimentInfo.BuildGUID = Application.buildGUID;
        }
        #endregion
        #region settings helpers
        private void PopulateId()
        {
            var participantMenu = GameObject.Find("ParticipantSettings");
            if (participantMenu == null) return;
            var idGameObject = participantMenu.transform.Find("ID");
            var field = idGameObject.GetComponentInChildren<InputField>();
            _experimentInfo.Participant.Id = field.text;
        }
        #endregion
    }
}