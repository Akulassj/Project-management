using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Shared.Entity
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RecieverName { get; set; }
        public bool IsReaded { get; set; }
        public Notification()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}
