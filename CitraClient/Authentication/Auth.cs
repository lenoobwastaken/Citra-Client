using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CitraClient.Authentication
{
	public static class Auth
	{
		private static string name = "CitraClient";

		private static string ownerid = "VYqpc8RwOp";

		private static string secret = "29311acbcba23582788b67ae06de349ef3280ed7b13d1d6f14877c5535a487f5";

		private static string version = "1.0";

		public static api KeyAuthApp = new api(name, ownerid, secret, version);

		public static void AuthUser()
		{
		
		}
	}
}
