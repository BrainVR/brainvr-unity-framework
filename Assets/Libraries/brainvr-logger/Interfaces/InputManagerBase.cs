namespace BrainVR.UnityLogger
{
    public abstract class InputManagerBase
    {
        public delegate void ButtonPressedHandler(string name);
        public static event ButtonPressedHandler ButtonPressed;
    }
}

