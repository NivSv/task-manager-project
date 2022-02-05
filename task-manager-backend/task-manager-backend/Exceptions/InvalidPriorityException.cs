namespace TaskManagerBackend.Exceptions
{
    public class InvalidPriorityException : Exception
    {
        public InvalidPriorityException(string message)
        : base(message)
        {
        }
    }
}