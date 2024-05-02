namespace ToDo.App.Exceptions
{
    public class NotFoundError : Exception
    {
        //public string Code = "blablabla";

        public NotFoundError(string message) : base(message) { }

    }
}
