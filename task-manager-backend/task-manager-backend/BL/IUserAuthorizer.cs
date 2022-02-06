namespace TaskManagerBackend.BL
{
    public interface IUserAuthorizer
    {
        bool isAuthorized(string username, string accessKey);
    }
}
