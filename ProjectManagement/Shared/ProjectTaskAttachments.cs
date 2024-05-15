using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class ProjectTaskAttachments
    {
        public ProjectTask ProjectTask { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
