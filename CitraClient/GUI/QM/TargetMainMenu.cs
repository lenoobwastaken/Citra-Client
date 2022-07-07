using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Target;
using CitraClient.Modules.Exploits;

namespace CitraClient.GUI.QM
{
	public class TargetMainMenu
	{
		public static QMNestedButton targetMenu;

		public static void InitTargetMenu()
		{
			targetMenu = new QMNestedButton("Menu_SelectedUser_Local", "Target Menu", 2.5f, -0.55f, "Citra Target menu", "Target Options", halfButton: true);
			TargetMenu.Init();
			new QMSingleButton(targetMenu, 2f, 0f, "Stop All", delegate
			{
				Swastika.isSwastikaFollow = false;
				RuntimeConfig.isPickupStare = false;
				RuntimeConfig.isPlungerHat = false;
				RuntimeConfig.isPickupOrbit = false;
				RuntimeConfig.isPickupHeadSwarm = false;
			}, "Stop All Targeted Mods");
		}
	}
}
