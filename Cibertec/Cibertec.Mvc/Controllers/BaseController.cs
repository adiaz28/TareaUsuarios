using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.Controllers
{
    //[Authorize]
    public class BaseController : Controller
    {
        protected readonly ILog _log;
        protected readonly IUnitOfWork _unit;
        public BaseController(ILog log, IUnitOfWork unit)
        {
            this._log = log;
            this._unit = unit;
        }
    }
}