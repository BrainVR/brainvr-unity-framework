using UnityEngine;

namespace BrainVR.UnityFramework.Helpers
{
    public static class SearchingHelper {

        public static GameObject GetPlayer()
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go == null)
            {
                Debug.Log("Couldn't find player");
                return(null);
            } return (go);
        }

        public static T GetSomething<T>(string gameobjectName) where T : class
        {
            GameObject go = GameObject.Find(gameobjectName);
            if (go == null)
            {
                Debug.Log("Couldn't find gameobject " + gameobjectName);
                return (null);
            }
            T wantedClass = go.GetComponent<T>();
            if (wantedClass == null)
            {
                Debug.Log("Couldn't find the class attached");
                return (null);
            }
            return (wantedClass);
        }
    }
}
