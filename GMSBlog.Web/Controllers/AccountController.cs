using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;

namespace GMSBlog.Web.Controllers
{
    public partial class AccountController : BaseBlogController
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult LogOn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult LogOn(string username, string password, string returnUrl)
        {
            if (FormsAuthentication.Authenticate(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl);
            }
            else
            {
                TempData["username"] = username;
                TempData["showErrorMessage"] = true;
                return View();
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult LogOut(string returnUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(returnUrl);
        }
    }
}
