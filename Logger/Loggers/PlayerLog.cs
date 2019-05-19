using System.Collections.Generic;
using BrainVR.UnityFramework.Helpers;
using BrainVR.UnityFramework.InputControl;
using BrainVR.UnityFramework.Logger.Interfaces;
using UnityEngine;

//REquires an objsect with a player tag to be present

namespace BrainVR.UnityFramework.Logger
{
    public class PlayerLog : MonoLog
    {
        public GameObject Player;
        //HOW OFTEN DO YOU WANNA LOG
        //applies only to children that can log continuously (Plyer Log), not to those that log based on certain events (Quest log, BaseExperiment log)
        public float LoggingFrequency = 0.005F;

        protected override string LogName => "player";

        private float _deltaTime;
        private double _lastTimeWrite;
        private IPlayerController _playerController;
        //this is for filling in custom number of fields that follow after common fields
        // for example, we need one column for input, but it is not always used, so we need to
        // create empty single column
        private const int NEmpty = 1;
        #region MonoBehaviour
        void Update()
        {
            //calculating FPS
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        }
        void FixedUpdate()
        {
            if (!IsLogging) return;
            if (_lastTimeWrite + LoggingFrequency < SystemTimer.TimeSinceMidnight)
            {
                LogPlayerUpdate();
                _lastTimeWrite = SystemTimer.TimeSinceMidnight;
            }
        }
        #endregion

        #region Monolog variables
        protected override void AfterLogSetup()
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
            Log.WriteLine(HeaderLine());
        }
        #endregion

        #region Public API
        public void StartLogging()
        {
            if (IsLogging) return;
            if (!Player)
            {
                Debug.Log("There is no player Game object. Cannot start player log.");
                return;
            }
            //this is the header line for analysiss software
            InputManager.ButtonPressed += LogPlayerInput;
            _lastTimeWrite = SystemTimer.TimeSinceMidnight;
            IsLogging = true;
        }
        public void StopLogging()
        {
            if (!IsLogging) return;
            InputManager.ButtonPressed -= LogPlayerInput;
            IsLogging = false;
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
        #endregion
        #region private function
        private string HeaderLine()
        {
            var line = "Time;";
            line += _playerController.HeaderLine();
            line += "FPS; Input;";
            return line;
        }
        private List<string> CollectData()
        {
            //TestData to Write is a parent method that adds some information to the beginning of the player info
            var strgs = _playerController.PlayerInformation();
            AddTimestamp(ref strgs);
            //adds FPS
            AddValue(ref strgs, (1.0f / _deltaTime).ToString("F4"));
            //needs an empty column for possible input information
            return strgs;
        }
        #endregion
    }
}
