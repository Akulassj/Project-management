using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class ProjectDeleteViewModel
    {
        public Project Project { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public string CreatorName { get; set; }
       
    }
}
