using CitraClient.API.QM;
using CitraClient.Modules.World;

namespace CitraClient.GUI.QM
{
	public class WorldMenu
	{
		public static QMNestedButton worldMenu;

		public static void InitWorldMenu()
		{
			worldMenu = new QMNestedButton(MainMenu.MainPage, "World", 1f, 1f, "Options for World related features", "<color=#CE389C>Citra:</color> <color=#00d1ed>World</color>");
			new QMSingleButton(worldMenu, 1f, 0f, "Copy Instance\nID", delegate
			{
				WorldModules.CopyInstanceToClipboard();
			}, "Copy the current Instance ID to the Clipboard.");
			new QMSingleButton(worldMenu, 2f, 0f, "Join From\nClipboard", delegate
			{
				WorldModules.JoinInstanceFromClipboard();
			}, "Join Instance from the Clipboard.");
			new QMSingleButton(worldMenu, 3f, 0f, "Optimize\nMirrors", delegate
			{
				MirrorMod.Optimize();
			}, "Lower the quality of mirrors for optimization..");
			new QMSingleButton(worldMenu, 4f, 0f, "Beautify\nMirrors", delegate
			{
				MirrorMod.Beautify();
			}, "Up the quality of mirrors. May lower optimization..");
			new QMSingleButton(worldMenu, 1f, 1f, "Normal\nMirrors", delegate
			{
				MirrorMod.Revert();
			}, "Revert mirrors back to their original quality state.");
		}
	}
}
