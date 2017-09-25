using UnityEngine;

namespace BrainVR.UnityFramework.Scripts.Objects.Beeper
{
    public class BeeperController : MonoBehaviour
    {
        public string BeeperName;
        public AudioClip sound;

        public AudioClip ReturnSound()
        {
            return sound;
        }
    }
}
