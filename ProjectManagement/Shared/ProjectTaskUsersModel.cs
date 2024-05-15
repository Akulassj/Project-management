using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class ProjectTaskUsersModel
    {
        public ProjectTask ProjectTask { get; set; }
        public User AssignedUser { get; set; }
        public bool InActive { get; set; }
        
    }
}
