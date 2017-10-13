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
            if (Input.GetButtonDown("Point")) Pointed();
            if (Input.GetButtonDown("Unstuck")) PlayerController.Instance.Unstuck();
            if (Input.GetButtonDown("Confirm")) Confirmed();
            if (Input.GetButtonDown("Menu")) MenuPressed();
            if (Input.GetButtonDown("Cancel")) Canceled();
        }

        #region EventRaisers
        public static void Pointed()
        {
            if (ButtonPressed != null) ButtonPressed("Point");
            if (PointButtonPressed != null) PointButtonPressed();
        }
        public static void Confirmed()
        {
            if (ConfirmationButtonPressed != null) ConfirmationButtonPressed();
        }
        public static void MenuPressed()
        {
            if (MenuButtonPressed != null) MenuButtonPressed();
        }
        public static void Canceled()
        {
            if (CancelButtonPressed != null) CancelButtonPressed();
        }
        #endregion
    }
}