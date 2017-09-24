using UnityEngine;

namespace Assets.ExperimentAssets.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Canvas SettingsCanvas;
        private bool _settingsOn;

        #region MonoBehaviour

        void Start()
        {
            SettingsCanvas.gameObject.SetActive(false);
            _settingsOn = false;
        }

        void Update()
        {
            if (Input.GetButtonDown("PlayerSettings")) _settingsOn = !_settingsOn;
            ShowSettings(_settingsOn);
        }

        #endregion

        #region Public API
        public void SetHeight(float height)
        {
            PlayerController.Instance.SetHeight(height);
        }

        public void SetSpeed(float speed)
        {
            PlayerController.Instance.SetSpeed(speed);
        }
        #endregion

        #region Private functions

        void ShowSettings(bool bo)
        {
            SettingsCanvas.gameObject.SetActive(bo);
            
        }

        #endregion
    }
}
