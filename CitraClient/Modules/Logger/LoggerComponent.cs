using System.Collections.Generic;
using System.IO;
using CitraClient.Utils.Managers;

namespace CitraClient.Modules.Logger
{
	public static class LoggerComponent
	{
		public static readonly List<string> AvatarLogs = new List<string>();

		public static readonly List<string> PlayerLogs = new List<string>();

		public static readonly List<string> WorldLogs = new List<string>();

		public static void LogAvatar(string text)
		{
			if (AvatarLogs.Count == 0)
			{
				File.WriteAllText(FileManager.AviLogFile ?? "", "{");
			}
			AvatarLogs.Add(text);
			File.WriteAllLines(FileManager.AviLogFile ?? "", AvatarLogs.ToArray());
		}

		public static void LogPlayer(string text)
		{
			if (PlayerLogs.Count == 0)
			{
				File.WriteAllText(FileManager.PlayerLogFile ?? "", "{");
			}
			PlayerLogs.Add(text);
			File.WriteAllLines(FileManager.PlayerLogFile ?? "", PlayerLogs.ToArray());
		}

		public static void LogWorld(string text)
		{
			if (WorldLogs.Count == 0)
			{
				File.WriteAllText(FileManager.WorldLogFile ?? "", "{");
			}
			WorldLogs.Add(text);
			File.WriteAllLines(FileManager.WorldLogFile ?? "", WorldLogs.ToArray());
		}
	}
}
