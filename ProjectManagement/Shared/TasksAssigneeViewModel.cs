using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class TasksAssigneeViewModel
    {
        public ProjectTask ProjectTask { get; set; }
        public List<TaskAssignee> TaskAssignees { get; set; }
    }
}
