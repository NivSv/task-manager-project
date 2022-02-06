using TaskManagerBackend.Exceptions;

namespace TaskManagerBackend.BL
{
    public class UserAuthorizer : IUserAuthorizer
    {
        private readonly IUserBL _userBL;
        private readonly IAccessKeyManager _accessKeyManager;

        public UserAuthorizer(IUserBL userBL, IAccessKeyManager _accessKeyManager)
        {
            this._userBL = userBL;
            this._accessKeyManager = _accessKeyManager;
        }

        public bool isAuthorized(string username, string accessKey)
        {
            int userID;
            try
            {
                userID = _userBL.GetByUsername(username).UserId;
            }
            catch (UserNotExistsException e)
            {
                return false;
            }
            if (_accessKeyManager.GetAccessKey(userID) == null) return false;
            return _accessKeyManager.GetAccessKey(userID) == accessKey;
        }
    }
}
