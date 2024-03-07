using JIRA.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA.Shared
{
    public class JobComments
    {
        public Job Job { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
