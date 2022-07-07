using System;
using Il2CppSystem.Net;

namespace CitraClient.Utils
{
	public class AutoUpdater
	{
		private const string _path = "";

		public void CheckForUpdate()
		{
			WebClient webClient = new WebClient();
			try
			{
			}
			catch (Exception ex)
			{
				ConsoleUtils.OnLogError(ex.ToString());
				throw;
			}
		}
	}
}
