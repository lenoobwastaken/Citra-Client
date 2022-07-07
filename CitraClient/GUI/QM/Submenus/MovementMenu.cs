using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Submenus.Sliders;
using CitraClient.Modules.Movement;
using CitraClient.Modules.Player;

namespace CitraClient.GUI.QM.Submenus
{
	public static class MovementMenu
	{
		public static QMToggleButton noclipButton;

		public static QMToggleButton basicFlyButton;

		public static QMToggleButton directionalFlyButton;

		public static QMNestedButton movementMenu;

		public static QMToggleButton infiniteJumpButton;

		public static QMToggleButton rocketJumpButton;

		public static QMToggleButton mouseTeleportButton;

		public static QMToggleButton beyBladeToggleButton;

		public static QMToggleButton allowJumpingToggleButton;

		public static QMToggleButton speedToggleButton;

		public static QMToggleButton headFlipperToggleButton;

		public static void InitMovementMenu()
		{
			movementMenu = new QMNestedButton(MainMenu.MainPage, "Movement", 2f, 1f, "Movement functions of <color=#CE389C>Citra</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Movement</color>");
			basicFlyButton = new QMToggleButton(movementMenu, 1f, 0f, "Flight", delegate
			{
				RuntimeConfig.basicfly = true;
				Configuration.Save();
				TempMovement.EnableFlight();
			}, delegate
			{
				RuntimeConfig.basicfly = false;
				Configuration.Save();
				TempMovement.DisableFlight();
			}, "Enables the normal fly mode.");
			beyBladeToggleButton = new QMToggleButton(movementMenu, 2f, 0f, "BeyBlade Mode", delegate
			{
				TempMovement.BeyBladeMode(state: true);
			}, delegate
			{
				TempMovement.BeyBladeMode(state: false);
				beyBladeToggleButton.SetToggleState(newState: false);
			}, "Spin around on your head like a BeyBlade.");
			mouseTeleportButton = new QMToggleButton(movementMenu, 3f, 0f, "Mouse Teleport", delegate
			{
				RuntimeConfig.raycastTeleport = true;
			}, delegate
			{
				RuntimeConfig.raycastTeleport = false;
				mouseTeleportButton.SetToggleState(newState: false);
			}, "Left Ctrl + Mouse Click to teleport.");
			speedToggleButton = new QMToggleButton(movementMenu, 4f, 0f, "Speed", delegate
			{
				RuntimeConfig.isSpeed = true;
				TempMovement.SpeedHandler();
			}, delegate
			{
				RuntimeConfig.isSpeed = false;
				TempMovement.SpeedHandler();
				speedToggleButton.SetToggleState(newState: false);
			}, "Toggles fast movement.");
			infiniteJumpButton = new QMToggleButton(movementMenu, 1f, 1f, "Infinite Jump", delegate
			{
				RuntimeConfig.infiniteJump = true;
				Configuration.Save();
			}, delegate
			{
				RuntimeConfig.infiniteJump = false;
				Configuration.Save();
				infiniteJumpButton.SetToggleState(newState: false);
			}, "Enables the ability to jump infinite times.");
			rocketJumpButton = new QMToggleButton(movementMenu, 2f, 1f, "Rocket Shoes", delegate
			{
				RuntimeConfig.jetPackJump = true;
			}, delegate
			{
				RuntimeConfig.jetPackJump = false;
				rocketJumpButton.SetToggleState(newState: false);
			}, "Creates Rockets on your shoes.");
			allowJumpingToggleButton = new QMToggleButton(movementMenu, 3f, 1f, "Enable Jumping", delegate
			{
				allowJumpingToggleButton.SetToggleState(newState: true);
				TempMovement.EnableDisableJumping(state: true);
			}, delegate
			{
				allowJumpingToggleButton.SetToggleState(newState: false);
				TempMovement.EnableDisableJumping(state: false);
			}, "Used to enabled jumping in worlds that don't allow it.");
			headFlipperToggleButton = new QMToggleButton(movementMenu, 4f, 1f, "Head Flipper", delegate
			{
				RuntimeConfig.headFlipped = true;
				Configuration.Save();
				PlayerModules.EnableDisableHeadFlipper(flipped: true);
			}, delegate
			{
				RuntimeConfig.headFlipped = false;
				Configuration.Save();
				PlayerModules.EnableDisableHeadFlipper(flipped: false);
			}, "Toggle to unlock your neck movement.");
			MovementSliders.Init();
		}
	}
}
