using ArduinoConnector;

namespace BrainVR.UnityFramework.Arduino
{
    public enum ArduinoState {Uninitalised, Ready, Paused, Silent, NotFound}
    public class ArduinoController : Singleton<ArduinoController>
    {
        private ArduinoConnector.Arduino _arduino;
        private ArduinoState _state;
        // Use this for initialization

        public void Connect()
        {
            if (_state == ArduinoState.Uninitalised)
            {
                _arduino = new ArduinoConnector.Arduino(ArduinoType.Leonardo);
                _state = _arduino.Connect() ? ArduinoState.Ready : ArduinoState.NotFound;
            }
        }
        public void Blink()
        {
            if (IsReady()) _arduino.Blink();
        }
        public bool SendPulsUp()
        {
            if (!IsReady()) return false;
            _arduino.StartPulse();
            return true;
        }
        public bool SendPulsDown()
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
}
