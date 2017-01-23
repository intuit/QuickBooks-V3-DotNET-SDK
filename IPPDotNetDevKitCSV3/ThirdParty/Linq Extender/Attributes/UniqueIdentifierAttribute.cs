using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Attributes
{
    
    /// <summary>
    ///  Defines a property to be unique.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueIdentifierAttribute : System.Attribute
    {

    }
}