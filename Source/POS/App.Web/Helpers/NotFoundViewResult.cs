using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Web.Helpers
{    
    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
