#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public class MapGenerator : MonoBehaviour
    {
      
        public GameObject CubeMesh;
        public Material BuildingMaterial;
        //public Transform customObject1, customObject2, customObject3;
        //public Material customMat1, customMat2, customMat3;

        [System.Serializable]
        public class SpecificMapObjects
        {
            public Transform obj;
            public Color color;
        
        }
        public SpecificMapObjects[] specificObjects;
    
        public void ProcessCustomObject(Transform o, Color c, Material mat) {

            GameObject go = Instantiate(o.gameObject, o.transform.position, o.transform.rotation) as GameObject;
            //removes all colliders
            Component[] colliders = go.GetComponentsInChildren<Collider>(true);
            foreach (Component collider in colliders)
            {
                DestroyImmediate(collider);
            }
            go.transform.SetParent(transform.Find("SCHEMATIC_MAP/OTHER"));
            int layer = transform.Find("SCHEMATIC_MAP/OTHER").gameObject.layer;
            go.transform.SetLayerRecursively(layer);
            go.name = o.name+"__schematic";
            mat.color = c;
            foreach (Renderer r in go.GetComponentsInChildren<Renderer>()) {
                for (int i=0; i < r.materials.Length; i++) {
                    r.materials[i].shader=mat.shader;
                    r.materials[i].CopyPropertiesFromMaterial(mat);
                }
            }
        }

        public void ProcessBuilding(Transform b) {
       
            if (!b.GetComponent<BoxCollider>()) {
                Debug.LogWarning("Building" + b.name+ " has no collider");
                return;
            }

            foreach (BoxCollider coll in b.GetComponents<BoxCollider>()) {

                GameObject go = Instantiate(CubeMesh, coll.bounds.center, b.rotation) as GameObject;

                go.transform.localScale = Vector3.Scale(coll.size, b.localScale);
                go.transform.SetParent(transform.Find("SCHEMATIC_MAP/BUILDINGS"));
                go.name = b.name+"__cube";		 
            } 
        }

        public void Clear(string placeholder) {
            foreach (Transform item in transform.Find(placeholder)) {
                DestroyImmediate(item.gameObject);
            }
        }

        public void GenerateSchematicCustomObjects() {
            Material mat = new Material(Shader.Find("Unlit/Color"));
            foreach (SpecificMapObjects smo in specificObjects)
                if (smo.obj)
                    ProcessCustomObject(smo.obj, smo.color, mat);
        }

        public void GenerateSchematicBuildings() {

            GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

            foreach (GameObject item in buildings) {
                //print("iname:"+item.name);
                ProcessCustomObject(item.transform, MapGeneratorEditor.buldingsColor, BuildingMaterial);
            }       
        }
        Camera lastActiveCam;
        public void GenerateMap_old() {

            lastActiveCam=Camera.main;
            transform.GetChild(0).gameObject.SetActive(true);

            print("button pressed");

            ScreenCapture.CaptureScreenshot("Screenshot.png",1);
            /*
        transform.GetChild(0).gameObject.SetActive(false);
        lastActiveCam.gameObject.SetActive(true);*/
        }
    }
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {

        public static Color buldingsColor;

        MapGenerator myScript;

        public void OnEnable()
        {
            myScript = (MapGenerator)target;
            buldingsColor = myScript.CubeMesh.GetComponent<Renderer>().sharedMaterial.color;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            buldingsColor = EditorGUILayout.ColorField("Buldings Color", buldingsColor);
            myScript.CubeMesh.GetComponent<Renderer>().sharedMaterial.color = buldingsColor;
            if (!GUILayout.Button("Update")) return;
            myScript.Clear("SCHEMATIC_MAP/BUILDINGS");
            myScript.Clear("SCHEMATIC_MAP/OTHER");
            myScript.GenerateSchematicBuildings();
            myScript.GenerateSchematicCustomObjects();
        }
    }
}
#endif