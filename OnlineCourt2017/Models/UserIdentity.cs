using OnlineCourt2017.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace OnlineCourt2017.Models
{
    public class UserIdentity : IPrincipal, IUserIdentity
    {
        public IIdentity Identity { get; private set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string GetUserName
        {
            get
            {
                if (string.IsNullOrEmpty(this.UserName))
                {
                    return HttpContext.Current.User.Identity.Name;
                }

                return this.UserName;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public void SetIdentity(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
    }
}