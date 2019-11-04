using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dejting.Models;
using Dejting.Repositories;
using Dejting.ViewModel.Friend;
using Dejting.ViewModel.Post;

namespace Dejting.Controllers
{
    //controller som ska bearbeta andra användare samt lägga till denne som vän 
    public class FriendController : Controller
    {
        // GET: Friend


        private UserRepository userRepository;
        private AddFriendRepository addFriendRepository;
        private FriendRepository friendRepository;
        private PostRepository postRepository;
        public FriendController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            addFriendRepository = new AddFriendRepository(context);
            friendRepository = new FriendRepository(context);
            postRepository = new PostRepository(context);
        }

 
        //details för Andra användare sida 
        public ActionResult FriendDetails(string id)
        {

            var user = userRepository.Get(id);
            if(user == null)
            return RedirectToAction("index");

            var postsForUser = postRepository.GetAll().Where(post => post.To.Id == id);

            var posts = postsForUser.Select(post => new PostListViewModel()
            {
                Text = post.Text,
                Date = post.DateTime,
                FromName = post.From.Email,
                ToName = post.To.Email
            });

            var model = new FriendViewModel
            {
                Id = user.Id,
                Bild = user.Bild,
                Email = user.Email,
                Stad = user.Stad,
                Alder = user.Alder,
                Kön = user.Kön,
                Posts = posts.ToList()
            };

            return View(model);

        }

        public ActionResult AcceptFriend(int id)
        {
            var friend = addFriendRepository.Get(id);   
            Friends friends = new Friends();
            {
                friends.Friend1 = friend.To;
                friends.Friend2 = friend.From;
            };

            friendRepository.Add(friends);
            friendRepository.Save();
            addFriendRepository.Remove(friend.Id);
            addFriendRepository.Save();

            return View("Accept");
        }

        public ActionResult DeclineFriend(int id)
        {
            var friendRequest = addFriendRepository.Get(Convert.ToInt32(id));
            var friendAccept = userRepository.Get(User.Identity.GetUserId());

            var Friend = addFriendRepository.GetAll().Single(x => x.From.Id.Equals(friendRequest.From.Id) && x.To.Id.Equals(friendAccept.Id));

            addFriendRepository.Remove(Friend.Id);
            addFriendRepository.Save();

            return View("Decline");
        }


        public PartialViewResult GetPosts(string id)
        {
            var postsForUser = postRepository.GetAll().Where(post => post.To.Id == id).OrderByDescending(post => post.DateTime);

            var posts = postsForUser.Select(post => new PostListViewModel()
            {
                Text = post.Text,
                Date = post.DateTime,
                FromName = post.From.Email,
                ToName = post.To.Email
            });

            return PartialView("_PostTable", posts);
        }

    }
}