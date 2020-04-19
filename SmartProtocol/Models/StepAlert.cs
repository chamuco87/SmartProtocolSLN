using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class StepAlert
    {
        public int StepAlertId { get; set; }
        public long StepId { get; set; }
        public int AlertTypeId { get; set; }
        public string StepAlertStatus { get; set; }
        public DateTime? StepStartedOn { get; set; }
        public DateTime? StepCompletedOn { get; set; }
        public int RetryCount { get; set; }

        public virtual AlertType AlertType { get; set; }
        public virtual Step Step { get; set; }
    }
}
