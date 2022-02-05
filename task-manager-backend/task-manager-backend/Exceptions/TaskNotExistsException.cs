namespace TaskManagerBackend.Exceptions
{
    public class TaskNotExistsException : Exception
    {
        public TaskNotExistsException(string message)
        : base(message)
        {
        }
    }
}