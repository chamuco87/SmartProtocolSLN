﻿using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Email
    {
        public Email()
        {
            Login = new HashSet<Login>();
        }

        public long EmailId { get; set; }
        public long UserId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsVerified { get; set; }
        public string ActivationToken { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Login> Login { get; set; }
    }
}
