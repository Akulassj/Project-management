using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class ProjectTaskAttachments
    {
        public ProjectTask ProjectTask { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
