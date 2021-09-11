using Events.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            ApplicationUser userProfile = null;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isUser())
                {
                    ViewBag.displayMenu = "Yes";
                    if (!string.IsNullOrEmpty(username))
                    {
                        var user = context.Users.SingleOrDefault(u => u.UserName == username);
                        ViewBag.Education = user.Education;
                        ViewBag.FullName = user.FullName;
                        ViewBag.Id = user.Id;
                        userProfile = user;
                    }
                    return View(userProfile);
                }
            }

            return View();
        }
        public Boolean isUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext db = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var get = UserManager.GetRoles(user.GetUserId());
                if (get[0].ToString() == "Trainee")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}