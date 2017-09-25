using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.UI
{
    public class CanvasTextController : MonoBehaviour
    {
        public string FieldName;

        private string _value;
        private string _name;
        private CanvasGroup canvas;
        public Text NameField;
        public Text ValueField;

        void Start()
        {
            gameObject.name = FieldName;
            _name = "";
            _value = "";
            canvas = GetComponent<CanvasGroup>();
        }
        #region Public API
        public void Show()
        {
            if (NameField != null) NameField.text = _name;
            ValueField.text = _value;
            canvas.alpha = 1f;
        }
        public void Hide()
        {
            canvas.alpha = 0f;
        }
        public void Clear()
        {
            NameField.text = "";
            ValueField.text = "";
        }
        public void FadeOut(float duration = 1.0f)
        {
            //DOTween.(() => cubeB.position, x => cubeB.position = x, new Vector3(-2, 2, 0), 1).SetRelative().SetLoops(-1, LoopType.Yoyo);
            //ValueField.DO()
        }
        public void SetTextValue(string text)
        {
            _value = text;
            Show();
        }
        public void SetTextName(string text)
        {
            _name = text;
            Show();
        }
        #endregion
    }
}
