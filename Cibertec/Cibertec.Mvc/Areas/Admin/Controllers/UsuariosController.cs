using Cibertec.Models;
using Cibertec.Mvc.Controllers;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cibertec.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : BaseController
    {
        public UsuariosController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
        }

        public ActionResult Index() {
            var modelo = _unit.Users.GetList();
            return View(modelo);
        }

        public ActionResult UpdateUser(int Id) {
            var user = _unit.Users.GetById(Id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateUser([Bind(Include = "Id, FirstName, LastName, Roles")]  User modelo)
        {
            if (!ModelState.IsValid) return View(modelo);
            var user = _unit.Users.GetById(modelo.Id);
            user.FirstName = modelo.FirstName;
            user.LastName = modelo.LastName;
            user.Roles = modelo.Roles;
            _unit.Users.Update(user);

            return RedirectToAction("Index");
        }
    }
}
