using ToDo.Domain.Enums;

namespace ToDo.Domain
{
    public class Subtask : BaseEntity
    {

        public string Title { get; set; }
        public int ToDoId { get; set; }

        //Navigation Property

        public Todo Todo { get; set; }
    }
}
