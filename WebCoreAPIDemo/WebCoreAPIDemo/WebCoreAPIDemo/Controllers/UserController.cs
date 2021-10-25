using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoreAPIDemo.Models;
using WebCoreAPIDemo.Utility;

namespace WebCoreAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        public UserController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        // GET api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" , "value3" };
        }


        [Route("Authenticate")]
        [HttpPost]
        public LoginResult Authenticate(LoginUser item)
        {
            var response = _cosmosDbService.GetItemsAsync("SELECT * FROM c where c.userId='" + item.UserName + "' and c.password='" + item.Password + "'");
            IEnumerable<UserDetail> userResult = response.Result;
            UserDetail userDetail = null;

            if (userResult.Any())
            {
                userDetail = userResult.ToList()[0];
                item.Status = "Success";
            }
            else
            {
                item.Status = "Invalid Login";
            }

            return new LoginResult() { userdetail = userDetail, loginuser = item };
        }


        [HttpGet]
        [Route("getuser/{id}")]
        public UserDetail GetUser(string id)
        {

            var response = _cosmosDbService.GetItemAsync(id);
            return response.Result;
        }


        [HttpGet]
        [Route("getallusers")]
        public IEnumerable<UserDetail> GetAllUsers()
        {         
            
           var response = _cosmosDbService.GetItemsAsync("SELECT * FROM c");
           return response.Result;
        }

        [HttpPost]

        public ActionResult<UserDetail> CreateAsync(UserDetail item)
        {
            item.Id =  Guid.NewGuid().ToString();
            _cosmosDbService.AddItemAsync(item);
            return item ;
        }

        [HttpPost]

        public ActionResult<UserDetail> EditAsync(UserDetail item)
        {
            _cosmosDbService.UpdateItemAsync(item.Id, item);
            return item;
        }


        [HttpPost]
        [ActionName("Delete")]

        public async Task DeleteAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            
        }


    }

    public class LoginResult
    {
        public LoginUser loginuser { get; set; }
        public UserDetail userdetail { get; set; }
    }
}
