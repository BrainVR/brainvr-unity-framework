using Assets.ExperimentAssets.Player;
using UnityEngine;

namespace Assets.ExperimentAssets.Menu
{
    public class MenuHelpers : MonoBehaviour
    {
        public static void MenuOn()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            PlayerController.Instance.EnableMovement(false);
            PlayerController.Instance.EnableRotation(false);
        }

        public static void MenuOff()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerController.Instance.EnableMovement();
            PlayerController.Instance.EnableRotation();
        }
    }
}
