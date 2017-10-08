using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.Crosshair
{
    public class CrosshairController : Singleton<CrosshairController>
    {
        public GameObject Canvas;
        public Image Image;
        public List<Sprite> PossibleImages = new List<Sprite>();
        private Color _defaultColor;

        void OnEnable()
        {
            _defaultColor = Image.color;
            SetImage(0);
            //Resize(new Vector2(10,10));
        }
        #region Public API
        public void Show()
        {
            Canvas.SetActive(true);
        }
        public void Hide()
        {
            Canvas.SetActive(false);
        }
        public void GameUpdate()
        {
            //does nothing
        }
        public void SetColour(Color color)
        {
            Image.color = color;
        }
        public void ResetColor()
        {
            Image.color = _defaultColor;
        }
        public void SetImage(int i)
        {
            Image.sprite = PossibleImages[i];
        }
        // Iterates through´images and selects the one 
        public void SetImage(string str)
        {
            int i  = PossibleImages.FindIndex(e => e.name == str);
            if (i >= 0) SetImage(i);
            else Debug.Log("No crosshair image " + str);
        }
        public void Resize(Vector2 size)
        {
            Image.rectTransform.sizeDelta = size;
        }
        #endregion
    }
}
