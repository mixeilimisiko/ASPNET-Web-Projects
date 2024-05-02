namespace ToDo.App.Exceptions
{
    public class AlreadyExistsError : Exception
    {
        //public string Code = "blabla";

        public AlreadyExistsError(string message) : base(message)
        {

        }
    }
}
