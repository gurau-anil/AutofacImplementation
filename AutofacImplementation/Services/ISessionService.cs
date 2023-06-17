using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacImplementation.Services
{
    public interface ISessionService
    {
        string AuthUserName { get; set; }
        string AuthToken { get; set; }
    }
    public class SessionService:ISessionService
    {
        public string AuthUserName { get; set; }
        public string AuthToken { get; set; }
    }
}