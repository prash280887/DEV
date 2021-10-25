using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAPIDemo.Models
{
        public class UserDetail
        {
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "userid")]
            public string UserId { get; set; }
            
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

        }

    public class LoginUser
    {

        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

    }

}
