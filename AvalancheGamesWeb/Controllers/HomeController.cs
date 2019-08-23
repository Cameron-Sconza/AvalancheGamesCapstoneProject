using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;


namespace AvalancheGamesWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Roles()
        {
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                List<BusinessLogicLayer.RoleBLL> model = ctx.GetRoles(0, 100);
                return View(model);
            }
        }
        [HttpGet]
      public ActionResult Login()
        {
           // displays empty login screen with predefined returnURL
            Models.LoginModel mapper = new Models.LoginModel(); 
            mapper.Message   = TempData["Message"]?.ToString()??"";
            mapper.ReturnURL = TempData["ReturnURL"]?.ToString()??@"~/Home";
            mapper.UserEmail  = "genericuser";
            mapper.Password  = "genericpassword";
 
            return View(mapper);
        }
   [HttpPost]
        public ActionResult Login(Models.LoginModel info)
        {
          using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
               BusinessLogicLayer.UserBLL user = ctx.FindUserByUserEmail(info.UserEmail);
               if (user == null) 
                            { 
                               info.Message = $"The Email '{info.UserEmail}' does not exist in the database";
                               return View(info);
                            }
             string actual = user.HASH;
               //string potential = user.Salt + info.Password;
                    string potential = info.Password;
                //bool validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual,potential);
                    bool validateduser = potential == actual;
                if (validateduser)
                {
                   Session["AUTHUsername"] = user.UserName;
                  Session["AUTHRoles"] = user.RoleName;
                    return Redirect(info.ReturnURL);
                }
                  info.Message = "The password was incorrect";  
                return View(info);           
           }
        }
    }
}