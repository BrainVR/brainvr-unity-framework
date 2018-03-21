#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
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
            public GameObject go;
            public Color Color;
        }

        public GameObject TerrainObject;
        public List<Color> TerrainColors;

        public TaggedObject[] TaggedObjects;
        public SpecificMapObject[] SpecificObjects;

        public Texture StaticImage;
        #region Public API

        public void CreateSchematicMap()
        {
            ClearSchematicMap();
            ProcessTerrain();
            ProcessTaggedObjects();
            ProcessSpecificObjects();
            SetSchematicLayer();
        }
        public void SetStaticTexture()
        {
            var go = transform.Find(StaticMap);
            var terrainSize = Terrain.activeTerrain.terrainData.size;
            terrainSize.y = -100;
            go.transform.localScale = new Vector3(terrainSize.x/10, 1, terrainSize.z/10);
            go.position = terrainSize / 2;
            var rend = go.GetComponent<Renderer>();
            var mat = new Material(Shader.Find("Unlit/Texture")) {mainTexture = StaticImage};
            rend.material = mat;
        }
        //TODO - deosn't delete all. Probly editor specific :(
        public void ClearSchematicMap()
        {
            var parent = transform.Find(SchematicPath);
            foreach (Transform item in parent)
                DestroyImmediate(item.gameObject);
        }
        #endregion
        #region Private functions
        #region Processing objects
        private void ProcessTerrain()
        {
            if (TerrainObject == null) TerrainObject = Terrain.activeTerrain.gameObject;
            if (TerrainObject == null)
            {
                Debug.Log("There is no main terrain object assigned and no terrain in the scene.");
                return;
            }

            var go = new GameObject {name = "Schematic Terrain"};
            go.transform.SetParent(transform.Find(SchematicPath)); /// needs before assignmnt of position because of hte Y coordinate
            var position = TerrainObject.transform.position;
            position.y = TerrainObject.transform.position.y + transform.Find(SchematicPath).position.y - 1; //terrain height works a bit differently
            go.transform.localPosition = position;

            var terrain = go.AddComponent<Terrain>();
            var oldTerrainData = Instantiate(TerrainObject.GetComponent<Terrain>().terrainData);
            terrain.terrainData = oldTerrainData;

            var tc = terrain.gameObject.AddComponent<TerrainCollider>();
            tc.terrainData = terrain.terrainData;

            //removes trees and vegetation
            terrain.terrainData.treeInstances = new TreeInstance[0];
            var emptyDetail = new int[terrain.terrainData.detailWidth, terrain.terrainData.detailHeight];

            //Removes all details
            if (terrain.terrainData.detailPrototypes.Length > 0)
            {
                for (var iDetail = 0; iDetail < terrain.terrainData.detailPrototypes.Length; iDetail++)
                    terrain.terrainData.SetDetailLayer(0, 0, iDetail, emptyDetail);
            }

            terrain.terrainData.treePrototypes = new TreePrototype[0];
            terrain.terrainData.detailPrototypes = new DetailPrototype[0];

            //sets colors
            terrain.materialType = Terrain.MaterialType.Custom;
            terrain.materialTemplate = new Material(Shader.Find("Unlit/Texture"));
            var splats = new List<SplatPrototype>();
            for (var i = 0; i < terrain.terrainData.alphamapLayers; i++)
            {
                var color = TerrainColors.Count >= i + 1 ? TerrainColors[i] : TerrainColors.Last();
                splats.Add(new SplatPrototype { texture = CreateTerrainTexture(color) });
            }
            terrain.terrainData.splatPrototypes = splats.ToArray();
            //SETS heights
            var height = oldTerrainData.heightmapHeight;
            var width = oldTerrainData.heightmapWidth;
            terrain.terrainData.SetHeights(0,0, oldTerrainData.GetHeights(0,0, width, height));
        }
        private void ProcessTaggedObjects()
        {
            foreach (var tagName in TaggedObjects)
            {
                //find all the objects
                var material = CreateSchematicMaterial(tagName.Color);
                var objects = GameObject.FindGameObjectsWithTag(tagName.Tag);
                //creates parent game objects
                var go = new GameObject { name = tagName.Tag };
                SetSchematicParent(go);
                foreach (var obj in objects)
                    ProcessObject(obj, material, tagName.Tag);
            }
        }
        private void ProcessSpecificObjects()
        {
            const string goName = "Specific";
            var go = new GameObject { name = goName };
            SetSchematicParent(go);
            foreach (var obj in SpecificObjects)
            {
                var material = CreateSchematicMaterial(obj.Color);
                go.transform.SetParent(transform.Find(SchematicPath));
                ProcessObject(obj.go, material, goName);
            }
        }
        private void ProcessObject(GameObject obj, Material material, string type)
        {
            var go = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            RemoveColliders(go);
            var objectPath = SchematicPath + "/" + type;
            go.transform.SetParent(transform.Find(objectPath));
            go.transform.localPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + go.transform.parent.position.y, obj.transform.position.z); //instantiates in the relative y coordinates
            go.name = "schematic_" + obj.name;
            foreach (var r in go.GetComponentsInChildren<Renderer>())
            {
                var mats = new Material[r.sharedMaterials.Length];
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i] = material;
                }
                r.sharedMaterials = mats;
            }
        }
        #endregion
        #region helpers
        private void SetSchematicParent(GameObject go)
        {
            go.transform.SetParent(transform.Find(SchematicPath));
            go.transform.localPosition = default(Vector3);
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
        private Texture2D CreateTerrainTexture(Color color)
        {
            var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.SetPixel(1, 1, color);
            return texture;
        }
        #endregion

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
                _myScript.CreateSchematicMap();
            }
            if (GUILayout.Button("Clear"))
            {
                _myScript.ClearSchematicMap();
            }
            if (GUILayout.Button("Set Static Image"))
            {
                _myScript.SetStaticTexture();
            }

        }
    }
}
#endif