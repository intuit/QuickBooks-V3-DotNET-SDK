using System;

namespace Intuit.Ipp.LinqExtender.Abstraction
{

    /// <summary>
    /// Entry point interface for <see cref="ClassGenerator"/>
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IClassGenerator
    {
        /// <summary>
        /// Builds the dynamic assembly.
        /// </summary>
        /// <returns></returns>
        IClassGenerator BuildDynamicAssembly();
        /// <summary>
        /// Builds a type in the dynamic assembly, if already the type is not created.
        /// </summary>
        /// <param name="parentType">type of object or interfae to implement</param>
        /// <param name="interfaceType">parent interface type.</param>
        /// <returns></returns>
        IClassGenerator CreateType(Type parentType, Type interfaceType);
        /// <summary>
        /// Adds properties to the dynamic type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IClassGenerator AddProperty(string name, object value);
        /// <summary>
        /// Creates a new instance of the dynamically generated type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source object from where to copy the properties.</param>
        /// <returns></returns>
        T CreateInstance<T>(object source);
    }
}