using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Submenus.Sliders;
using CitraClient.Modules.Protections;
using CitraClient.Utils;
using Il2CppSystem.Collections.Generic;
using VRC;

namespace CitraClient.GUI.QM.Submenus
{
	public class SafetyMenu
	{
		public static QMNestedButton safetyMenu;

		public static QMToggleButton teleportToSpaceToggleButton;

		private static bool _isDebug = true;

		public static void InitSafetyMenu()
		{
			safetyMenu = new QMNestedButton(MainMenu.MainPage, "Safety", 4f, 1f, "Safety functions of <color=#CE389C>Citra</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Safety</color>");
			new QMToggleButton(safetyMenu, 1f, 0f, "RPC Protections", delegate
			{
				Configuration.GetConfig().rpcProtection = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().rpcProtection = false;
				Configuration.Save();
			}, "Attempts to prevent any type of RPC exploits", Configuration.GetConfig().rpcProtection);
			new QMToggleButton(safetyMenu, 2f, 0f, "USpeak Protections", delegate
			{
				Configuration.GetConfig().uspeakProtection = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().uspeakProtection = false;
				Configuration.Save();
			}, "Attempts to prevent any type of USpeak exploits", Configuration.GetConfig().rpcProtection);
			new QMToggleButton(safetyMenu, 3f, 0f, "Ping Spoof", delegate
			{
				Configuration.GetConfig().pingSpoof = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().pingSpoof = false;
				Configuration.Save();
			}, "Changes your server ping so people view it as different", Configuration.GetConfig().pingSpoof);
			new QMToggleButton(safetyMenu, 4f, 0f, "FPS Spoof", delegate
			{
				Configuration.GetConfig().fpsSpoof = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().fpsSpoof = false;
				Configuration.Save();
			}, "Changes your server FPS so people view it as different", Configuration.GetConfig().fpsSpoof);
			new QMToggleButton(safetyMenu, 1f, 1f, "Pickup Safety", delegate
			{
				Configuration.GetConfig().pickupsToggle = true;
				WorldProtections.EnableDisablePickups(state: true);
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().pickupsToggle = false;
				WorldProtections.EnableDisablePickups(state: false);
				Configuration.Save();
			}, "Locally toggle Pickups on and off.", Configuration.GetConfig().pickupsToggle);
			new QMToggleButton(safetyMenu, 2f, 1f, "Pen Safety", delegate
			{
				Configuration.GetConfig().penToggle = true;
				WorldProtections.EnableDisablePens(state: true);
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().penToggle = false;
				WorldProtections.EnableDisablePens(state: false);
				Configuration.Save();
			}, "Locally toggle Pens on and off.", Configuration.GetConfig().penToggle);
			new QMToggleButton(safetyMenu, 3f, 1f, "Video Player Safety", delegate
			{
				Configuration.GetConfig().videoPlayerToggle = true;
				WorldProtections.EnableDisableVideoPlayers(state: true);
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().videoPlayerToggle = false;
				WorldProtections.EnableDisableVideoPlayers(state: false);
				Configuration.Save();
			}, "Locally toggle Video Players on and off.", Configuration.GetConfig().videoPlayerToggle);
			new QMToggleButton(safetyMenu, 4f, 1f, "Local Hide", delegate
			{
				TargetCrashMenu.DisableEnableLocalPlayer(state: true);
			}, delegate
			{
				TargetCrashMenu.DisableEnableLocalPlayer(state: false);
			}, "Locally hide your own avatar..", Configuration.GetConfig().localHide);
			new QMToggleButton(safetyMenu, 1f, 2f, "Mute All\nQuest Users", delegate
			{
				Configuration.GetConfig().questUserToggle = true;
				List<Player>.Enumerator enumerator2 = PlayerUtils.GetAllPlayersToList().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Player current2 = enumerator2.Current;
					PlayerUtils.CheckForQuest(current2, state: true);
				}
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().questUserToggle = false;
				List<Player>.Enumerator enumerator = PlayerUtils.GetAllPlayersToList().GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player current = enumerator.Current;
					PlayerUtils.CheckForQuest(current, state: false);
				}
				Configuration.Save();
			}, "Locally mute all Quest Users. Lmao", Configuration.GetConfig().questUserToggle);
			new QMSingleButton(safetyMenu, 2f, 2f, "Get Off\nMy Head", delegate
			{
				WorldProtections.TeleportToSpace(3f).Start();
			}, "Used to crash Users that use Attachment mods on you.");
			new QMSingleButton(safetyMenu, 3f, 2f, "Destroy Portals", delegate
			{
				WorldProtections.DestroyAllPortals();
			}, "Destroys all active Portals in Lobby.");
			SafetySliders.Init();
		}
	}
}
