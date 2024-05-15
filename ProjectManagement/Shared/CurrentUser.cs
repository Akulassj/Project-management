using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared
{
    public class CurrentUser
    {
        public Guid Id { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
