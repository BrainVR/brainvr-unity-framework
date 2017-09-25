using System;
using Assets.ExperimentAssets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ExperimentAssets.Menu
{
    public class MenuExperiment : Singleton<MenuExperiment>
    {
        public Toggle MenuToggle;

        public enum MenuState {ON, OFF};

        public delegate void MenuStateHandler(MenuState toState);
        public static event MenuStateHandler MenuStateChanged;

        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SwitchMenu();
        }

        public void SwitchMenu()
        {
            (MenuToggle.isOn ? (Action) TurnMenuOff : TurnMenuOn)();
        }

        public void BackToMenu()
        {
            StopExperiment();
            //better check it and keep some settings
            //needs to reset menu state as well as time 
            SceneManager.LoadScene(0);
        }

        public void TurnMenuOn()
        {
            MenuHelpers.MenuOn();
            MenuToggle.isOn = true;
            if (MenuStateChanged != null) MenuStateChanged(MenuState.ON);
        }

        public void TurnMenuOff()
        {
            MenuHelpers.MenuOff();
            MenuToggle.isOn = false;
            if (MenuStateChanged != null) MenuStateChanged(MenuState.OFF);
        }

        public void StopExperiment()
        {
            ExperimentManager.Instance.StopExperiment();
        }

        public void StartExperiment()
        {
            ExperimentManager.Instance.StartExperiment();
            TurnMenuOff();
        }

        public void RestartExperiment()
        {
            ExperimentManager.Instance.RestartExperiment();
            TurnMenuOff();
        }
    }
}
