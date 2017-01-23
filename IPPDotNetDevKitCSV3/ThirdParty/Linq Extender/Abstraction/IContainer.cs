using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Abstraction
{

    /// <summary>
    /// Defaines common propeties for accessing <see cref="Bucket"/> or <see cref="BucketItem"/>.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IContainer
    {
        /// <summary>
        /// Gets the current container.
        /// </summary>
        IContainer Container { get;}
        /// <summary>
        /// Gets the name of the current container.
        /// </summary>
        string Name { get; }
    }
}
