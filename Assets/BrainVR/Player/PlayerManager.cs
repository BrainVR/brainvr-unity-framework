using System;
using BrainVR.UnityFramework.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Canvas SettingsCanvas;
        public Text PlayerSpeedText;
        public Text PlayerHeighText;
        private bool _settingsOn;

        #region MonoBehaviour
        void Update()
        {
            if (Input.GetButtonDown("PlayerSettings"))
            {
                _settingsOn = !_settingsOn;
                ShowSettings(_settingsOn);
            }
        }
        #endregion
        #region Public API
        public void SetHeight(float height)
        {
            PlayerController.Instance.SetHeight(height);
            PlayerHeighText.text = "Player Height: " + height;
        }
        public void SetSpeed(float speed)
        {
            PlayerController.Instance.SetSpeed(speed);
            PlayerSpeedText.text = "Player Speed: " + speed;

        }
        #endregion

        #region Private functions
        private void ShowSettings(bool bo)
        {
            (bo ? (Action) MenuHelpers.MenuOn : MenuHelpers.MenuOff)();
            SettingsCanvas.gameObject.SetActive(bo);
        }

        #endregion
    }
}
