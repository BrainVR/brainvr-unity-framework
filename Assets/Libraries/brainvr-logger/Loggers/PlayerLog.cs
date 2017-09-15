using System.Collections.Generic;
using UnityEngine;

//REquires an objsect with a player tag to be present

namespace BrainVR.UnityLogger
{
    public class PlayerLog : MonoLog
    {
        public GameObject Player;
        //HOW OFTEN DO YOU WANNA LOG
        //applies only to children that can log continuously (Plyer Log), not to those that log based on certain events (Quest log, Experiment log)
        public float LoggingFrequency = 0.005F;

        float _deltaTime;
        private double _lastTimeWrite;

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
                WriteLine(CollectData());
                _lastTimeWrite = SystemTimer.timeSinceMidnight;
            }
        }
        void SetupLog()
        {
            if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
            if (!Player)
            {
                Debug.Log("There is no player Game object. Cannot setup player log.");
                Log.WriteLine("There is no player Game object in the game. Can't log");
                return;
            }
            Log.WriteLine("Time; Position; Rotation.X; Rotation.Y; FPS; Input;");
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
            List<string> strgs = PlayerInformation();
            AddTimestamp(ref strgs);
            AddValue(ref strgs, input);
            WriteLine(strgs);
        }
        /// <summary>
        /// Collects data and writes it to the appropriate log
        /// </summary>
        protected List<string> CollectData()
        {
            //TestData to Write is a parent method that adds some information to the beginning of the player info
            List<string> strgs = PlayerInformation();
            AddTimestamp(ref strgs);
            //needs an empty column for possible input information
            strgs.AddRange(WriteBlank(1));
            return strgs;
        }   
        /// <summary>
        /// Function to prepare a c# list of strings to be written to the appropriate log file
        /// </summary>
        /// <returns>List of strings</returns>
        private List<string> PlayerInformation()
        {
            List<string> strgs = new List<string>();
            //logging position
            strgs.Add(Player.transform.position.ToString("F4"));
            //logging rotation
            strgs.Add(Player.transform.eulerAngles.y.ToString("F4"));
            //logging rotation
            strgs.Add(Camera.main.transform.eulerAngles.x.ToString("F4"));
            //loggin FPS
            strgs.Add((1.0f/_deltaTime).ToString("F4"));
            return strgs;
        }
    }
}
