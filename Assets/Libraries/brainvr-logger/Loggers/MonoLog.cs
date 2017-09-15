using System;
using System.Collections.Generic;
using UnityEngine;
//important namespaces

namespace BrainVR.UnityLogger
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

        public bool Logging;

        #region MonoBehaviour 
        void OnApplicationQuit()
        {
            Close();
        }
        #endregion
        public abstract void Instantiate(string str);
        public virtual void Close()
        {
            if (Log != null) Log.Close();
            Logging = false;
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
            strgs.Insert(0, SystemTimer.timeSinceMidnight.ToString("F4"));
        }
        protected void AddValue(ref List<string> strgs, string value)
        {
            strgs.Add(value);
        }
        //helper to fill blank spaces in logs so that each line has the same number of separators
        //takes int as an input and creates a blank list of that many empty spaces
        protected List<String> WriteBlank(int num)
        {
            List<String> ls = new List<String>();
            for (int i = 0; i < num; i++) ls.Add("");
            return ls;
        }
        #endregion
    }
}
