namespace TaskManagerBackend.BL
{
    public interface IAccessKeyValidator
    {
        string CreateAccessKey(int userID);
        bool CheckAccessKey(int userID, string accessKey);
        string? GetAccessKey(int userID);
        void CheckExpiry(Object source, System.Timers.ElapsedEventArgs e);
    }
}
