using OnlineCourt2017.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Services
{
    public class SecurityService : BaseService
    {
        private AppDbContext context;
        public SecurityService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public bool ValidateUser(string userName, string password)
        {
            var dbUserName = this.context.Users.Where(w => w.UserName == userName && w.Password == password).Select(s => s.UserName).FirstOrDefault();
            if (!string.IsNullOrEmpty(dbUserName))
            {
                return true;
            }

            return false;
        }

        public User GetUser(string userName)
        {
            return this.context.Users.Where(w => w.UserName == userName).FirstOrDefault();
        }
    }
}
