using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UserLogin(string userName, string password)
        {
            UserManager<Admin> _userManager;
            _userManager = new UserManager<Admin>(new UserStore<Admin>(db));
           
            Admin user = _userManager.Find(userName, password);
            if (user!=null)
            {
                Session.Add("UserName", user.UserName);
                Session.Add("UserId", user.Id);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
	}
}