using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProjectBoostLadder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoostLadder.Controllers
{
    public class ProjectController<TController> : Controller
    {
        protected readonly ILogger<TController> _logger;

        protected ProjectController()
        {
            
        }

        protected ProjectController(ILogger<TController> logger)
        {
            _logger = logger;
        }


        //    protected ProjectController()
        //    {

        //    }

        //    protected ProjectController(ILogger logger)
        //    {
        //        _logger = logger;
        //    }

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    base.OnActionExecuted(context);
        //}

        //public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    return base.OnActionExecutionAsync(context, next);
        //}
    }
}
