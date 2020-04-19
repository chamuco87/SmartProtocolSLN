using System;
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
        public string Email1 { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsVerified { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Login> Login { get; set; }
    }
}
