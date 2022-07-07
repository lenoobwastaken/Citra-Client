using CitraClient.API.QM;
using CitraClient.Config;

namespace CitraClient.GUI.QM
{
	public class CustomizeUIMenu
	{
		public static QMNestedButton customizeUIMenu;

		public static void InitCustomizeUIMenu()
		{
			customizeUIMenu = new QMNestedButton(MainMenu.MainPage, "Customize UI", 4f, 0f, "Menu that contains different options for Menu customization", "<color=#CE389C>Citra:</color> <color=#00d1ed>Customize UI</color>");
			new QMToggleButton(customizeUIMenu, 1f, 0f, "Cursor Line", delegate
			{
				Configuration.GetConfig().cursorLineRenderer = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().cursorLineRenderer = false;
				Configuration.Save();
			}, "Toggles the line renderer on the Cursor", Configuration.GetConfig().cursorLineRenderer);
			new QMToggleButton(customizeUIMenu, 2f, 0f, "Cursor Colors", delegate
			{
				Configuration.GetConfig().cursorColors = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().cursorColors = false;
				Configuration.Save();
			}, "Toggles the line renderer on the Cursor", Configuration.GetConfig().cursorColors);
		}
	}
}
