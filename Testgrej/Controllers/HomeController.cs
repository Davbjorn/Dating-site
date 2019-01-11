using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testgrej.Models;

namespace Testgrej.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            List<ApplicationUser> users = store.Users.OrderByDescending(item => item.Id)
                                               .Take(3)  // Takes the first 3 items
                                               .ToList();
            List<ProfileViewModel> viewModels = new List<ProfileViewModel>();
            foreach (var user in users) {
                viewModels.Add(GetViewModel(user));
            }
            HomeViewModel model = new HomeViewModel {profiles = viewModels };
            return View(model);
        }

        public ProfileViewModel GetViewModel(ApplicationUser user)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - user.BirthDate.Year;
            // Go back to the year the person was born in case of a leap year
            if (user.BirthDate > today.AddYears(-age)) age--;
            return new ProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Age = age,
                Image = user.Image,
                AboutMe = user.AboutMe

            };
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
    }
}