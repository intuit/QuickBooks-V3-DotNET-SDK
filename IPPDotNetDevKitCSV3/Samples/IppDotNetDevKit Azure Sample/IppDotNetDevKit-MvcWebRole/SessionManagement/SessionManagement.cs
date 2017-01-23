using System.Web;
using Intuit.Ipp.Data;
using IppDotNetDevKit_MvcWebRole.Models;

namespace IppDotNetDevKit_MvcWebRole.SessionManagement
{
    public class SessionManagement
    {
        internal void AddToSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        internal void AddCustomerToSession(string key, Customer customer)
        {
            HttpContext.Current.Session[key] = customer;
        }

        internal SettingsModel GetSettingsModelFromSession(string key)
        {
            return HttpContext.Current.Session[key] as SettingsModel;
        }
    }
}