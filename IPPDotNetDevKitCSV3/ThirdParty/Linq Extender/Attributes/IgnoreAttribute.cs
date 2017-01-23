using System;

namespace Intuit.Ipp.LinqExtender.Attributes
{
    
    /// <summary>
    /// Under this attribute present, property will be ignored by extender.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : System.Attribute
    {
  
    }
}