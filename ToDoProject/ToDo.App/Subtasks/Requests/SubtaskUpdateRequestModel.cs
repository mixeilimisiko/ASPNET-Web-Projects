using ToDo.Domain.Enums;

namespace ToDo.App.Subtasks.Requests
{
    public class SubtaskUpdateRequestModel
    {
        public string Title { get; set; }
        public Statuses Status { get; set; }
    }
}
