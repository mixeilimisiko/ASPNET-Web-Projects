
namespace ToDo.App.Exceptions
{
    public class InvalidPasswordError : Exception
    {
        //public string Code = "blablablabla";

        public InvalidPasswordError(string message) : base(message) { }

    }
}
