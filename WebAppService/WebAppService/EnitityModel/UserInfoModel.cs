using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebAppService.Entities;

namespace WebAppService.EntityModel
{
    //for data retirieve Base operations
    [DataContract]
    public class UserInfoModel : BaseEntityModel
    {
        [DataMember]
        public List<UserInfo> dataList { get; set; }

    }
}