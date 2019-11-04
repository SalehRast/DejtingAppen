using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dejting.Models;

namespace Dejting.Repositories
{
    public class FriendRepository : Repository<Friends, int>
    {
        public FriendRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}