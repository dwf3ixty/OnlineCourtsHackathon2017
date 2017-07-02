using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Interfaces
{
    public interface IUserIdentity
    {
        string GetUserName { get;}
        string Name { get; }
    }
}
