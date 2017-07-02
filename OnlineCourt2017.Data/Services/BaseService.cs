using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Services
{
    public abstract class BaseService
    {
        internal AppDbContext dbContext;
        public BaseService(AppDbContext context)
        {
            this.dbContext = context;
        }
    }
}
