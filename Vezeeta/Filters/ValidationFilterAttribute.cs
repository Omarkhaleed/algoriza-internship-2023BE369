using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILogger<ValidationFilterAttribute> _logger;

         public ValidationFilterAttribute( ILogger<ValidationFilterAttribute> ilogger) { 
            _logger = ilogger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var Inputs = context.ActionArguments.SingleOrDefault(p=> p.Value is IEntityEntryGraphIterator);
       
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

          
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Result is null)
            //{
            //    _logger.LogInformation("Action returned a null result");
            //    context.Result = new NotFoundResult();
            //}
            //if (context.Result is ObjectResult objectResult && objectResult.Value != null)
            //{
            //    context.Result = new OkObjectResult(objectResult.Value);
            //}

        }

    }
}
