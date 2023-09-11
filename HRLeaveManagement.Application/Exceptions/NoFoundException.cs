namespace HRLeaveManagement.Application.Exceptions
{
    public class NoFoundException : Exception
    {
        public NoFoundException(string name, object key) : base($"{name} ({key})  was not found")
        {

        }
    }
}
