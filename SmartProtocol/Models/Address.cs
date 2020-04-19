using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Address
    {
        public long AddressId { get; set; }
        public long UserId { get; set; }
        public int AddressTypeId { get; set; }
        public string Address1 { get; set; }
        public string Address11 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual User User { get; set; }
    }
}
