using System;

namespace BrainVR.UnityLogger
{
    public class MasterLog : Singleton<MasterLog>
    {
        public bool ShouldLog = true;

        private ExperimentInfoLog _experimentInfoLog;
        private PlayerLog _playerLog;

        public string CreationTimestamp { get; private set; }

        //should work as a master log script
        //should be only one running at all times
        public void Instantiate(string participantId = null)
        {
            if (participantId == null) participantId = ExperimentInfo.Instance.Participant.Id;
            InstantiateLoggers(participantId);
        }
        //RETURNS if logs already exist, otherwise reinstantiates them
        void InstantiateLoggers(string participantId)
        {
            if (_playerLog && _experimentInfoLog) return;
            //to get one timestapm in order to synchronize loading of log files
            CreationTimestamp = DateTime.Now.ToString("HH-mm-ss-dd-MM-yyy");
            
            //instantiates the log
            _playerLog = gameObject.AddComponent<PlayerLog>();
            _experimentInfoLog = gameObject.AddComponent<ExperimentInfoLog>();
            //var experimentInfo = SettingsHolder.Instance.ExperimentInfo;
            if (participantId != null)    //if we have an ParticipantId
            {
                _playerLog.Instantiate(CreationTimestamp, participantId);
                _experimentInfoLog.Instantiate(CreationTimestamp, participantId);
            }
            else //the defaults
            {
                _playerLog.Instantiate(CreationTimestamp);
                _experimentInfoLog.Instantiate(CreationTimestamp);
            }
        }
        public void StartLogging()
        {
            if(_playerLog) _playerLog.StartLogging();
        }
        public void StopLogging()
        {
            if (_playerLog) _playerLog.StopLogging();
        }
        public void CloseLogs()
        {
            StopLogging();
            if (_experimentInfoLog)
            {
                _experimentInfoLog.Close();
                
                Destroy(_experimentInfoLog);
                _experimentInfoLog = null; //because of timing issues when closing and opening in one method
            }
            if (_playerLog)
            {
                _playerLog.Close();
                Destroy(_playerLog);
                _playerLog = null;
            }
        }
    }
}