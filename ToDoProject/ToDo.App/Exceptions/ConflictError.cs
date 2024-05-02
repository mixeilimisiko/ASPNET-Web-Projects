
namespace ToDo.App.Exceptions
{
    public class ConflictError : Exception
    {
        public ConflictError(string message) : base(message) { }
    }
}
