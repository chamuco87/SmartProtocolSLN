using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class StepType
    {
        public StepType()
        {
            Step = new HashSet<Step>();
        }

        public int StepTypeId { get; set; }
        public string StepTypeName { get; set; }
        public string StepTypeDescription { get; set; }

        public virtual ICollection<Step> Step { get; set; }
    }
}
