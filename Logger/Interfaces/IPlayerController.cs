using System.Collections.Generic;

namespace BrainVR.UnityLogger.Interfaces
{
    public interface IPlayerController
    {
        string HeaderLine();
        List<string>  PlayerInformation();
    }
}