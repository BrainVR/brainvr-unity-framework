using System.Runtime.Serialization;
using UnityEngine;

namespace BrainVR.UnityLogger
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
    public class ExperimentInfo : ScriptableObject
    {
        private static ExperimentInfo _instance;

        [DataMember] public ParticipantInfo Participant;
        [DataMember] public CameraInfo CameraInfo;
        [DataMember] public ScreenInfo ScreenInfo;
        [DataMember] public string LevelName;

        public void OnEnable()
        {
            Participant = new ParticipantInfo();
            CameraInfo = new CameraInfo();
            ScreenInfo = new ScreenInfo();
            LevelName = "BVA";
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

        public static ExperimentInfo Instance
        {
            get { return _instance ?? (_instance = CreateInstance<ExperimentInfo>()); }
        }
    }
}
