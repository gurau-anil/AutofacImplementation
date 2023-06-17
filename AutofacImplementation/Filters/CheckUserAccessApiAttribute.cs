using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AutofacImplementation.Filters
{
    public class CheckUserAccessApiAttribute : ActionFilterAttribute
    {
        private readonly IWorkContext _workContext;
        public CheckUserAccessApiAttribute(IWorkContext workContext)
        {
            _workContext = workContext;
        }
        public CheckUserAccessApiAttribute()
        {

        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
            string authToken = HttpContext.Current.Request.Cookies["authToken"]?.Value;
            string authUser = HttpContext.Current.Request.Cookies["authUser"]?.Value;

            var workContext = actionContext.RequestContext.Configuration.DependencyResolver.GetService(typeof(IWorkContext)) as WorkContext;

            if (workContext != null &&
                workContext.AuthToken != null &&
                workContext.User != null &&
                workContext.User == authUser &&
                workContext.AuthToken == authToken)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Unauthorized access");
            }
            
            base.OnActionExecuting(actionContext);
        }
    }
}