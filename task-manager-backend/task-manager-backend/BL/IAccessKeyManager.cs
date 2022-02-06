namespace TaskManagerBackend.BL
{
    public interface IAccessKeyManager
    {
        string CreateAccessKey(int userID);
        string? GetAccessKey(int userID);
        void CheckExpiry(Object source, System.Timers.ElapsedEventArgs e);
    }
}
