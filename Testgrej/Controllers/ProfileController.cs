using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testgrej.Models;

namespace Testgrej.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        public ActionResult Index(string id)
        {
            var userId = id;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(userId);
            if (currentUser == null)
            {
                return View("NotFound");
            }
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - currentUser.BirthDate.Year;
            // Go back to the year the person was born in case of a leap year
            if (currentUser.BirthDate > today.AddYears(-age)) age--;
            var model = new ProfileViewModel
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Age = age,
                Image = currentUser.Image,
                AboutMe = currentUser.AboutMe

            };
            return View(model);
        }
        [Authorize]
        public ActionResult AddFriend(string id)
        {
            var userId = id;
            var loggedInUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(userId);
            if (currentUser == null)
            {
                return View("NotFound");
            }
            var db = new ApplicationDbContext();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id.Equals(loggedInUserId));
            ApplicationUser otherUser = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            otherUser.FriendRequests.Add(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }
        public ActionResult DeclineFriend(string id)
        {
            var userId = id;
            var loggedInUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(userId);
            if (currentUser == null)
            {
                return View("NotFound");
            }
            var db = new ApplicationDbContext();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id.Equals(loggedInUserId));
            ApplicationUser otherUser = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            otherUser.FriendRequests.Remove(user);
            db.Entry(otherUser).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Friends");
        }

    }
}