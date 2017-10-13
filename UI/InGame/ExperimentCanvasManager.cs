using System.Collections.Generic;
using UnityEngine;

namespace BrainVR.UnityFramework.UI.InGame
{
    public class ExperimentCanvasManager : Singleton<ExperimentCanvasManager>
    {
        private Dictionary<string, CanvasTextController> fieldDictionary = new Dictionary<string, CanvasTextController>();
        private Canvas _canvas;

        #region Monobehaviour
        void Awake ()
        {
            _canvas = GetComponentInChildren<Canvas>();
            //check if GOs are not null
            UpdateCanvasControllers();
            Show(false);
        }
        #endregion
        #region Public API
        public void SetText(string fieldName, string text)
        {
            var field = GetField(fieldName);
            if (field) field.SetTextValue(text);
        }
        public void SetName(string fieldName, string text)
        {
            var field = GetField(fieldName);
            if (field) field.SetTextName(text);
        }
        public void ShowField(string fieldName)
        {
            var field = GetField(fieldName);
            if (field) field.Show();
        }
        public void HideField(string fieldName)
        {
            var field = GetField(fieldName);
            if (field) field.Hide();
        }
        public void Show(bool bo = true)
        {
            _canvas.enabled = bo;
        }
        #endregion
        #region private helpers
        private CanvasTextController GetField(string field)
        {
            if (fieldDictionary.Count == 0) return null;
            try
            {
                var fl = fieldDictionary[field];
                return fl;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("Key " + field + " not foudn in the Canvas");
                return null;
            }
        }
        private void UpdateCanvasControllers()
        {
            var controllers = GetComponentsInChildren<CanvasTextController>();
            foreach (var child in controllers)
            {
                fieldDictionary.Add(child.FieldName, child);
            }
        }
        #endregion
    }
}