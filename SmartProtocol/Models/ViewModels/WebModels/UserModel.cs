using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartProtocol.Models.ViewModels.WebModels
{
    public class BaseModel
    {
        public bool _isEditMode { get; set; }
        public long _userId { get; set; }
        public string _createdDate { get; set; }
        public string _lastLoginOn { get; set; }
    }

    public class User: BaseModel
    {
        public long id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public List<long> roles { get; set; }
        public string pic { get; set; }
        public string fullname { get; set; }
        public string occupation { get; set; }
	    public string companyName { get; set; }
	    public string phone { get; set; }
        public Address address { get; set; }
        public SocialNetworks socialNetworks { get; set; }
    }

    public class Address
    {
        public string addressLine { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
    }

    public class SocialNetworks
    {
        public string linkedIn { get; set; }
	    public string facebook { get; set; }
	    public string twitter { get; set; }
        public string instagram { get; set; }
    }


}
