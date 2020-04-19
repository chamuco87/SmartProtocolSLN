using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            StepAlert = new HashSet<StepAlert>();
        }

        public int AlertTypeId { get; set; }
        public string AlertTypeName { get; set; }
        public string AlertTypeDescription { get; set; }

        public virtual ICollection<StepAlert> StepAlert { get; set; }
    }
}
