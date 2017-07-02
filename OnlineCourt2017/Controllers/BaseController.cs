
using OnlineCourt2017.Data;
using OnlineCourt2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourt2017.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected virtual new UserIdentity User
        {
            get { return HttpContext.User as UserIdentity; }

        }
        public BaseController()
        {
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UserDisplayName = this.User.Name;
        }
    }
}