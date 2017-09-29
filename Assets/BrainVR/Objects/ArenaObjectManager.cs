﻿using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.Helpers;
using BrainVR.UnityFramework.Objects;
using BrainVR.UnityFramework.Objects.Goals;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BrainVR.UnityFramework.Objects
{
    public class AreaObjectManager<T> : Singleton<T> where T : MonoBehaviour
    {
        public List<ArenaObject> Objects = new List<ArenaObject>();
        public void SetColor(Color color)
        {
            foreach (var controller in Objects)
                controller.SetColor(color);
        }
        public void ResetColor()
        {
            foreach (var controller in Objects)
                controller.ResetColor();
        }
        public void SetType(string s)
        {
            foreach (var controller in Objects)
                controller.SetType(s);
        }
        public void ShowAll(bool bo = true)
        {
            foreach (var obj in Objects)
                obj.Show(bo);
        }
        public void HideAll()
        {
            ShowAll(false);
        }
        public void MoveObjectsCircumference(int number, int[] positions, float radius = 15, Vector3 center = default(Vector3))
        {
            //validations
            if (positions.Max() > number - 1)
            {
                Debug.Log("Cannot move goals on positions higher than number of goals");
                return;
            }
            var angle = Mathf.PI * 2 / number;
            for (var i = 0; i < Objects.Count; i++)
            {
                var obj = Objects[i];
                var newPosition = InstantiatingHelper.GetWorldPositionFromVector2(MathHelper.GetCirclePoint(positions[i] * angle, radius), center);
                obj.SetPosition(new Vector3(newPosition.x, obj.transform.position.y, newPosition.z));
                //ooking into the center
                obj.SetRotation(Quaternion.LookRotation(new Vector3(0, obj.transform.position.y, 0) - obj.transform.position));
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AreaObjectManager<>), true)]
public class ArenaObjectManagerInspector : Editor
{
    private SerializedProperty _objects;
    void OnEnable()
    {
        // Setup the SerializedProperties
        _objects = serializedObject.FindProperty("Objects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (_objects.arraySize > 100)
        {
            string[] excludes = {"Objects"};
            DrawPropertiesExcluding(serializedObject, excludes);
            GUILayout.Label("There is too many objects. You need to delete and reinstantiate object.");
        }
        else
        {
            GUILayout.Label("If you enter more than 100 objects, following edits in Unity eitor will be disabled. Many objects in lists slow the Editor.");
            DrawDefaultInspector();
        }
    }
}
#endif