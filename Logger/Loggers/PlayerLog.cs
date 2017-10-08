using System.Collections.Generic;
using BrainVR.UnityLogger.Interfaces;
using UnityEngine;

//REquires an objsect with a player tag to be present

namespace BrainVR.UnityLogger
{
    public class PlayerLog : MonoLog
    {
        public GameObject Player;

        private IPlayerController _playerController;
        //HOW OFTEN DO YOU WANNA LOG
        //applies only to children that can log continuously (Plyer Log), not to those that log based on certain events (Quest log, Experiment log)
        public float LoggingFrequency = 0.005F;

        float _deltaTime;
        private double _lastTimeWrite;

        //this is for filling in custom number of fields that follow after common fields
        // for example, we need one column for input, but it is not always used, so we need to
        // create empty single column
        private const int NEmpty = 1;


        public override void Instantiate(string timeStamp)
        {
            Log = new Log("NEO", "player", timeStamp);
            SetupLog();
        }
        public void Instantiate(string timeStamp, string id)
        {
            Log = new Log(id, "player", timeStamp);
            SetupLog();
        }
        #region MonoBehaviour
        void Update()
        {
            //calculating FPS
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        }
        void FixedUpdate()
        {
            if (!Logging) return;
            if (_lastTimeWrite + LoggingFrequency < SystemTimer.timeSinceMidnight)
            {
                LogPlayerUpdate();
                _lastTimeWrite = SystemTimer.timeSinceMidnight;
            }
        }
        #endregion
        void SetupLog()
        {
            if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
            if (!Player)
            {
                Debug.Log("There is no player Game object. Cannot setup player log.");
                Log.WriteLine("There is no player Game object in the game. Can't log");
                return;
            }
            _playerController = Player.GetComponent<IPlayerController>();
            if (_playerController == null)
            {
                Debug.Log("player GO does not have Player Controller component.");
                Log.WriteLine("There is no valid player Game object in the game. Can't log");
                return;
            }
            Log.WriteLine(_playerController.HeaderLine());
        }
        public void StartLogging()
        {
            if (Logging) return;
            if (!Player)
            {
                Debug.Log("There is no player Game object. Cannot start player log.");
                return;
            }
            //this is the header line for analysiss software
            InputManagerBase.ButtonPressed += LogPlayerInput;
            _lastTimeWrite = SystemTimer.timeSinceMidnight;
            Logging = true;
        }
        public void StopLogging()
        {
            if (!Logging) return;
            InputManagerBase.ButtonPressed -= LogPlayerInput;
            Logging = false;
        }
        public void LogPlayerInput(string input)
        {
            var strgs = CollectData();
            AddValue(ref strgs, input);
            WriteLine(strgs);
        }
        public void LogPlayerUpdate()
        {
            var strgs = CollectData();
            strgs.AddRange(WriteBlank(NEmpty));
            WriteLine(strgs);
        }
        /// <summary>
        /// Collects data and writes it to the appropriate log
        /// </summary>
        protected List<string> CollectData()
        {
            //TestData to Write is a parent method that adds some information to the beginning of the player info
            List<string> strgs = _playerController.PlayerInformation();
            AddTimestamp(ref strgs);
            //adds FPS
            AddValue(ref strgs, (1.0f / _deltaTime).ToString("F4"));
            //needs an empty column for possible input information
            return strgs;
        }
    }
}
