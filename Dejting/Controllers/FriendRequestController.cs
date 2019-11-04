using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dejting.Models;
using Dejting.Repositories;


namespace Dejting.Controllers
{
    public class FriendRequestController : Controller
    {

        private AddFriendRepository AddFriendRepository;
        private UserRepository UserRepository;
        private FriendRepository FriendRepository; 

        public FriendRequestController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AddFriendRepository = new AddFriendRepository(context);
            UserRepository = new UserRepository(context);
            FriendRepository = new FriendRepository(context);

        }
        // GET: FriendRequest
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendRequest(string id)
        {
            FriendRequest FriendRequest = new FriendRequest();

            var userNameId = User.Identity.GetUserId();

            var fromUser = UserRepository.Get(userNameId);

            FriendRequest.From = fromUser;

            var toUser = UserRepository.Get(id.ToString());

            FriendRequest.To = toUser;
            
            var checklist = AddFriendRepository.GetAll();

            bool FriendRequestExist = false;


            foreach (var item in checklist)
            {

                if (fromUser.Id.Equals(item.To.Id) && toUser.Id.Equals(item.From.Id) || fromUser.Id.Equals(item.From.Id) && toUser.Id.Equals(item.To.Id))
                {
                    FriendRequestExist = true;
                }
            }

            if (FriendRequestExist == false)
            {
                AddFriendRepository.Add(FriendRequest);

                AddFriendRepository.Save();

                return View("Error");
            }
           
            else
            {
                return View("AlreadyFriends");
            }
        }

        //visar lista på vänförfrågningar 
        [HttpGet]
        public ActionResult RequestList()
        {   
            var anvandare = User.Identity.GetUserId();

            var requests = AddFriendRepository.GetAll().Where(request => request.To.Id == anvandare).ToList();

            

            //ska hämta till vilken dvs inloggade användaren 
            var anvandaretill = AddFriendRepository.GetAll().Where(x => x.To.Id == (anvandare)).Select(u=> u.From);

            var user = from a in UserRepository.GetAll().Where(x=>x.Id.Equals(anvandaretill)) select a;

            return View(requests);
        }



        //visar lista på mina vänner
        [HttpGet]
        public ActionResult FriendList()
        {
            var anvandare = User.Identity.GetUserId();

            //ska hämta till vilken dvs inloggade användaren 
            var anvandaretill = FriendRepository.GetAll().Where(x => x.Friend1.Id ==(anvandare)).Select(u => u.Friend2);

            var user = from a in UserRepository.GetAll().Where(x => x.Id.Equals(anvandaretill)) select a;





            var anvandaretill2 = FriendRepository.GetAll().Where(x => x.Friend2.Id == (anvandare)).Select(u => u.Friend1);

            var user2 = from a in UserRepository.GetAll().Where(x => x.Id.Equals(anvandaretill2)) select a;

            var total = anvandaretill.Concat(anvandaretill2);

            return View(total);
        }
        
        [HttpGet]
        public PartialViewResult Notification()
        {
 
            var requests = AddFriendRepository.GetAll().Where(request => request.To.Id == User.Identity.GetUserId()).ToList();

            var model = new BoolHolder();
            model.UserGotRequest = requests.Any();

            return PartialView("_Notification", model);
        }
    }
}