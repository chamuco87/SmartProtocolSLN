using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class User
    {
        public User()
        {
            Address = new HashSet<Address>();
            Email = new HashSet<Email>();
            Flow = new HashSet<Flow>();
            Login = new HashSet<Login>();
            Step = new HashSet<Step>();
            Telephone = new HashSet<Telephone>();
        }

        public long UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Email> Email { get; set; }
        public virtual ICollection<Flow> Flow { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<Step> Step { get; set; }
        public virtual ICollection<Telephone> Telephone { get; set; }
    }
}
