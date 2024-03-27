using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class TasksAssigneeViewModel
    {
        public Job Job { get; set; }
        public List<TaskAssignee> TaskAssignees { get; set; }
    }
}
