using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class ProjectsAsignees
    {
        public Project Project { get; set; }
        public List<ProjectAsignee> ProjectAsignees { get; set; }
        public string UserName { get; set; }
    }
}
