namespace TaskManagerBackend.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string message)
        : base(message)
        {
        }
    }
}