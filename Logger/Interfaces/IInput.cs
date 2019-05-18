using System;

namespace BrainVR.UnityFramework.Logger.Interfaces
{
    public class ButtonPressedArgs : EventArgs
    {
        public string ButtonName;
    }
    public interface IInput
    {
        event EventHandler<ButtonPressedArgs> ButtonPressed;
    }
}