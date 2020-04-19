using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressTypeDescription { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
