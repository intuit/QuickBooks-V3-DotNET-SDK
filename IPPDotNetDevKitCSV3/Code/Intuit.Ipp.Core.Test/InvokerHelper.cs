using System;
using System.Reflection;

namespace Intuit.Ipp.Core.Test
{
	/// <summary>
	/// Summary description for InvokeHelper.
	/// </summary>
	public static class InvokeHelper
	{		
		#region Run Method

		/// <summary>
		///		Runs a method on a type, given its parameters. This is useful for
		///		calling private methods.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="strMethod"></param>
		/// <param name="aobjParams"></param>
		/// <returns>The return value of the called method.</returns>
		public static object RunStaticMethod(System.Type t, string strMethod, object [] aobjParams) 
		{
			BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, null, aobjParams, eFlags);
		} //end of method

		public static object RunInstanceMethod(string strMethod, object objInstance, object [] aobjParams) 
		{
			BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(objInstance.GetType(), strMethod, objInstance, aobjParams, eFlags);
		} //end of method

		private static object RunMethod(Type t, string strMethod, object objInstance, object [] aobjParams, BindingFlags eFlags) 
		{
			MethodInfo m;
			try 
			{
                m = t.GetMethod(strMethod, eFlags);
				if (m == null)
				{
					throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t.ToString() + "'.");
				}

				object objRet = m.Invoke(objInstance, aobjParams);
				return objRet;
			}
			catch
			{
				throw;
			}
		} //end of method

		#endregion

	} //end of class

} //end of namespace
