

using Autofac.Integration.Mvc;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutofacImplementation.Filters
{
    public class CheckUserAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
            string authToken = filterContext.HttpContext.Request.Cookies["authToken"]?.Value;
            string authUser = filterContext.HttpContext.Request.Cookies["authUser"]?.Value;

            var workContext = AutofacDependencyResolver.Current.GetService<IWorkContext>();

            if (workContext!=null && 
                workContext.AuthToken != null && 
                workContext.User != null && 
                workContext.User == authUser && 
                workContext.AuthToken == authToken)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();

                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Home");

                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            
        }
    }
}