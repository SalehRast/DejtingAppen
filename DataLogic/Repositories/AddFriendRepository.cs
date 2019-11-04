using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dejting.Models;

namespace Dejting.Repositories
{
   
        public class AddFriendRepository : Repository<FriendRequest, int>
        {
            public AddFriendRepository(ApplicationDbContext context) : base(context)
            {

            }
        
    }
}