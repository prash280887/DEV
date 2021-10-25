using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebAppService.EntityModel;

namespace WebAppService
{
    [ServiceContract]
    public interface IWebAppService
    {

        [OperationContract]
        void TestService_SPCall(string userId);

        [OperationContract]
        void TestService_QueryCall(string query);

        [OperationContract]
        UserInfoModel GetUserDetails(string userId);

    }

}
