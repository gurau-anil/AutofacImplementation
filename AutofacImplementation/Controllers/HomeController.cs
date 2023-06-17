using AutofacImplementation.Filters;
using AutofacImplementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutofacImplementation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;
        private readonly IWorkContext _workContext;
        public HomeController(ITestService testService, IWorkContext workContext)
        {
            _testService = testService;
            _workContext = workContext;
        }
        [CheckUserAccess]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [CheckUserAccess]
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
