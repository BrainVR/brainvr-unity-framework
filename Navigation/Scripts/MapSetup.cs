#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public class MapSetup : MonoBehaviour
    {
        public GameObject CubeMesh;
        public Material BuildingMaterial;
        //public Transform customObject1, customObject2, customObject3;
        //public Material customMat1, customMat2, customMat3;

        [System.Serializable]
        public class SpecificMapObjects
        {
            public Transform Obj;
            public Color Color;
        }
        public SpecificMapObjects[] SpecificObjects;
    
        public void ProcessCustomObject(Transform o, Color c, Material mat)
        {
            var go = Instantiate(o.gameObject, o.transform.position, o.transform.rotation);
            //removes all colliders
            var colliders = go.GetComponentsInChildren<Collider>(true);
            foreach (var col in colliders)
            {
                DestroyImmediate(col);
            }
            go.transform.SetParent(transform.Find("SCHEMATIC_MAP/OTHER"));
            var layer = transform.Find("SCHEMATIC_MAP/OTHER").gameObject.layer;
            go.transform.SetLayerRecursively(layer);
            go.name = o.name+"__schematic";
            mat.color = c;
            foreach (var r in go.GetComponentsInChildren<Renderer>())
            {
                foreach (var material in r.materials)
                {
                    material.shader=mat.shader;
                    material.CopyPropertiesFromMaterial(mat);
                }
            }
        }
        public void ProcessBuilding(Transform b) {
       
            if (!b.GetComponent<BoxCollider>()) {
                Debug.LogWarning("Building" + b.name+ " has no collider");
                return;
            }

            foreach (var col in b.GetComponents<BoxCollider>()) {

                var go = Instantiate(CubeMesh, col.bounds.center, b.rotation);

                go.transform.localScale = Vector3.Scale(col.size, b.localScale);
                go.transform.SetParent(transform.Find("SCHEMATIC_MAP/BUILDINGS"));
                go.name = b.name+"__cube";		 
            }
        }
        public void Clear(string placeholder)
        {
            foreach (Transform item in transform.Find(placeholder))
                DestroyImmediate(item.gameObject);
        }
        public void GenerateSchematicCustomObjects() 
        {
            var mat = new Material(Shader.Find("Unlit/Color"));
            foreach (var smo in SpecificObjects)
                if (smo.Obj) ProcessCustomObject(smo.Obj, smo.Color, mat);
        }
        public void GenerateSchematicBuildings()
        {
            var buildings = GameObject.FindGameObjectsWithTag("Building");
            foreach (var item in buildings)
                ProcessCustomObject(item.transform, MapGeneratorEditor.BuldingsColor, BuildingMaterial);
        }
        public void GenerateMap_old()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            ScreenCapture.CaptureScreenshot("Screenshot.png",1);
            /*
            transform.GetChild(0).gameObject.SetActive(false);
            lastActiveCam.gameObject.SetActive(true);
            */
        }
    }
    [CustomEditor(typeof(MapSetup))]
    public class MapGeneratorEditor : Editor
    {

        public static Color BuldingsColor;
        MapSetup _myScript;

        public void OnEnable()
        {
            _myScript = (MapSetup)target;
            BuldingsColor = _myScript.CubeMesh.GetComponent<Renderer>().sharedMaterial.color;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BuldingsColor = EditorGUILayout.ColorField("Buldings Color", BuldingsColor);
            _myScript.CubeMesh.GetComponent<Renderer>().sharedMaterial.color = BuldingsColor;
            if (!GUILayout.Button("Update")) return;
            _myScript.Clear("SCHEMATIC_MAP/BUILDINGS");
            _myScript.Clear("SCHEMATIC_MAP/OTHER");
            _myScript.GenerateSchematicBuildings();
            _myScript.GenerateSchematicCustomObjects();
        }
    }
}
#endif