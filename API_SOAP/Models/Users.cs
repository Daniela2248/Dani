using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace API_SOAP.Models
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
