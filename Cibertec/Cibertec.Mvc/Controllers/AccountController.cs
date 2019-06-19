using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cibertec.Mvc.Models;
using Cibertec.UnitOfWork;
using log4net;

namespace Cibertec.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            var userModel = new UserViewModel() { ReturnUrl = returnUrl };
            return View(userModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(UserViewModel user) {
            if (!ModelState.IsValid) return View(user);

            var validUser = _unit.Users.ValidateUser(user.Email, user.Password);

            if (validUser == null) {
                ModelState.AddModelError("Error", "Invalid Email or Password");
                return View(user);
            }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, validUser.Email),
                new Claim(ClaimTypes.Role, validUser.Roles),
                new Claim(ClaimTypes.Name, $"{validUser.FirstName} {validUser.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, validUser.Id.ToString())
            }, "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignIn(identity);

            return RedirectToLocal(user.ReturnUrl);
        }

        public ActionResult Logout() {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
