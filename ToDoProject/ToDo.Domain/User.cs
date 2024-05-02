using ToDo.Domain.Enums;

namespace ToDo.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        //Navigation Property
        public ICollection<Todo> ToDos { get; set; }
    }
}
