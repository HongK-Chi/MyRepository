using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Resource
{
    public static class UrlExtensions
    {
        public static string AbsoluteAction(
            this IUrlHelper url,
            string actionName,
            string controllerName,
            object routeValues = null)
        {
            if (url != null)
                return url.Action(actionName, controllerName, routeValues,
                           url.ActionContext.HttpContext.Request.Scheme);
            else
                return string.Empty;
        }
    }
}
