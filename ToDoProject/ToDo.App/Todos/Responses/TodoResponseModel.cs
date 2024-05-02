using ToDo.App.Subtasks.Responses;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.App.Todos.Responses
{
    public class TodoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TargetDate { get; set; }
        public int UserId { get; set; }
        public List<SubtaskResponseModel> Subtasks { get; set; }

        public Statuses Status { get; set; }

    }
}
