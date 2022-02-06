using TaskManagerBackend.Models;
using System.Timers;

namespace TaskManagerBackend.BL
{
    public class AccessKeyValidator : IAccessKeyValidator
    {
        private Dictionary<int, AccessKeyInfo> _accessKeyDict;
        private Queue<AccessKeyInfo> _accessKeyQueue;
        private System.Timers.Timer _timer;
        private int _accessKeyAge;
        public AccessKeyValidator(int accessKeyAge,int validatorInterval)
        {
            _timer = new System.Timers.Timer();
            _accessKeyDict = new Dictionary<int, AccessKeyInfo>();
            _accessKeyQueue = new Queue<AccessKeyInfo>();

            _timer.Interval = validatorInterval;
            _timer.Elapsed += CheckExpiry;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _accessKeyAge = accessKeyAge;
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

        public bool CheckAccessKey(int userID, string accessKey)
        {
            return _accessKeyDict[userID].AccessKey == accessKey;
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
