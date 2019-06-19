﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cibertec.Mvc.ActionFilters
{
    public class ErrorActionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var log = LogManager.GetLogger(typeof(ErrorActionFilter));
            filterContext.ExceptionHandled = true;
            log.Error(filterContext.Exception);
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}
