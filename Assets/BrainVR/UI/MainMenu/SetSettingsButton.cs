using BrainVR.UnityFramework.DataHolders;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.UI.MainMenu
{
    public class SetSettingsButton : MonoBehaviour
    {
        public Button MyButton;
        public Text Text;
        public Image Image;
        void Start()
        {
            //validates it has text and color
            MyButton = MyButton ?? GetComponent<Button>();
            Text = Text ?? gameObject.GetComponentInChildren<Text>();
        }

        public void Initialise(string experimentName, int i)
        {
            Text.text = experimentName;
            SettingsActive(false);
            Subscribe(i);
        }

        public void Subscribe(int i)
        {
            MyButton.onClick.RemoveAllListeners();
            MyButton.onClick.AddListener(() => SettingsHolder.Instance.SetSettings(i));
            MyButton.onClick.AddListener(() => MainMenuController.Instance.UpdateSettings());
        }

        public void SettingsActive(bool bo)
        {
            Image.color = bo ? Color.green : Color.grey;
        }
    }
}
