using System.Runtime.Serialization;
using Assets.ExperimentAssets.VR;
using UnityEngine;

namespace Assets.ExperimentAssets.Scripts.DataHolders
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
    public class ExperimentInfo
    {
        [DataMember]
        public ParticipantInfo Participant = new ParticipantInfo();
        [DataMember]
        public CameraInfo CameraInfo = new CameraInfo();
        [DataMember]
        public ScreenInfo ScreenInfo = new ScreenInfo();
        [DataMember]
        public string LevelName = "BVA";
        [DataMember]
        public VRType VRType = VRType.None;

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
    