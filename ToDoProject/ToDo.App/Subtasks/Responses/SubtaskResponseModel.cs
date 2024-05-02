

using ToDo.Domain.Enums;

namespace ToDo.App.Subtasks.Responses
{
    public class SubtaskResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Statuses Status { get; set; }
    }
}
