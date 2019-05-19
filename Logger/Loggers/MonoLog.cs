using System;
using System.Collections.Generic;
using BrainVR.UnityFramework.Helpers;
using UnityEngine;
//important namespaces

namespace BrainVR.UnityFramework.Logger
{
    /// <summary>
    /// This class is to include some of the basic functionality and properties for the other log functions
    /// It differs from the Log class in that it incorporates the monoBehaviour for Application quit etc.
    /// </summary>
    ///  
    public abstract class MonoLog : MonoBehaviour
    {
        //class that takes care of System logging, not derived from the monobehaviour
        protected Log Log;
        protected abstract string LogName { get; }
        public bool IsLogging;
        #region MonoBehaviour 
        void OnApplicationQuit()
        {
            Close();
        }
        #endregion
        public void Setup(string timestamp = null, string id = "NEO")
        {
            OnLogSetup();
            Log = new Log(id, LogName, timestamp);
            AfterLogSetup();
        }
        protected virtual void OnLogSetup() { }
        protected virtual void AfterLogSetup() { }
        public virtual void Close()
        {
            Log?.Close();
            IsLogging = false;
        }
        protected virtual void WriteLine(List<string> strgs)
        {
            Log.WriteList(strgs);
        }
        protected virtual void WriteLine(string str)
        {
            Log.WriteLine(str);
        }
        public string GetLogTimestamp()
        {
            return Log.DateString;
        }
        #region Helpers
        //function to prep data for the log to do it's function 
        // basically it adds time and frameCount
        protected void AddTimestamp(ref List<string> strgs)
        {
            strgs.Insert(0, SystemTimer.TimeSinceMidnight.ToString("F4"));
        }
        protected void AddValue(ref List<string> strgs, string value)
        {
            strgs.Add(value);
        }
        //helper to fill blank spaces in logs so that each line has the same number of separators
        //takes int as an input and creates a blank list of that many empty spaces
        protected List<string> WriteBlank(int num)
        {
            var ls = new List<string>();
            for (var i = 0; i < num; i++) ls.Add("");
            return ls;
        }
        #endregion
    }
}
