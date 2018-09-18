using UnityEngine;

namespace BrainVR.UnityFramework.Menu
{
    public class MenuHelpers : MonoBehaviour
    {
        public static void MenuOn()
        {
            //redo cursor to a cursoc class - maybe we don't want it confined
            Cursor.visible = true;
            //hack from http://answers.unity3d.com/answers/1119750/view.html
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }

        public static void MenuOff()
        {
            //redo cursor to a cursoc class - maybe we don't want it confined
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
