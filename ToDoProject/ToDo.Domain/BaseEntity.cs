
using ToDo.Domain.Enums;

namespace ToDo.Domain
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Statuses Status { get; set; }
    }
}
