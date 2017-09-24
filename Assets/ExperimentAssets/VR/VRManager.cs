using Assets.ExperimentAssets.Player;
using Assets.ExperimentAssets.Scripts.DataHolders;
using Assets.ExperimentAssets.VR.Player;

namespace Assets.ExperimentAssets.VR
{
    public enum VRType
    {
        None,
        Vive,
        Virtualizer,
        Occulus
    }
    public class VRManager : Singleton<VRManager>
    {
        public VRType Type;

        public RigidBodyPlayerController RigidBodyPlayer;
        public VivePlayerController VivePlayer;
        public VirtualizerPlayerController VirtualizerPlayer;

        #region MonoBehaviour
        void Awake()
        {
            var settings = SettingsHolder.Instance.ExperimentInfo;
            SetType(settings.VRType);
            //FindPlayers();
        }
        #endregion
        #region Public API
        public void SetType(VRType type)
        {
            Type = type;
            switch (type)
            {
                case VRType.Virtualizer:
                    if(RigidBodyPlayer) RigidBodyPlayer.gameObject.SetActive(false);
                    if(VivePlayer) VivePlayer.gameObject.SetActive(false);
                    VirtualizerPlayer.gameObject.SetActive(true);
                    break;
                case VRType.Vive:
                    break;
                case VRType.None:
                    if(VivePlayer) VivePlayer.gameObject.SetActive(false);
                    if(VirtualizerPlayer) VirtualizerPlayer.gameObject.SetActive(false);
                    RigidBodyPlayer.gameObject.SetActive(true);
                    break;
            }
        }
        #endregion

        #region Private functions


        #endregion
    }
}