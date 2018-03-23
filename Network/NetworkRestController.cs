using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PlayerController = BrainVR.UnityFramework.Player.PlayerController;

namespace BrainVR.UnityFramework.Networking
{
    public class NetworkRestController : Singleton<NetworkRestController>
    {
        [SerializeField]
        private string _url = "http://localhost";

        [SerializeField]
        private int _port = 80;

        private IEnumerator _playerSender;

        public void StartSendingPlayerInformation()
        {
            _playerSender = ContinuousPlayerPost(0.25f);
            StartCoroutine(_playerSender);
        }

        public void StopSendingPlayerInformation()
        {
            if(_playerSender != null) StopCoroutine(_playerSender);
        }

        public IEnumerator SendPost(Dictionary<string, string> dict)
        {
            var form = new WWWForm();
            foreach (var di in dict)
            {
                form.AddField(di.Key, di.Value);
            }
            var address = _url + ":" + _port;
            // Upload to a cgi script
            var w = UnityWebRequest.Post(address, form);
            yield return w.Send();

            if (w.isNetworkError || w.isHttpError) Debug.Log(w.error);
            else Debug.Log("Finished POST");
        }

        private IEnumerator ContinuousPlayerPost(float time)
        {
            while (true)
            {
                var playerInformation = PlayerController.Instance.PlayerInformation().ToArray();
                var postContent = string.Join(";", playerInformation);
                StartCoroutine(SendPost(new Dictionary<string, string> { { "player", postContent } }));
                yield return new WaitForSeconds(time);
            }
        }
    }
}
