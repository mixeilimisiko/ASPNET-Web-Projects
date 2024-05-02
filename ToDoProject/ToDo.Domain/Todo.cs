
using ToDo.Domain.Enums;

namespace ToDo.Domain
{
    public class Todo : BaseEntity
    {
        public string Title { get; set; }
        public DateTime? TargetDate { get; set; }
        public int UserId { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public IEnumerable<Subtask> Subtasks { get; set; }


    }
}
