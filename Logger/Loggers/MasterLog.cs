using System;

namespace BrainVR.UnityFramework.Logger
{
    public class MasterLog : Singleton<MasterLog>
    {
        public bool ShouldLog = true;
        private ExperimentInfoLog _experimentInfoLog;
        private PlayerLog _playerLog;

        public string CreationTimestamp { get; private set; }

        //should work as a master log script
        //should be only one running at all times
        #region public API
        public void Instantiate(string participantId = null)
        {
            if (participantId == null) participantId = ExperimentInfo.Instance.Participant.Id;
            InstantiateLoggers(participantId);
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
            if (!_playerLog) return;
            _playerLog.Close();
            Destroy(_playerLog);
            _playerLog = null;
        }
        #endregion
        #region private helpers
        //RETURNS if logs already exist, otherwise reinstantiates them
        private void InstantiateLoggers(string participantId)
        {
            if (_playerLog && _experimentInfoLog) return;
            //to get one timestapm in order to synchronize loading of log files
            CreationTimestamp = DateTime.Now.ToString("HH-mm-ss-dd-MM-yyy");
            _playerLog = gameObject.AddComponent<PlayerLog>();
            _experimentInfoLog = gameObject.AddComponent<ExperimentInfoLog>();
            if (participantId != null) participantId = "NEO";
            _playerLog.Setup(CreationTimestamp, participantId);
            _experimentInfoLog.Setup(CreationTimestamp, participantId);
        }
        #endregion
    }
}