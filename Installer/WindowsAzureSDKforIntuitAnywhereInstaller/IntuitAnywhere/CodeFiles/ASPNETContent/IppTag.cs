using System;
using System.Web;

namespace IntuitSampleWebsite.utils
{
    public class IppTag : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            
            context.BeginRequest += (new EventHandler(OnLogRequest));
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            app.Context.Response.Write("<!--IPP Tag Registration, this is for demonstration purpose only-->");
            app.Context.Response.Write("<!--remove the blow duplicate HTML tag and place the xmlns:ipp attribute in the original HTML tag-->");
            app.Context.Response.Write("<html xmlns:ipp='' />");
            app.Context.Response.Write("<!-- end of IPP Tag Registration-->");
        }
    }
}
