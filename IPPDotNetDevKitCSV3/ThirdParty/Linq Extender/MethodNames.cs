
using System;
namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class MethodNames
    {
        public static string StartsWith = "StartsWith";
        public static string EndsWith = "EndsWith";
        public static string Contains = "Contains";
        public static string In = "In";

        public static string Group = "GroupBy";
        internal const string Join = "Join";
        internal const string Take = "Take";
        internal const string Skip = "Skip";
        internal const string Where = "Where";
        internal const string Select = "Select";
        internal const string Orderby = "OrderBy";
        internal const string ThenBy = "ThenBy";
        internal const string Orderbydesc = "OrderByDescending";
    }
}