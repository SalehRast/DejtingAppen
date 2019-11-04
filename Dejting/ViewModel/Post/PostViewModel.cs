using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dejting.ViewModel.Post
{
    public class PostViewModel
    {
        public string From{ get; set; }
        public string To { get; set;  }

        [StringLength(600), Required]
        public string Text { get; set; }
    }
}