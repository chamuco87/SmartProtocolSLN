using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Flow
    {
        public Flow()
        {
            Step = new HashSet<Step>();
        }

        public long FlowId { get; set; }
        public long UserId { get; set; }
        public string FlowName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Step> Step { get; set; }
    }
}
