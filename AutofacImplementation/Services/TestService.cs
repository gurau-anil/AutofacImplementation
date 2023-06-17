using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacImplementation.Services
{
    public interface ITestService
    {
        bool Authenticate(string userName, string password);
    }
    public class TestService : ITestService
    {
        public bool Authenticate(string userName, string password)
        {
            return !String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password) && userName == "user" && password == "password";
        }
    }

}