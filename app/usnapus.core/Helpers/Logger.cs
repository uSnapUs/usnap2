using System;

namespace uSnapUs.Core.Helpers
{
	public static class Logger
	{
		public static void Trace(string message)
		{
			var method = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod();
			Console.WriteLine("{0} from {1} in {2}",message, method.Name,method.DeclaringType.Name);
		}
		public static void Exception(Exception e)
		{
			var method = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod();
			Console.WriteLine("Exception '{0}' from {1} in {2}",e.Message, method.Name,method.DeclaringType.Name);
		}
	}
}

