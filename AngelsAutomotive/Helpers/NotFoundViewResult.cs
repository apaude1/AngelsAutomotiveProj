using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AngelsAutomotive.Helpers
{
    public class NotFoundViewResult : ViewResult //this class will override FrameworkCore's ViewResult class, in order to overlay our view
    {
        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
