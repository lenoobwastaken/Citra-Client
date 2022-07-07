using System.IO;
using System.Net;
using MelonLoader;

namespace CitraClient.Utils.Managers
{
	public static class FileManager
	{
		public static string BaseDirectory = MelonUtils.BaseDirectory + "\\Citra\\";

		public static string DiscordDirectory = BaseDirectory + "Discord\\";

		public static string ConfigFile = BaseDirectory + "Config.json";

		public static string DiscordFile = DiscordDirectory + "discordrpc.dll";

		public static string AviLogFile = BaseDirectory + "\\AvatarLog.txt";

		public static string WorldLogFile = BaseDirectory + "\\WorldLog.txt";

		public static string PlayerLogFile = BaseDirectory + "\\PlayerLog.txt";

		public static string SkyboxDirectory = BaseDirectory + "SkyBoxes\\";

		public static string InstanceHistoryFile = BaseDirectory + "\\InstanceHistory.txt";

		public static string AudioDirectory = BaseDirectory + "Audios\\";

		public static string AvatarAssetDirectory = BaseDirectory + "VRCA\\";

		public static string WorldAssetDirectory = BaseDirectory + "VRCW\\";

		public static string AuthFile = BaseDirectory + "\\Auth.txt";

		public static string CustomNamePlates = BaseDirectory + "\\CustomNamePlates.txt";

		public static void Initialize()
		{
			if (!Directory.Exists(BaseDirectory))
			{
				Directory.CreateDirectory(BaseDirectory);
			}
			if (!Directory.Exists(DiscordDirectory))
			{
				Directory.CreateDirectory(DiscordDirectory);
			}
			if (!File.Exists(AviLogFile))
			{
				File.Create(AviLogFile);
			}
			if (!File.Exists(WorldLogFile))
			{
				File.Create(WorldLogFile);
			}
			if (!File.Exists(PlayerLogFile))
			{
				File.Create(PlayerLogFile);
			}
			if (!Directory.Exists(SkyboxDirectory))
			{
				Directory.CreateDirectory(SkyboxDirectory);
			}
			if (!File.Exists(InstanceHistoryFile))
			{
				File.Create(InstanceHistoryFile);
			}
			if (!File.Exists(AuthFile))
			{
				File.Create(AuthFile);
			}
			if (!File.Exists(CustomNamePlates))
			{
				File.Create(CustomNamePlates);
			}
			using WebClient webClient = new WebClient();
			if (!File.Exists(DiscordFile))
			{
				webClient.DownloadFile("https://citra.cc/no/discordrpc.dll", DiscordFile);
			}
		}
	}
}
