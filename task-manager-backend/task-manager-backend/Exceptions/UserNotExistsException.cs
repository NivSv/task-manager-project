namespace TaskManagerBackend.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException(string message)
        : base(message)
        {
        }
    }
}