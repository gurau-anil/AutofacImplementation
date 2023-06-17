using AutofacImplementation.Filters;
using AutofacImplementation.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AutofacImplementation.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITestService _testService;
        private readonly IWorkContext _workContext;
        public ValuesController(ITestService testService, IWorkContext workContext)
        {
            _testService = testService;
            _workContext = workContext;
        }
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate(Auth model)
        {
            var isAuthenticated = _testService.Authenticate(model.UserName, model.Password);
            if (isAuthenticated)
            {
                _workContext.AuthToken = Guid.NewGuid().ToString();
                _workContext.User = model.UserName;

                var cookies = HttpContext.Current.Response.Cookies;
                cookies.Add(new HttpCookie("authToken")
                {
                    Path = "/",
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    HttpOnly = true,
                    Value = _workContext.AuthToken

                });
                cookies.Add(new HttpCookie("authUser")
                {
                    Path = "/",
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    HttpOnly = true,
                    Value = model.UserName

                });
            }
            return Ok(new { isAuthenticated = isAuthenticated });
        }
        [HttpGet]
        [Route("value")]
        [CheckUserAccessApi]
        public IHttpActionResult GetValue()
        {
            
            return Ok(new string[] { "value1", "value2" });
        }

    }
    public class Auth
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
