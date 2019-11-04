using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dejting.Models;

namespace Dejting.Repositories
{
    public class PostRepository: Repository<Post, int>
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }
   
    }
}