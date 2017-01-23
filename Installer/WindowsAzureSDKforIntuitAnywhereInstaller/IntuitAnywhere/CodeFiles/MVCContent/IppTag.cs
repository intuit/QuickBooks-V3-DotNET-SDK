#region Using

using System;
using System.Web;
using System.Web.Mvc;

#endregion

namespace IntuitSampleMVC.utils
{
    public class IppTag : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Write("<!--IPP Tag Registration, this is for demonstration purpose only-->");
            HttpContext.Current.Response.Write("<!--remove the blow duplicate HTML tag and place the xmlns:ipp attribute in the original HTML tag-->");
            HttpContext.Current.Response.Write("<html xmlns:ipp='' />");
            HttpContext.Current.Response.Write("<!-- end of IPP Tag Registration-->");
        }
    }
}
