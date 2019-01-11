using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testgrej.Models;

namespace Testgrej.Controllers
{
    public class FriendsController : Controller
    {
        // GET: Friends
        [Authorize]
        public ActionResult Index()
        {
            var loggedInUserId = User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id.Equals(loggedInUserId));
            return View(user);
        }
    }
}