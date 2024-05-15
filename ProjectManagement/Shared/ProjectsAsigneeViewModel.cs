using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class ProjectsAsigneeViewModel
    {
        public Project Project { get; set; }
        public List<ProjectAsignee> ProjectAsignees { get; set; }
        public string UserName { get; set; }
    }
}
