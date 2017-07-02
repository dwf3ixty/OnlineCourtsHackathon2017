using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCourt2017.Data.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}