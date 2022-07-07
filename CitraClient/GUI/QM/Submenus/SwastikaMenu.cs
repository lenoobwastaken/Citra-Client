using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.Modules.Exploits;

namespace CitraClient.GUI.QM.Submenus
{
	public static class SwastikaMenu
	{
		public static QMNestedButton swastikaMenu;

		public static QMToggleButton swastikaToggleButton;

		public static void InitSwastikaMenu()
		{
			swastikaMenu = new QMNestedButton(ExploitMenu.exploitMenu, "Swastika", 4f, 3f, "Funny functions of <color=#CE389C>Citra</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Funny</color>");
			swastikaToggleButton = new QMToggleButton(swastikaMenu, 4f, 3f, "Toggle", delegate
			{
				RuntimeConfig.isDownStairsSpam = true;
			}, delegate
			{
				swastikaToggleButton.SetToggleState(newState: false);
			}, "Toggles Swastika on/off");
			new QMSingleButton(swastikaMenu, 1f, 0f, "Create", delegate
			{
				Swastika.ToggleSwastikaControl();
			}, string.Empty);
			new QMSlider(swastikaMenu, "Swastika Movement\nSpeed", 500f, -100f, delegate(float f)
			{
				Configuration.GetConfig().swastikaMovement = f;
				Configuration.Save();
			}, "Change the speed the swastika moves around", 1f, 100f, Configuration.GetConfig().swastikaMovement);
		}
	}
}
