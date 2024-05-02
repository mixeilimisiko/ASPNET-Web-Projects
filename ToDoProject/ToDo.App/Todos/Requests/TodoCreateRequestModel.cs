using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.App.Todos.Requests
{
    public class TodoCreateRequestModel
    {
        public string Title { get; set; }
        public DateTime TargetDate { get; set; } = DateTime.Now.AddDays(1);
    }
}
