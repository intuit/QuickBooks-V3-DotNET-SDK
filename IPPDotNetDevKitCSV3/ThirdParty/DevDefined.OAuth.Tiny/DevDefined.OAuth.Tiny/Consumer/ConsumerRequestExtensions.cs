using System;
using System.Collections;
using System.Collections.Specialized;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Utility;

namespace DevDefined.OAuth.Consumer
{
	public static class ConsumerRequestExtensions
	{
		static void ApplyParameters(NameValueCollection destination, IDictionary additions)
		{
			if (additions == null) throw new ArgumentNullException("additions");

			foreach (string parameter in additions.Keys)
			{
				destination[parameter] = Convert.ToString(additions[parameter]);
			}
		}
		static void ApplyParameters(NameValueCollection destination, NameValueCollection additions)
		{
			if (additions == null) throw new ArgumentNullException("additions");
			ApplyParameters(destination, new ReflectionBasedDictionaryAdapter(additions));

			//foreach (var x in additions.AllKeys)
			//{
			//    destination[x] = Convert.ToString(additions[x]);
			//}
		}

		public static IConsumerRequest ForMethod(IConsumerRequest request, string method)
		{
			request.Context.RequestMethod = method;
			return request;
		}

		public static IConsumerRequest ForUri(IConsumerRequest request, Uri uri)
		{
			request.Context.RawUri = uri;
			return request;
		}


		public static IConsumerRequest WithFormParameters(IConsumerRequest request, NameValueCollection dictionary)
		{
			ApplyParameters(request.Context.FormEncodedParameters, dictionary);
			return request;
		}


		public static IConsumerRequest WithQueryParameters(IConsumerRequest request, NameValueCollection dictionary)
		{
			ApplyParameters(request.Context.QueryParameters, dictionary);
			return request;
		}



		public static IConsumerRequest WithCookies(IConsumerRequest request, NameValueCollection dictionary)
		{
			ApplyParameters(request.Context.Cookies, dictionary);
			return request;
		}


		public static IConsumerRequest WithHeaders(IConsumerRequest request, NameValueCollection dictionary)
		{
			ApplyParameters(request.Context.Headers, dictionary);
			return request;
		}



		public static IConsumerRequest AlterContext(IConsumerRequest request, Action<IOAuthContext> alteration)
		{
			alteration(request.Context);
			return request;
		}

		public delegate TResult SelectFunc<T, TResult>(T arg);

		public static T Select<T>(IConsumerRequest request, SelectFunc<NameValueCollection, T> selectFunc)
		{
			try
			{
				return selectFunc(request.ToBodyParameters());
			}
			catch (ArgumentNullException)
			{
				throw Error.FailedToParseResponse(request.ToString());
			}
		}
	}
}