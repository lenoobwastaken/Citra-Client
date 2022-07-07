using CitraClient.API.QM;
using CitraClient.Config;

namespace CitraClient.GUI.QM
{
	public class DiscordMenu
	{
		public static QMNestedButton discordMenu;

		public static void InitDiscordMenu()
		{
			discordMenu = new QMNestedButton(MainMenu.MainPage, "Discord", 2f, 0f, "Options for the Discord related features", "<color=#CE389C>Citra:</color> <color=#00d1ed>Discord</color>");
			new QMToggleButton(discordMenu, 1f, 0f, "Hide Username", delegate
			{
				Configuration.GetConfig().hideName = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().hideName = false;
				Configuration.Save();
			}, "Disable your display name from being shown in the clients Discord RPC", Configuration.GetConfig().hideName);
		}
	}
}
