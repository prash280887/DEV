using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAPIDemo.Models;

namespace WebCoreAPIDemo.Utility
{
           public interface ICosmosDbService
        {
            Task<IEnumerable<UserDetail>> GetItemsAsync(string query);
            Task<UserDetail> GetItemAsync(string id);
            Task AddItemAsync(UserDetail item);
            Task UpdateItemAsync(string id, UserDetail item);
            Task DeleteItemAsync(string id);
        }
   
}
