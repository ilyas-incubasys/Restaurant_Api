using RestuarantApp_API.Context;
using RestuarantApp_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RestuarantApp_API.AdminController
{
    public class LoginController : Controller
    {
        private RestaurantContext db = new RestaurantContext();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            CreateMenu("khan", "123456");
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateMenu(string userName, string password)
        {
            string respponse = string.Empty;
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            var hashedPassword = md5.ComputeHash(data);
            if (ModelState.IsValid)
            {
                var user =  from u in db.Users
                                   where u.UserName == userName 
                                   //u.PasswordHash == hashedPassword.ToString()
                                   select u;
                if (user != null)
                {
                    return Json(true);
                }
                else
                    return Json(false);
            }
            else
            {
                return Json(false);
            }
        }
	}
}