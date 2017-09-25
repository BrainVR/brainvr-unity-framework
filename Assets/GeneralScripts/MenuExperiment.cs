using System;
using Assets.ExperimentAssets.Experiments;
using Assets.ExperimentAssets.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.GeneralScripts
{
    public class MenuExperiment : Singleton<MenuExperiment>
    {

        public Toggle MenuToggle;

        public enum MenuState {ON, OFF};

        public delegate void MenuStateHandler(MenuState toState);
        public static event MenuStateHandler MenuStateChanged;

        void Update () {
            if (Input.GetKeyDown(KeyCode.Escape))
                SwitchMenu();
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            PlayerController.Instance.EnableMovement(false);
            PlayerController.Instance.EnableRotation(false);
            MenuToggle.isOn = true;
            if (MenuStateChanged != null) MenuStateChanged(MenuState.ON);
        }

        public void TurnMenuOff()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            MenuToggle.isOn = false;
            PlayerController.Instance.EnableMovement();
            PlayerController.Instance.EnableRotation();
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
