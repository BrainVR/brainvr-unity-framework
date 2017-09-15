using Assets.ExperimentAssets.Scripts.Player;
using UnityEngine;

namespace Assets.GeneralScripts
{
    public class InputManager : MonoBehaviour
    {
        public delegate void ButtonPressedHandler(string name);
        public static event ButtonPressedHandler ButtonPressed;

        public delegate void SpecialButtonHandler();
        public static event SpecialButtonHandler PointButtonPressed;

        // Update is called once per frame
        void Update ()
        {
            if (Input.GetButtonDown("Point"))
            {
                if (ButtonPressed != null) ButtonPressed("Point");
                if (PointButtonPressed != null) PointButtonPressed();
            }

            if (Input.GetButtonDown("Unstuck"))
            {
                PlayerController.Instance.Unstuck();
            }
        }
    }
}
