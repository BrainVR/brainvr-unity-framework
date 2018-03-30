using System.Collections.Generic;
using UnityEngine;

namespace BrainVR.UnityFramework.Networking
{
    public class NetworkEventDemo : MonoBehaviour
    {

        void Start()
        {
            NetworkRestController.Instance.SendCustomInfo("map", "size", Terrain.activeTerrain.terrainData.size.ToString("N1"));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4)) NetworkRestController.Instance.StartSendingPlayerInformation();
            if (Input.GetKeyDown(KeyCode.F5)) NetworkRestController.Instance.StopSendingPlayerInformation();
        }

        void OnTriggerEnter()
        {
            NetworkRestController.Instance.SendEvent("Player entered zone");
        }

        void OnTriggerExit()
        {
            NetworkRestController.Instance.SendEvent("Player left zone");
        }
    }
}
