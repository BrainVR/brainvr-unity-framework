using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityLogger;
using UnityEngine;
using UnityEngine.Networking;
using PlayerController = BrainVR.UnityFramework.Player.PlayerController;

namespace BrainVR.UnityFramework.Networking
{
    #region Data classes

    public class DataPacket
    {
        private string _experiment;
        private Dictionary<string, string> _defaultDictionary;

        public DataPacket(string code,  string experiment)
        {
            ParticipantCode = code;
            _experiment = experiment;
            _defaultDictionary = new Dictionary<string, string>
            {
                {"code", code},
                {"experiment", experiment}
            };
        }

        public string ParticipantCode { get; private set; }

        public WWWForm CustomForm(string type, string key, string value)
        {
            var form = CreateCustomForm(type, key, value);
            return CreateForm(form);
        }
        public WWWForm EventForm(string eventName)
        {
            var eventDict = CreateCustomForm("event", "name", eventName);
            return CreateForm(eventDict);
        }
        public WWWForm PlayerForm(Dictionary<string, string> dict)
        {
            var playerDict = _defaultDictionary.Union(dict).ToDictionary(k => k.Key, v => v.Value);
            playerDict.Add("type", "player");
            return CreateForm(playerDict);
        }
        private static WWWForm CreateForm(Dictionary<string, string> dict)
        {
            var form = new WWWForm();
            foreach (var di in dict)
            {
                form.AddField(di.Key, di.Value);
            }
            return form;
        }

        private Dictionary<string, string> CreateCustomForm(string fieldName, string key, string value)
        {
            var dict = new Dictionary<string, string> { { "type", fieldName }, { key, value } };
            dict = _defaultDictionary.Union(dict).ToDictionary(k => k.Key, v => v.Value);
            return dict;
        }
    }
    #endregion

    public class NetworkRestController : Singleton<NetworkRestController>
    {
        [SerializeField]
        private string _url = "http://localhost";

        [SerializeField]
        private int _port = 80;
        //just buffers it to speed things up
        private DataPacket _packet;

        private IEnumerator _playerSender;

        #region MonoBehaviours
        void Awake()
        {
            _packet = new DataPacket(ExperimentInfo.Instance.Participant.Id, "Test");
        }
        #endregion

        public void SetEndpoint(string url, int port)
        {
            //VALIDATION
            _url = url;
            _port = port;
        }

        public void SendEvent(string eventName)
        {
            var form = _packet.EventForm(eventName);
            StartCoroutine(SendPost(form));
        }

        public void StartSendingPlayerInformation()
        {
            if (_playerSender != null) StopSendingPlayerInformation();
            _playerSender = ContinuousPlayerPost(0.25f);
            StartCoroutine(_playerSender);
        }

        public void SendCustomInfo(string type, string key, string info)
        {
            var form = _packet.CustomForm(type, key, info);
            StartCoroutine(SendPost(form));
        }

        public void StopSendingPlayerInformation()
        {
            if(_playerSender != null) StopCoroutine(_playerSender);
        }

        public IEnumerator SendPost(WWWForm form)
        {
            var address = _url + ":" + _port + "/";
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
                var playerForm = _packet.PlayerForm(PlayerController.Instance.PlayerInformationDictionary());
                StartCoroutine(SendPost(playerForm));
                yield return new WaitForSeconds(time);
            }
        }
    }
}
