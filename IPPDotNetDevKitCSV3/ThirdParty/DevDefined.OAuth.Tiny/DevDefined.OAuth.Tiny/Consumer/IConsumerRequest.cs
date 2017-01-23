using System.Collections.Specialized;
using DevDefined.OAuth.Framework;

namespace DevDefined.OAuth.Consumer
{
  public interface IConsumerRequest
  {
    IOAuthContext Context { get; }
    NameValueCollection ToBodyParameters();
    RequestDescription GetRequestDescription();
    IConsumerRequest SignWithoutToken();
    IConsumerRequest SignWithToken();
    IConsumerRequest SignWithToken(IToken token);
  }
}