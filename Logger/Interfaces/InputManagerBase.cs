namespace BrainVR.UnityFramework.Logger.Interfaces
{
    public abstract class InputManagerBase
    {
        public delegate void ButtonPressedHandler(string name);
        public static event ButtonPressedHandler ButtonPressed;
    }
}

