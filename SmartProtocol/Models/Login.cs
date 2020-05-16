using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Login
    {
        public long LoginId { get; set; }
        public long UserId { get; set; }
        public long EmailId { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public string ResetToken { get; set; }
        public DateTime? TokenCreatedOn { get; set; }

        public virtual Email Email { get; set; }
        public virtual User User { get; set; }
    }
}
