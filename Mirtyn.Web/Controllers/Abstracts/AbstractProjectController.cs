using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mirtyn.Web
{
    public abstract class AbstractProjectController<TController> : Controller
    {
        private ILogger<TController> _logger;
        protected ILogger<TController> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<TController>>());


        //private static ILogger logger = LogManager.GetCurrentClassLogger();

        private IConfiguration _configuration;
        protected IConfiguration Configuration => _configuration ?? (_configuration = HttpContext.RequestServices.GetService<IConfiguration>());
    }
}
