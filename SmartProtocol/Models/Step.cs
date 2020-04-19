using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Step
    {
        public Step()
        {
            StepAlert = new HashSet<StepAlert>();
        }

        public long StepId { get; set; }
        public long FlowId { get; set; }
        public long UserId { get; set; }
        public int StepTypeId { get; set; }
        public string StepObject { get; set; }
        public string StepName { get; set; }
        public string StepStatus { get; set; }
        public DateTime? StepStartedOn { get; set; }
        public DateTime? StepCompletedOn { get; set; }
        public DateTime? ClaimedBy { get; set; }
        public DateTime TriggerDateTime { get; set; }
        public int RetryCount { get; set; }
        public bool IsActive { get; set; }

        public virtual Flow Flow { get; set; }
        public virtual StepType StepType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StepAlert> StepAlert { get; set; }
    }
}
