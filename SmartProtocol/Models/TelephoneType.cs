using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class TelephoneType
    {
        public TelephoneType()
        {
            Telephone = new HashSet<Telephone>();
        }

        public int TelephoneTypeId { get; set; }
        public string TelephoneTypeName { get; set; }
        public string TelephoneTypeDescription { get; set; }

        public virtual ICollection<Telephone> Telephone { get; set; }
    }
}
