using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dejting.Models;

namespace Dejting.Repositories
{
    public class UserRepository : Repository <ApplicationUser, string>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }  
}