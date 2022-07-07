using System.Windows.Forms;
using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Console;
using CitraClient.Modules.Exploits;
using CitraClient.Modules.Player;
using CitraClient.Utils;
using VRC;

namespace CitraClient.GUI.QM.Target
{
	public class TargetMenu
	{
		public static QMNestedButton sTMenu;

		public static void Init()
		{
			sTMenu = new QMNestedButton(TargetMainMenu.targetMenu, "Target", 1f, 0f, "Options that target other players", "Target Menu");
			new QMSingleButton(sTMenu, 1f, 0f, "Plunger Hat", delegate
			{
				RuntimeConfig.isPlungerHat = !RuntimeConfig.isPlungerHat;
				PickupExploits.PlungerHat(PlayerUtils.SelectedPlayer()).Start();
			}, "Gives Player a stylish Plunger Hat (Black Cat Only)");
			new QMSingleButton(sTMenu, 2f, 0f, "Teleport to", delegate
			{
				PlayerModules.TeleportSelfToPlayer(PlayerUtils.SelectedPlayer());
			}, "Teleport Self to selected player");
			new QMSingleButton(sTMenu, 3f, 0f, "Teleport Pickups", delegate
			{
				PickupExploits.PickupsToPlayer(PlayerUtils.SelectedPlayer());
			}, "Teleport all World Pickups to selected player");
			new QMSingleButton(sTMenu, 4f, 0f, "Creepy Pickups", delegate
			{
				RuntimeConfig.isPickupStare = !RuntimeConfig.isPickupStare;
				PickupExploits.PickupStare(PlayerUtils.SelectedPlayer()).Start();
			}, "Makes World Pickups stare at selected player");
			new QMSingleButton(sTMenu, 1f, 1f, "Force Clone", delegate
			{
				PlayerModules.ForceCloneAvatar(PlayerUtils.SelectedPlayer());
			}, "Clone Player's avatar without their permission. (Public Avatar's Only)");
			new QMSingleButton(sTMenu, 2f, 1f, "Reload Avatar", delegate
			{
				PlayerUtils.ReloadAvatar(PlayerUtils.SelectedPlayer()._vrcplayer);
			}, "Reloads selected Player's Avatar");
			new QMSingleButton(sTMenu, 3f, 1f, "Swastika Hat", delegate
			{
				Swastika.swastikaTargetPlayer = PlayerUtils.SelectedPlayer();
				Swastika.ToggleSwastikaFollow();
			}, "卐");
			new QMSingleButton(sTMenu, 4f, 1f, "Log Info", delegate
			{
				PlayerUtils.LogInfoOnUser(PlayerUtils.SelectedPlayer());
			}, "Log a good amount of information about the user to the Console.");
			new QMSingleButton(sTMenu, 1f, 2f, "Copy Avatar ID", delegate
			{
				Player player = PlayerUtils.SelectedPlayer();
				Clipboard.SetText(player.prop_ApiAvatar_0.id);
				CenterConsole.Log(CenterConsole.LogsType.AVATAR, "Copied \"" + player.prop_ApiAvatar_0.id + "\".");
				ConsoleUtils.OnLogInfo("Copied \"" + player.prop_ApiAvatar_0.id + "\".");
			}, "Copy Selected Players Avatar ID to the ClipBoard");
			new QMSingleButton(sTMenu, 2f, 2f, "Pickup Orbit", delegate
			{
				PickupExploits.PickupOrbitTarget();
			}, "Orbit selected player with pickups");
			new QMSingleButton(sTMenu, 3f, 2f, "Pickup Head\nSwarm", delegate
			{
				RuntimeConfig.isPickupHeadSwarm = !RuntimeConfig.isPickupHeadSwarm;
				PickupExploits.PickupHeadSwarm(PlayerUtils.SelectedPlayer()).Start();
			}, "Bring all pickups to Selected Players head.");
			new QMSingleButton(sTMenu, 4f, 3f, "Head", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.HEAD, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
			new QMSingleButton(sTMenu, 3f, 3f, "Left Hand", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.LEFT_HAND, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
			new QMSingleButton(sTMenu, 2f, 3f, "Left Foot", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.LEFT_FOOT, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
			new QMSingleButton(sTMenu, 2f, 3.5f, "Right Hand", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.RIGHT_HAND, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
			new QMSingleButton(sTMenu, 3f, 3.5f, "Right Foot", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.RIGHT_FOOT, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
			new QMSingleButton(sTMenu, 4f, 3.5f, "STOP", delegate
			{
				AttachToPlayer._shouldAttach = !AttachToPlayer._shouldAttach;
				AttachToPlayer.AttachTo(AttachToPlayer.BodyPart.NOT_ATTACHED, PlayerUtils.SelectedPlayer()).Start();
			}, "", null, halfBtn: true);
		}
	}
}
