using MVCAJDemo.WebAppService;
using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCAJDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUserDetails(UserInfo uiRequest)
        {
            //call service
            IWebAppService svc = Proxy.WebAppServiceClient();
            string usrId = null;// Convert.ToString(uiRequest.USERID);
            UserInfoModel ui = svc.GetUserDetails(usrId);
            return Json(ui.dataList, JsonRequestBehavior.AllowGet);   
                 
        }

        public class Users
        {
           public  string name { get; set; }
           public int age { get; set; }
          public  string location { get; set; }
        }
        public JsonResult GetUserList(UserInfoModel pager)
        {
            //call service
            List<Users> users = new List<Users>();
            users.Add(new Users() { name = "Madhav Sai", age = 10, location = "Nagpur" });
            users.Add(new Users() { name = "Suresh Dasari", age = 30, location = "Chennai" });
            users.Add(new Users() { name = "Rohini Alavala", age = 29, location = "Chennai" });
            users.Add(new Users() { name = "Praveen Kumar", age = 25, location = "Bangalore" });
            users.Add(new Users() { name = "Sateesh Chandra", age = 27, location = "Vizag" });
            users.Add(new Users() { name = "Siva Prasad", age = 38, location = "Nagpur" });
            users.Add(new Users() { name = "Sudheer Rayana", age = 25, location = "Kakinada" });
            users.Add(new Users() { name = "Honey Yemineni", age = 7, location = "Nagpur" });
            users.Add(new Users() { name = "Mahendra Dasari", age = 22, location = "Vijayawada" });
            users.Add(new Users() { name = "Mahesh Dasari", age = 23, location = "California" });
            users.Add(new Users() { name = "Nagaraju Dasari", age = 34, location = "Atlanta" });
            users.Add(new Users() { name = "Gopi Krishna", age = 29, location = "Repalle" });
            users.Add(new Users() { name = "Sudheer Uppala", age = 19, location = "Guntur" });
            users.Add(new Users() { name = "Sushmita", age = 27, location = "Vizag" });
        
            var lst = users;
            string dir = pager.sortDir == null ?"" : Convert.ToString(pager.sortDir).ToUpper();
            string col = pager.sortCol == null ? "" : Convert.ToString(pager.sortCol).ToUpper();
            switch (dir)
            {
                case "ASC":
                    SortList<Users>(lst, pager.sortCol, SortDirection.Ascending);
                    break;
                case "DESC":
                    SortList<Users>(lst, pager.sortCol, SortDirection.Descending);
                    break;
                default:
                    lst = users;
                    break;
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public void SortList<T>(List<T> list, string columnName, SortDirection direction)
        {
            var property = typeof(T).GetProperty(columnName);
            var multiplier = direction == SortDirection.Descending ? -1 : 1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}