using System;
using System.Collections.Generic;
using Assets.GeneralScripts;
using UnityEngine;

namespace BrainVR.UnityFramework.Scripts.Objects.Beeper
{
    public class BeeperManager : Singleton<BeeperManager>
    {
        public Dictionary<string, BeeperController> BeeperControllers = new Dictionary<string, BeeperController>();
        private AudioSource _audioPlayer;
        // Use this for initialization
        void Awake ()
        {
            foreach (var beeper in transform.GetComponentsInChildren<BeeperController>())
            {
                //TODO needs to increment the unknown name in case Unknown is called
                if (String.IsNullOrEmpty(beeper.BeeperName)) beeper.BeeperName = "Unknown";
                BeeperControllers.Add(beeper.BeeperName, beeper);
            }

            //TODO NEEDS TO CHECK PRESENCE
            _audioPlayer = GetComponent<AudioSource>();
        }
        public void Play(string beeperName)
        {
            try
            {
                _audioPlayer.PlayOneShot(BeeperControllers[beeperName].ReturnSound());
            }
            catch(KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
