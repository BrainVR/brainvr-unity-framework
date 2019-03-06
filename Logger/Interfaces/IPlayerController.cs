using System.Collections.Generic;

namespace BrainVR.UnityFramework.Logger.Interfaces
{
    public interface IPlayerController
    {
        string HeaderLine();
        List<string>  PlayerInformation();
    }
}