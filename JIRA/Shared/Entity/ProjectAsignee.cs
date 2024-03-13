using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared.Entity
{
    public class ProjectAsignee
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid UserId { get; set; }

        public bool IsCreator { get; set; }

        public ProjectAsignee()
        {
            Id = Guid.NewGuid();
        }
    }
}
