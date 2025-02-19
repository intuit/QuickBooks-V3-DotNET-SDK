using System.Net;
using System.Text;

namespace Intuit.Ipp.Core.RestCalls;

/// <summary>
/// Extension methods for <see cref="WebHeaderCollection"/>
/// </summary>
public static class WebHeaderCollectionExtensions
{
    /// <summary>
    /// Convert <see cref="WebHeaderCollection"/> to string 
    /// </summary>
    /// <param name="headers">The <see cref="WebHeaderCollection"/></param>
    /// <returns>The headers string</returns>
    public static string ConvertHeaderToString(this WebHeaderCollection headers)
    {
        var headersStringBuilder = new StringBuilder();
        foreach (string header in headers)
        {
            headersStringBuilder.Append($"{header}: {headers[header]};");
        }

        return headersStringBuilder.ToString();
    }
}
