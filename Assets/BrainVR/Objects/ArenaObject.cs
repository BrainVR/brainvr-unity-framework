using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BrainVR.UnityFramework.Objects
{
    public class ArenaObject : MonoBehaviour
    {
        public GameObject ActiveObject;
        public GameObject[] PossibleObjects;
        private Color _originalColor;

        private IEnumerator rotation;

        #region Monobehaviour
        void OnEnable()
        {
            if (ActiveObject == null)
            {
                if (PossibleObjects.Length == 0) throw new MissingComponentException(name + " object missing list of possible game prefabs");
                SetObject(PossibleObjects[0]);
            }
            if (ActiveObject.GetComponent<Renderer>() != null) _originalColor = ActiveObject.GetComponent<Renderer>().material.GetColor("_Color");
            else Debug.Log("Object does not have direct colour attached");
        }
            
        #endregion
        #region Information
        /// <summary>
        /// returns positon in world coordinates ignoring Y axis
        /// </summary>
        /// <returns>Vector2 coordinates (X, Z)</returns>
        public Vector2 GetVector2Position()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
        #endregion
        #region Manipulation
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        public void StartRotation(Vector3 direction, float speed)
        {
            StopRotation();
            rotation = Rotate(direction, speed);
            StartCoroutine(rotation);
        }
        public void StopRotation()
        {
            if(rotation != null) StopCoroutine(rotation);
        }
        public void SetSize(Vector3 scale)
        {
            ActiveObject.transform.localScale = scale;
        }
        public void SetColor(Color color)
        {
            if (!HasColour()) return;
            var mat = ActiveObject.GetComponent<Renderer>().material;
            _originalColor = mat.GetColor("_Color");
            mat.SetColor("_Color", color);
        }
        public void ResetColor()
        {   
            if (!HasColour()) return;
            var mat = ActiveObject.GetComponent<Renderer>().material;
            mat.SetColor("_Color", _originalColor);
        }
        public void SetType(string s, bool force = false)
        {
            try
            {
                SetObject(s, force);
            }
            catch (KeyNotFoundException ex)
            {
                Debug.Log("Exception" + ex + "raised. Object of name " + s + " does not exist in " + name + ", instantiating default");
                //instantiates defualt
            }
        }
        public bool Switch()
        {
            bool toState = !ActiveObject.GetComponent<Renderer>().enabled;
            ActiveObject.GetComponent<Renderer>().enabled = toState;
            return toState;
        }
        public void Show(bool bo = true)
        {
            foreach (var renderer in ActiveObject.GetComponentsInChildren<Renderer>())
                renderer.enabled = bo;
            foreach (var collider in ActiveObject.GetComponentsInChildren<Collider>())
                if (!collider.isTrigger) collider.enabled = bo;
        }
        public void Hide()
        {
            Show(false);
        }
        #endregion
        #region private helpers
        private void SetObject(GameObject go, bool force = false)
        {
            InstantiateActiveGameObject(go, force);
        }
        private void SetObject(string s, bool force = false)
        {
            //this doesnt work as intended
            GameObject go = PossibleObjects.SingleOrDefault(obj => obj.name == s);
            if (go == null) throw new KeyNotFoundException();
            InstantiateActiveGameObject(go, force);
        }
        private void SetObject(int i, bool force = false)
        {
            if (i > PossibleObjects.Length - 1) throw new MissingReferenceException();
            InstantiateActiveGameObject(PossibleObjects[i], force);
        }
        private void InstantiateActiveGameObject(GameObject go, bool force)
        {
            //if the tem is still the same and we don't want to force reinstantiate
            if (go == ActiveObject & !force) return;
            CheckExistence();
            ActiveObject = Instantiate(go);
            SetActiveObjectTransforms();
        }
        private void SetActiveObjectTransforms()
        {
            Quaternion originalQuaternion = ActiveObject.transform.rotation;
            Vector3 originalPosition = ActiveObject.transform.position;
            ActiveObject.transform.SetParent(transform);
            ActiveObject.transform.localPosition = originalPosition;
            ActiveObject.transform.localRotation = originalQuaternion;
        }
        private bool CheckExistence()
        {
            if (ActiveObject != null)
            {
                Destroy(ActiveObject);
                return true;
            } return false;

        }
        private bool HasColour()
        {
            if (ActiveObject.GetComponent<Renderer>() == null)
            {
                Debug.Log("Object does not have direct colour attached");
                return false;
            }
            return true;
        }
        private IEnumerator Rotate(Vector3 dir, float speed)
        {
            while (true)
            {
                transform.Rotate(dir, speed * Time.deltaTime);
                yield return null;
            }
        }
        #endregion 
    }
}
