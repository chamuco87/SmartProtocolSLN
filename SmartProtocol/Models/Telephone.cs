using System;
using System.Collections.Generic;

namespace SmartProtocol.Models
{
    public partial class Telephone
    {
        public long TelephoneId { get; set; }
        public long UserId { get; set; }
        public int TelephoneTypeId { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual TelephoneType TelephoneType { get; set; }
        public virtual User User { get; set; }
    }
}
