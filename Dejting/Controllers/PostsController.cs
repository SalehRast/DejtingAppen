using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dejting.Models;
using Dejting.Repositories;
using Dejting.ViewModel.Post;

namespace Dejting.Controllers
{
    public class PostsController : ApiController
    {
        private PostRepository PostRepository;
        private UserRepository UserRepository; 
        public PostsController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            PostRepository = new PostRepository(context);
            UserRepository = new UserRepository(context);
        }

        // skickar post
        [HttpPost]
        public void Post(PostViewModel model)
        {
            if(model.Text != "")
            {
                Post post = new Post();
                var anvandareTill = UserRepository.Get(model.To);
                post.To = anvandareTill;

                var anvandareFran = UserRepository.Get(model.From);
                post.From = anvandareFran;

                post.Text = model.Text;
                post.DateTime = DateTime.Now;
                PostRepository.Add(post);
                PostRepository.Save(); 
            }
        }
        [HttpGet]
        public List<Post> GetPost(string id)
        {
            var post = PostRepository.GetAll().Where(x => x.To.Id == id).ToList();
            return post;

        }

    }
}
