using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacImplementation
{
    public interface IWorkContext
    {
        string User { get; set; }
        string AuthToken { get; set; }
    }
    public class WorkContext : IWorkContext
    {
        public string User { get; set; }
        public string AuthToken { get; set; }
    }
}