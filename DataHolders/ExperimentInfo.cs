using System.Runtime.Serialization;
using UnityEngine;

namespace BrainVR.UnityFramework.Logger
{
    [System.Serializable]
    public class ParticipantInfo
    {
        public string Id = "NEO";
    }

    [System.Serializable]
    public class CameraInfo
    {
        public float FieldOfView;
        public float FacClipPlane;
    }

    [System.Serializable]
    public class ScreenInfo
    {
        public float Height;
        public float Width;
    }

    [DataContract]
    public class ExperimentInfo : Singleton<ExperimentInfo>
    {
        [DataMember] public ParticipantInfo Participant;
        [DataMember] public CameraInfo CameraInfo;
        [DataMember] public ScreenInfo ScreenInfo;
        [DataMember] public string ProductName;
        [DataMember] public string VersionNumber;
        [DataMember] public string UnityVersion;
        [DataMember] public string BuildGUID;
        [DataMember] public string Platform;
        [DataMember] public string LevelName;

        public void OnEnable()
        {
            Participant = new ParticipantInfo();
            CameraInfo = new CameraInfo();
            ScreenInfo = new ScreenInfo();
            LevelName = "NOT SPECIFIED";
            PopulateInfo();
        }
        public void PopulateInfo()
        {
            InvestigateCamera();
            InvestigateScreen();
        }
        private void InvestigateScreen()
        {
            ScreenInfo.Width = Screen.width;
            ScreenInfo.Height = Screen.height;
        }
        public void InvestigateCamera()
        {
            CameraInfo.FieldOfView = Camera.main.fieldOfView;
            CameraInfo.FacClipPlane = Camera.main.farClipPlane;
        }
    }
}
