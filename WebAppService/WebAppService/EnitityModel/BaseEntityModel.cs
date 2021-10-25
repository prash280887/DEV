using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAppService.EntityModel
{
    //for data retirieve Base operations
    [DataContract]
    public class BaseEntityModel
    {
        [DataMember]
        public string sortDir { get; set; }
        [DataMember]
        public string sortCol { get; set; }
        [DataMember]
        public int pageNumber { get; set; }
        [DataMember]
        public int pageSize { get; set; }

        [DataMember]
        public byte[] byteData { get; set; }

        [DataMember]
        public int returnCode { get; set; }

        [DataMember]
        public string returnMsg { get; set; }

        [DataMember]
        public Exception exception { get; set; }

         [DataMember] //optional
         public object returnData { get; set; }

    }
}