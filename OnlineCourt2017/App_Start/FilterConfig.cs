using OnlineCourt2017.Models;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourt2017
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
