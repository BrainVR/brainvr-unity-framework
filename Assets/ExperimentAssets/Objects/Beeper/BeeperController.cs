using UnityEngine;

namespace Assets.ExperimentAssets.Scripts.Objects.Beeper
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
