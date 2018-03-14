#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public class MapSetup : MonoBehaviour
    {
        private const string SchematicPath = "SCHEMATIC_MAP";
        private const string StaticMap = "STATIC_MAP";
        [System.Serializable]
        public class TaggedObject
        {
            public string Tag;
            public Color Color;
        }
        [System.Serializable]
        public class SpecificMapObject
        {
            public Transform Obj;
            public Color Color;
        }

        public TaggedObject[] TaggedObjects;
        public SpecificMapObject[] SpecificObject;

        public Texture StaticImage;

        #region Public API
        public void ProcessTaggedObjects()
        {
            foreach (var tagName in TaggedObjects)
            {
                //find all the objects
                var material = CreateSchematicMaterial(tagName.Color);
                var objects = GameObject.FindGameObjectsWithTag(tagName.Tag);
                //creates parent game objects
                var go = new GameObject{name = tagName.Tag};
                go.transform.SetParent(transform.Find(SchematicPath));
                go.transform.localPosition = default(Vector3);
                foreach (var obj in objects)
                    ProcessObject(obj, material, tagName.Tag);
            }
            SetSchematicLayer();
        }

        public void SetStaticTexture()
        {
            var go = transform.Find(StaticMap);
            var terrainSize = Terrain.activeTerrain.terrainData.size;
            terrainSize.y = -100;
            go.transform.localScale = terrainSize/10;
            go.position = terrainSize / 2;
            var rend = go.GetComponent<Renderer>();
            var mat = new Material(Shader.Find("Unlit/Texture")) {mainTexture = StaticImage};
            rend.material = mat;
        }
        public void Clear(string placeholder)
        {
            foreach (Transform item in transform.Find(placeholder))
                DestroyImmediate(item.gameObject);
        }
        #endregion
        #region Private functions
        private void ProcessObject(GameObject obj, Material material, string type)
        {
            var go = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            RemoveColliders(go);
            var objectPath = SchematicPath + "/" + type;
            go.transform.SetParent(transform.Find(objectPath));
            go.transform.localPosition = new Vector3(obj.transform.position.x, 0, obj.transform.position.z); //instantiates in the same Y coordinates as schematic map -* flatten the map
            go.name = "schematic_" + obj.name;
            foreach (var r in go.GetComponentsInChildren<Renderer>())
                r.material = material;
        }
        private LayerMask GetObjectLayer(string goName)
        {
            var layer = transform.Find(goName).gameObject.layer;
            return layer;
        }
        private void SetSchematicLayer()
        {
            var go = transform.Find(SchematicPath);
            go.transform.SetLayerRecursively(GetObjectLayer(SchematicPath));
        }
        public GameObject RemoveColliders(GameObject go)
        {
            var colliders = go.GetComponentsInChildren<Collider>(true);
            foreach (var col in colliders)
            {
                DestroyImmediate(col);
            }
            return go;
        }
        private static Material CreateSchematicMaterial(Color color)
        {
            return new Material(Shader.Find("Unlit/Color")) { color = color };
        }
        #endregion
    }
    [CustomEditor(typeof(MapSetup))]
    public class MapGeneratorEditor : Editor
    {
        private MapSetup _myScript;
        public void OnEnable()
        {
            _myScript = (MapSetup)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Update"))
            {
                _myScript.Clear("SCHEMATIC_MAP");
                _myScript.ProcessTaggedObjects();
            }
            if (GUILayout.Button("Clear"))
            {
                _myScript.Clear("SCHEMATIC_MAP");
            }
            if (GUILayout.Button("Set Static Image"))
            {
                _myScript.SetStaticTexture();
            }

        }
    }
}
#endif