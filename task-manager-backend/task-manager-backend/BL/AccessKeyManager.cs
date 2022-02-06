using TaskManagerBackend.Models;
using System.Timers;
using TaskManagerBackend.Exceptions;

namespace TaskManagerBackend.BL
{
    public class AccessKeyManager : IAccessKeyManager
    {
        private Dictionary<int, AccessKeyInfo> _accessKeyDict;
        private Queue<AccessKeyInfo> _accessKeyQueue;
        private System.Timers.Timer _timer;
        private int _accessKeyAge;
        private int _validatorInterval;
        public AccessKeyManager()
        {
            Configure();
            _timer = new System.Timers.Timer();
            _accessKeyDict = new Dictionary<int, AccessKeyInfo>();
            _accessKeyQueue = new Queue<AccessKeyInfo>();

            _timer.Interval = _validatorInterval;
            _timer.Elapsed += CheckExpiry;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void Configure()
        {
            this._accessKeyAge = 86400000;
            this._validatorInterval = 5000;
        }

        public string CreateAccessKey(int userID)
        {
            string newAccessKey = Guid.NewGuid().ToString(); ;
            var keyinfo = new AccessKeyInfo
            {
                UserID = userID,
                AccessKey = newAccessKey,
                CreatedDate = DateTime.UtcNow,
            };
            _accessKeyDict.Add(userID, keyinfo);
            _accessKeyQueue.Enqueue(keyinfo);
            return newAccessKey;
        }

        public string? GetAccessKey(int userID)
        {
            if (!_accessKeyDict.ContainsKey(userID)) return null;
            return _accessKeyDict[userID].AccessKey;
        }
        public void CheckExpiry(Object source, System.Timers.ElapsedEventArgs e)
        {
            while(_accessKeyQueue.Count != 0  &&  _accessKeyQueue.Peek().CreatedDate.AddMilliseconds(_accessKeyAge) <= DateTime.UtcNow)
            {
                AccessKeyInfo accessKeyInfo = _accessKeyQueue.Dequeue();
                _accessKeyDict.Remove(accessKeyInfo.UserID);
            }
        }
    }
}
