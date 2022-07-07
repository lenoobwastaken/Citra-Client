using System.IO;
using CitraClient.Utils.Managers;
using Newtonsoft.Json;

namespace CitraClient.Config
{
	public static class Configuration
	{
		public static Settings settings { get; set; }

		public static void Initialize()
		{
			if (!File.Exists(FileManager.ConfigFile))
			{
				File.Create(FileManager.ConfigFile).Close();
				File.WriteAllText(FileManager.ConfigFile, JsonConvert.SerializeObject(new Settings(), Formatting.Indented));
			}
			Load();
		}

		public static void Load()
		{
			settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(FileManager.ConfigFile));
		}

		public static void Save()
		{
			File.WriteAllText(FileManager.ConfigFile, JsonConvert.SerializeObject(settings, Formatting.Indented));
		}

		public static Settings GetConfig()
		{
			return settings;
		}
	}
}
