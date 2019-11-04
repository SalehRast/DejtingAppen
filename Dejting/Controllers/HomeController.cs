using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dejting.Models;
using Dejting.Repositories;
using Dejting.ViewModel.Profilepage;

namespace Dejting.Controllers
{
    public class HomeController : Controller
    {
        private UserRepository userRepository;

        public HomeController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);

        }
        //startsidan
        public ActionResult Index()
        {
            var anvandare = userRepository.GetAll().Where(x => x.Synlighet == Synlighet.Synlig);
            anvandare.Take(2);
            return View(anvandare);
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