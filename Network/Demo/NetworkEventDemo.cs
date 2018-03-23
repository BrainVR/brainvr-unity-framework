using System.Collections.Generic;
using UnityEngine;

namespace BrainVR.UnityFramework.Networking
{
    public class NetworkEventDemo : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3)) SendTestInfo();
            if (Input.GetKeyDown(KeyCode.F4)) NetworkRestController.Instance.StartSendingPlayerInformation();
            if (Input.GetKeyDown(KeyCode.F5)) NetworkRestController.Instance.StopSendingPlayerInformation();
        }

        public void SendTestInfo()
        { 
            var test = new Dictionary<string, string> { { "key", "value" }, { "secondKey", "secondValue" } };
            StartCoroutine(NetworkRestController.Instance.SendPost(test));
        }

        void OnTriggerEnter()
        {
            var test = new Dictionary<string, string> { { "event", "Player entered zone" } };
            StartCoroutine(NetworkRestController.Instance.SendPost(test));
        }

        void OnTriggerExit()
        {
            var test = new Dictionary<string, string> { { "event", "Player left zone" } };
            StartCoroutine(NetworkRestController.Instance.SendPost(test));
        }
    }
}
