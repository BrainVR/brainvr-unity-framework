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
        public enum MinimapBehaviour
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
        public GameObject MapArrow;
        public MinimapType Type;
        public GameObject Map;
        public Sprite DirrectionalArrow;
        public Sprite LocationMark;

        private GameObject _player;

        private RectTransform _mapArrowTransform;
        public MinimapBehaviour FollowingState = MinimapBehaviour.FollowRotate;
        private LineRenderer _lineRenderer;

        #region MonoBehaviour
        void OnEnable()
        {
            if (_player == null) _player = PlayerController.Instance.gameObject;
            //TODO - problematic
            _mapArrowTransform = MapArrow.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
            _lineRenderer.positionCount = 1;
        }
        void Update()
        {
            switch (FollowingState)
            {
                case MinimapBehaviour.FollowRotate:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    Quaternion rotation = Quaternion.Euler(90, _player.transform.eulerAngles.y,
                        _player.transform.eulerAngles.z);
                    MapCamera.transform.rotation = rotation;
                    break;
                case MinimapBehaviour.FollowRotateArrow:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    //the minus in the transform is important
                    //the player rotates around y axis in the world, but the arrow transform is in the Z axis in the canvas
                    _mapArrowTransform.rotation = Quaternion.Euler(0, 0, -_player.transform.eulerAngles.y);
                    break;
                case MinimapBehaviour.FollowDontRotate:
                    MapCamera.transform.position = _player.transform.position + Vector3.up * 100;
                    break;
                case MinimapBehaviour.DontFollowDontRotate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
        #region Public API
        //simple public method to change the Arrow/Map Behaviour from outside
        public void SetBehaviour(MinimapBehaviour state)
        {
            FollowingState = state;
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
                    _lineRenderer.gameObject.layer = LayerMask.NameToLayer("StaticMap");
                    break;
                case MinimapType.Schematic:
                    MapCamera.cullingMask = LayerMask.GetMask("SchematicMap");
                    _lineRenderer.gameObject.layer = LayerMask.NameToLayer("SchematicMap");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
        public void ClearPath()
        {
            _lineRenderer.positionCount = 1;
        }
        public void DrawPath(Vector3[] path)
        {
            //TODO - Needs afixin
            var positionCount = path.Length + 1; //set the array of positions to the amount of corners
            _lineRenderer.SetPosition(0, PlayerController.Instance.transform.position);
            //we don't redraw this until its needed. - until position count doesn't change we are still looking only for the line to the first "node"
            if (_lineRenderer.positionCount == positionCount) return;
            _lineRenderer.positionCount = positionCount;
            for (var i = 1; i < positionCount; i++)
            {
                _lineRenderer.SetPosition(i, path[i-1]); //go through each corner and set that to the line renderer's position
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
        private void ShowArrow(bool bo)
        {
            MapArrow.GetComponent<Image>().sprite = bo ? DirrectionalArrow : LocationMark;
        }
        //This method takes care of the necessary changes that follow changing of states
        //For example, whn we dont Follow and don't rotate, we want to set the Camera position once and for all
        //so it wouldn't change in each Update
        private void Hook()
        {
            switch (FollowingState)
            {
                case MinimapBehaviour.FollowRotate:
                    ShowArrow(true);
                    _mapArrowTransform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case MinimapBehaviour.FollowRotateArrow:
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    ShowArrow(true);
                    break;
                case MinimapBehaviour.FollowDontRotate:
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    ShowArrow(false);
                    break;
                case MinimapBehaviour.DontFollowDontRotate:
                    MapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                    MapCamera.transform.position = new Vector3(0, 0, 0);
                    MapArrow.GetComponent<Image>().enabled = false;
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
        private MapController.MinimapBehaviour _followState;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var map = (MapController) target;
            map.Type = (MapController.MinimapType) EditorGUILayout.EnumPopup(map.Type);
            switch (map.Type)
            {
                case MapController.MinimapType.Schematic:
                    map.MapCamera.cullingMask = LayerMask.GetMask("SchematicMap");
                    break;
                case MapController.MinimapType.Static:
                    map.MapCamera.cullingMask = LayerMask.GetMask("StaticMap");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _followState = (MapController.MinimapBehaviour) EditorGUILayout.EnumPopup(_followState);
            map.SetBehaviour(_followState);
            map.MapCamera.orthographicSize = EditorGUILayout.FloatField("Map size:", map.MapCamera.orthographicSize);
            if (GUILayout.Button("+")) map.MapCamera.orthographicSize -= 5;
            if (GUILayout.Button("-")) map.MapCamera.orthographicSize += 5;
        }
    }
#endif
}