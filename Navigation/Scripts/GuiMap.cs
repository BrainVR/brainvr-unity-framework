#if UNITY_EDITOR
using UnityEditor;
#endif
using BrainVR.UnityFramework.Navigation;
using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.Navigation
{
    public class GuiMap : MonoBehaviour
    {
        public enum ForceShow
        {
            AlwaysShow,
            NeverShow,
            QuestHandles
        }
        public enum FollowPlayer
        {
            FollowRotate,
            FollowRotateArrow,
            FollowDontRotate,
            DontFollowDontRotate
        }
        public enum MinimapType
        {
            Static,
            Schematic
        }

        public GameObject MapCamera;
        public MinimapType Type;
        public GameObject Map;
        public Sprite DirrectionalArrow;
        public Sprite LocationMark;

        private GameObject _player;
        private GameObject _mapArrow;
        private RectTransform _mapArrowTransform;

        FollowPlayer _followingState = FollowPlayer.FollowRotate;
        ForceShow _forceVisible = ForceShow.QuestHandles;

        #region MonoBehaviour


        #endregion
        void OnEnable()
        {
            if (_player == null) _player = PlayerController.Instance.gameObject;
            if (_mapArrow == null) _mapArrow = GetArrow();
            //TODO - problematic
            _mapArrowTransform = _mapArrow.GetComponent<RectTransform>(); 
        }
        void Update()
        {
            switch (_followingState)
            {
                case FollowPlayer.FollowRotate:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    Quaternion rotation = Quaternion.Euler(90, _player.transform.eulerAngles.y, _player.transform.eulerAngles.z);
                    MapCamera.transform.rotation = rotation;
                    break;
                case FollowPlayer.FollowRotateArrow:
                    MapCamera.transform.position = _player.transform.position+ Vector3.up*100;
                    //the minus in the transform is important
                    //the player rotates around y axis in the world, but the arrow transform is in the Z axis in the canvas
                    _mapArrowTransform.rotation = Quaternion.Euler(0, 0, -_player.transform.eulerAngles.y);
                    break;
                case FollowPlayer.FollowDontRotate:
                    MapCamera.transform.position = _player.transform.position + Vector3.up*100;
                    break;
                case FollowPlayer.DontFollowDontRotate:
                    break;

            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                _forceVisible += 1;
                if (_forceVisible >= ForceShow.AlwaysShow) _forceVisible = ForceShow.QuestHandles;
                Appear();
            }
            if (Input.GetKeyUp(KeyCode.O))
            {
                switch (_followingState)
                {
                    case FollowPlayer.FollowRotate:
                        _followingState = FollowPlayer.FollowRotateArrow;
                        break;
                    case FollowPlayer.FollowRotateArrow:
                        _followingState = FollowPlayer.FollowDontRotate;
                        break;
                    case FollowPlayer.FollowDontRotate:
                        _followingState = FollowPlayer.DontFollowDontRotate;
                        break;
                    case FollowPlayer.DontFollowDontRotate:
                        _followingState = FollowPlayer.FollowRotate;
                        break;
                }
                Hook();
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) Zoom();
            if (Input.GetKeyDown(KeyCode.KeypadMinus)) Unzoom();
        }
        internal Texture GetStaticMap()
        {
            //TODO - better validation
            try
            {
                return transform.Find("STATIC_MAP/guimap3d").gameObject.GetComponent<Renderer>().material.mainTexture;
            }
            catch
            {
                return null;
            }
        }
        //This method takes care of the necessary changes that follow changing of states
        //For example, whn we dont Follow and don't rotate, we want to set the Camera position once and for all
        //so it wouldn't change in each Update
        private void Hook()
        {
            switch (_followingState)
            {
                case FollowPlayer.FollowRotate:
                    ShowArrow(true);
                    break;
                case FollowPlayer.FollowRotateArrow:
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    ShowArrow(true);
                    break;
                case FollowPlayer.FollowDontRotate:
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    ShowArrow(false);
                    break;
                case FollowPlayer.DontFollowDontRotate:
                    MapCamera.transform.rotation = Quaternion.Euler(90,0,0);
                    MapCamera.transform.position = new Vector3(0, 0, 0);
                    _mapArrow.GetComponent<Image>().enabled = false;
                    break;
            }
        }
        //simple public method to change the Arrow/Map Behaviour from outside
        public void ChangeFollowState(FollowPlayer state)
        {
            _followingState = state;
            Hook();
        }
        public void Appear(bool bo = true)
        {
            switch (_forceVisible)
            {
                case ForceShow.NeverShow:
                    Hide();
                    break;
                case ForceShow.AlwaysShow:
                    Show();
                    break;
                case ForceShow.QuestHandles:
                    Map.SetActive(bo);
                    break;
            }
        }
        public void Show()
        {
            Map.SetActive(true);
        }
        public void Hide()
        {
            Map.SetActive(false);
        }
        public void Zoom()
        {
            var cam = MapCamera.GetComponent<Camera>();
            cam.orthographicSize = cam.orthographicSize + 5;
        }
        public void Unzoom()
        {
            var cam = MapCamera.GetComponent<Camera>();
            cam.orthographicSize = cam.orthographicSize - 5;
        }
        public void SetSize(float size)
        {
            if (size <= 0) return;;
            var cam = MapCamera.GetComponent<Camera>();
            cam.orthographicSize = size;
        }
        ///--------
        ///HELPER FUNCTIONS
        ///--------
        private GameObject GetArrow()
        {
            //GameObject go = transform.Find("Map-Arrow").gameObject;
            GameObject go = GameObject.Find("Map-Arrow");
            if (go == null)
            {
                Debug.LogError("Map-Arrow game object is not part of the prefab.");
                Debug.Break();
                return null;
            }
            return go;
        }

        private void ShowArrow(bool bo)
        {
            _mapArrow.GetComponent<Image>().sprite = bo ? DirrectionalArrow : LocationMark;
        }

        public void SetMinimapType(MinimapType type)
        {
            if (type.Equals(MinimapType.Static))
            {
                MapCamera.GetComponent<Camera>().cullingMask = LayerMask.GetMask("StaticMap");
            }
            else if (type.Equals(MinimapType.Schematic))
            {
                MapCamera.GetComponent<Camera>().cullingMask = LayerMask.GetMask("SchematicMap");
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GuiMap))]
public class GuiMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GuiMap map = (GuiMap)target;
        map.Type = (GuiMap.MinimapType)EditorGUILayout.EnumPopup(map.Type);

        Camera cam = map.MapCamera.gameObject.GetComponent<Camera>();

        switch (map.Type)
        {
            case GuiMap.MinimapType.Schematic:
                cam.cullingMask = LayerMask.GetMask("StaticMap");
                break;
            case GuiMap.MinimapType.Static:
                cam.cullingMask = LayerMask.GetMask("SchematicMap");
                break;
        }
        cam.orthographicSize = EditorGUILayout.FloatField("Camera size:", cam.orthographicSize);
        if (GUILayout.Button("+")) cam.orthographicSize -= 5;
        if (GUILayout.Button("-")) cam.orthographicSize += 5;
    }
}
#endif