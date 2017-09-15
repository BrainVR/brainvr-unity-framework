using UnityEngine;
using System;
using System.IO;

namespace BrainVR.UnityLogger
{
    public class Dbg
    {
        //-------------------------------------------------------------------------------------------------------------------------
        public string LogFile = "unity-log.txt";

        public bool EchoToConsole = true;
        public bool AddTimeStamp = true;

        //-------------------------------------------------------------------------------------------------------------------------
        private StreamWriter OutputStream;

        //-------------------------------------------------------------------------------------------------------------------------
        static Dbg Singleton = null;

        public Dbg()
        {
            OutputStream = new StreamWriter("unity-log.txt", true);
            Singleton = this;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public static Dbg Instance
        {
            get
            {
                if (Singleton == null) new Dbg();
                return Singleton;
            }
        }
        private void Write(string message)
        {
#if DEBUG
            if (AddTimeStamp)
            {
                DateTime now = DateTime.Now;
                message = string.Format("[{0:H:mm:ss}] {1}", now, message);
            }
            if (OutputStream == null)
            {
                UnityEngine.Debug.Log("OutputStream is NULL!");
                OutputStream = new StreamWriter(Application.dataPath + "/" + "unity-log.txt", true);
            }
            if (OutputStream != null)
            {
                OutputStream.WriteLine(message);
                OutputStream.Flush();
            }
            if (EchoToConsole)
            {
                UnityEngine.Debug.Log(message);
            }
#endif
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //[Conditional("DEBUG"), Conditional("PROFILE")]
        public static void Trace(string Message)
        {
#if DEBUG
            if (Dbg.Instance != null) Dbg.Instance.Write(Message);
            else Debug.Log(Message);
#endif
        }


    }
}
