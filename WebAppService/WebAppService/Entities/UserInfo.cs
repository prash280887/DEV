using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAppService.Entities
{
    
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int USERID { get; set; }
        [DataMember]
        public string EMAILID { get; set; }
        [DataMember]
        public string FName { get; set; }
        [DataMember]
        public string MName { get; set; }
        [DataMember]
        public string LName { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public string UPDATED_BY { get; set; }
        [DataMember]
        public DateTime? UPDATED_DATE { get; set; }
       
    }
}