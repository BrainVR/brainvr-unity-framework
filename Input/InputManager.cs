using BrainVR.UnityFramework.Player;
using UnityEngine;

namespace BrainVR.UnityFramework.InputControl
{
    public class InputManager : MonoBehaviour
    {
        public delegate void ButtonPressedHandler(string name);

        public static event ButtonPressedHandler ButtonPressed;

        public delegate void SpecialButtonHandler();

        public static event SpecialButtonHandler PointButtonPressed;
        public static event SpecialButtonHandler ConfirmationButtonPressed;
        public static event SpecialButtonHandler CancelButtonPressed;
        public static event SpecialButtonHandler MenuButtonPressed;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Point"))
            {
                if (ButtonPressed != null) ButtonPressed("Point");
                if (PointButtonPressed != null) PointButtonPressed();
            }

            if (Input.GetButtonDown("Unstuck")) PlayerController.Instance.Unstuck();
            if (Input.GetButtonDown("Confirm"))
            {
                if (ConfirmationButtonPressed != null) ConfirmationButtonPressed();
            }
            if (Input.GetButtonDown("Menu"))
            {
                if (MenuButtonPressed != null) MenuButtonPressed();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (CancelButtonPressed != null) CancelButtonPressed();
            }

        }
    }
}