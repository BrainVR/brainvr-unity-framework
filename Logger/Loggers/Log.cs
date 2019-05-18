using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using System;
//important namespace
//for the write function

namespace BrainVR.UnityFramework.Logger
{
    public class Header
    {
        public string Participant;
        public string Timestamp;

        public Header(string participant, string timestamp)
        {
            Participant = participant;
            Timestamp = timestamp;
        }
    }

    public class Log
    {
        readonly StreamWriter _logFile;
        public string FilePath;
        public string DateString;
        const string RelativePath = "/../logs/";
        /// <summary>
        /// polymorph for init with timepstmp provided by master log 
        /// </summary>
        /// <param name="id">ParticipantId of the participant</param>
        /// <param name="logName">name of the log file, e.g. Player/experiment etc.</param>
        /// <param name="timestamp">timestamp provided by master log</param>
        public Log(string id, string logName, string timestamp = null)
        {
            if (timestamp == null) timestamp = DateTime.Now.ToString("HH-mm-ss-dd-MM-yyy");
            DateString = timestamp;
            var folderName = id + "_" + DateTime.Now.ToString("dd-MM-yyy") + "/";
            FilePath = Application.dataPath + RelativePath + folderName;
            Directory.CreateDirectory(FilePath);
            _logFile = new StreamWriter(FilePath + id + "_" + logName + "_" + timestamp + ".txt", true) {AutoFlush = true};
            WriteHeader(id);
        }
        //simple header file for each new star of the experiment
        private void WriteHeader(string id)
        {
            var header = new Header(id, DateString);
            _logFile.WriteLine("***SESSION HEADER***");
            _logFile.WriteLine(JsonConvert.SerializeObject(header, Formatting.Indented));
            _logFile.WriteLine("---SESSION HEADER---");
        }
        //takes a string and writes it down
        public void WriteLine(string str)
        {
            _logFile.WriteLine(str);
        }
        public void Close() 
        {
            _logFile.Close();
        }
        //takes a list of string as an argument and turns them into a one line that is written into the file
        public void WriteList(List<string> data)
        {
            //basically LINQ foreach
            var line = data.Aggregate("", (current, text) => current + (text + ";"));
            _logFile.WriteLine(line);
        }
        #region Helpers
        public static string NewLine => Environment.NewLine;
        #endregion
    }
}