using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.Navigation
{
    public class MapController : Singleton<MapController>
    {
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

        public Camera MapCamera;
        public MinimapType Type;
        public GameObject Map;
        public Sprite DirrectionalArrow;
        public Sprite LocationMark;

        private GameObject _player;
        private GameObject _mapArrow;
        private RectTransform _mapArrowTransform;

        private FollowPlayer _followingState = FollowPlayer.FollowRotate;

        #region MonoBehaviour
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
                    Quaternion rotation = Quaternion.Euler(90, _player.transform.eulerAngles.y,
                        _player.transform.eulerAngles.z);
                    MapCamera.transform.rotation = rotation;
                    break;
                case FollowPlayer.FollowRotateArrow:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    //the minus in the transform is important
                    //the player rotates around y axis in the world, but the arrow transform is in the Z axis in the canvas
                    _mapArrowTransform.rotation = Quaternion.Euler(0, 0, -_player.transform.eulerAngles.y);
                    break;
                case FollowPlayer.FollowDontRotate:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    break;
                case FollowPlayer.DontFollowDontRotate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
        #region Public API
        //simple public method to change the Arrow/Map Behaviour from outside
        public void ChangeFollowState(FollowPlayer state)
        {
            _followingState = state;
            Hook();
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
            MapCamera.orthographicSize = MapCamera.orthographicSize + 5;
        }
        public void Unzoom()
        {
            MapCamera.orthographicSize = MapCamera.orthographicSize - 5;
        }
        public void SetSize(float size)
        {
            if (size <= 0) return;
            var cam = MapCamera.GetComponent<Camera>();
            cam.orthographicSize = size;
        }
        public void SetMinimapType(MinimapType type)
        {
            switch (type)
            {
                case MinimapType.Static:
                    MapCamera.cullingMask = LayerMask.GetMask("StaticMap");
                    break;
                case MinimapType.Schematic:
                    MapCamera.cullingMask = LayerMask.GetMask("SchematicMap");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
        #endregion
        #region Helper functions
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
        private GameObject GetArrow()
        {
            //GameObject go = transform.Find("Map-Arrow").gameObject;
            var go = GameObject.Find("Map-Arrow");
            if (go != null) return go;
            Debug.LogError("Map-Arrow game object is not part of the prefab.");
            Debug.Break();
            return null;
        }
        private void ShowArrow(bool bo)
        {
            _mapArrow.GetComponent<Image>().sprite = bo ? DirrectionalArrow : LocationMark;
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
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    MapCamera.transform.position = new Vector3(0, 0, 0);
                    _mapArrow.GetComponent<Image>().enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MapController))]
    public class GuiMapEditor : Editor
    {
        private MapController.FollowPlayer _followState;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var map = (MapController) target;
            map.Type = (MapController.MinimapType) EditorGUILayout.EnumPopup(map.Type);
            switch (map.Type)
            {
                case MapController.MinimapType.Schematic:
                    map.MapCamera.cullingMask = LayerMask.GetMask("StaticMap");
                    break;
                case MapController.MinimapType.Static:
                    map.MapCamera.cullingMask = LayerMask.GetMask("SchematicMap");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _followState = (MapController.FollowPlayer) EditorGUILayout.EnumPopup(_followState);
            map.ChangeFollowState(_followState);

            map.MapCamera.orthographicSize = EditorGUILayout.FloatField("Camera size:", map.MapCamera.orthographicSize);
            if (GUILayout.Button("+")) map.MapCamera.orthographicSize -= 5;
            if (GUILayout.Button("-")) map.MapCamera.orthographicSize += 5;
        }
    }
#endif
}