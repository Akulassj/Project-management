using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class ProjectInfoViewModel
    {
        public Project Project {  get; set; }
        //public ProjectTask ProjectTask { get; set; }
        public List<User> AssignedUsers { get; set; }
        public List<ProjectTaskUsersModel> ProjectTaskUsers { get; set; }
        public string Creatorname {  get; set; }
    }
}
