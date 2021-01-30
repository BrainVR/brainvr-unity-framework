using ArduinoConnector;
using UnityEditor;
using UnityEngine;

namespace BrainVR.UnityFramework.Arduino
{
    public enum ArduinoState {Uninitalised, Ready, NotFound}

    public class ArduinoController : Singleton<ArduinoController>
    {
        public ArduinoType Type;
        public bool IsConnected => _state == ArduinoState.Ready;
        private ArduinoConnector.Arduino _arduino;
        private ArduinoState _state;
        // Use this for initialization

        public void Connect()
        {
            if (_state != ArduinoState.Uninitalised) Disconnect();
            _arduino = new ArduinoConnector.Arduino(Type);
            _state = _arduino.Connect() ? ArduinoState.Ready : ArduinoState.NotFound;
        }

        public void Disconnect()
        {
            if (_arduino == null) return;
            _arduino.Disconnect();
            _arduino = null;
            _state = ArduinoState.Uninitalised;
        }

        public void Blink()
        {
            if (IsReady()) _arduino.Blink();
        }

        public bool SendPulseUp()
        {
            if (!IsReady()) return false;
            _arduino.StartPulse();
            return true;
        }

        public bool SendPulseDown()
        {
            if (!IsReady()) return false;
            _arduino.StopPulse();
            return true;
        }

        private bool IsReady()
        {
            return _state == ArduinoState.Ready;
        }
    }

    [CustomEditor(typeof(ArduinoController))]
    public class ArduinoControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myScript = (ArduinoController)target;
            if (GUILayout.Button(!myScript.IsConnected ? "Connect" : "Disconnect"))
            {
                if (myScript.IsConnected)
                {
                    myScript.Disconnect();
                }
                else 
                {
                    myScript.Connect();
                }
            }
            GUILayout.Label(myScript.IsConnected ? "Arduino connected" : "Not connected");
            if(GUILayout.Button("Blink")) myScript.Blink();
        }
    }
}
