using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class ProjectDeleteViewModel
    {
        public Project Project { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public string CreatorName { get; set; }
       
    }
}
