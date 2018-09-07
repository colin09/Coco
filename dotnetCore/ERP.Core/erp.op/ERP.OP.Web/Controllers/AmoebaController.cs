using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ERP.Common.Infrastructure.Ioc;
using ERP.Common.Infrastructure.Log;
using ERP.Common.Infrastructure.Models;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;
using ERP.Domain.IService.AmoebaModule.AmoebaReportModule;
using ERP.OP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.OP.Web.Controllers {

    public class AmoebaController : Controller {

        // private readonly ILogger _log;

        // public AmoebaController (ILogger log) {
        //     _log = log;
        // }

        public IActionResult List () {

            using (var scope = IocManager.BeginLifetimeScope ()) {
                var service = scope.Resolve<IAmoebaReportQueryService> ();
                var search = new AmoebaReportSearch { DataMonth = "2018-07" };
                var pager = new Pager { PageIndex = 1, PageSize = 20 };
                var result = service.QuerySimpleInfo (search, pager);

                var log = scope.Resolve<ILogger> ();
                log.Info (result);

                return View (result.ToList ());
            }
        }

    }
}