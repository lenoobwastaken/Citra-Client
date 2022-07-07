using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.GUI.QM
{
	public class KeybindMenu
	{
		public static bool waitingForBF;

		public static bool waitingForDF;

		public static bool waitingForNC;

		public static bool waitingForTP;

		public static QMSingleButton ncKeyButton { get; private set; }

		public static QMSingleButton dfKeyButton { get; private set; }

		public static QMSingleButton bfKeyButton { get; private set; }

		public static QMSingleButton tpKeyButton { get; private set; }

		public static void Init()
		{
			QMNestedButton qMNestedButton = new QMNestedButton(MainMenu.MainPage, "Keybinds", 4f, 3f, "Set your keybinds for the clients different features", "<color=#CE389C>Citra:</color> <color=#00d1ed>Keybinds</color>");
			new QMToggleButton(qMNestedButton, 1f, 0f, "Basic Fly Keybind", delegate
			{
				Configuration.GetConfig().basicFlyKeybind = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().basicFlyKeybind = false;
				Configuration.Save();
			}, "Toggles whether you can use a keybind to activate Basic Fly", Configuration.GetConfig().basicFlyKeybind);
			KeyCode basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().basicFlyKeybindNum;
			bfKeyButton = new QMSingleButton(qMNestedButton, 1f, 0.93f, basicFlyKeybindNum.ToString(), delegate
			{
				waitingForBF = true;
				bfKeyButton.SetButtonText("Waiting..");
				PlayerUtils.ToggleInput(b: false);
			}, "Waits for an input for your Basic Fly Keybind", null, halfBtn: true);
			new QMToggleButton(qMNestedButton, 2f, 0f, "Directional Fly Keybind", delegate
			{
				Configuration.GetConfig().directionalFlyKeybind = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().directionalFlyKeybind = false;
				Configuration.Save();
			}, "Toggles whether you can use a keybind to activate Directional Fly", Configuration.GetConfig().directionalFlyKeybind);
			basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().directionalFlyKeybindNum;
			dfKeyButton = new QMSingleButton(qMNestedButton, 2f, 0.93f, basicFlyKeybindNum.ToString(), delegate
			{
				waitingForDF = true;
				dfKeyButton.SetButtonText("Waiting..");
				PlayerUtils.ToggleInput(b: false);
			}, "Waits for an input for your Directional Fly Keybind", null, halfBtn: true);
			new QMToggleButton(qMNestedButton, 3f, 0f, "Noclip Keybind", delegate
			{
				Configuration.GetConfig().noclipKeybind = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().noclipKeybind = false;
				Configuration.Save();
			}, "Toggles whether you can use a keybind to activate Noclip", Configuration.GetConfig().noclipKeybind);
			basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().noclipKeybindNum;
			ncKeyButton = new QMSingleButton(qMNestedButton, 3f, 0.93f, basicFlyKeybindNum.ToString(), delegate
			{
				waitingForNC = true;
				ncKeyButton.SetButtonText("Waiting..");
				PlayerUtils.ToggleInput(b: false);
			}, "Waits for an input for your Noclip Keybind", null, halfBtn: true);
			new QMToggleButton(qMNestedButton, 4f, 0f, "Thirdperson Keybind", delegate
			{
				Configuration.GetConfig().thirdPersonKeybind = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().thirdPersonKeybind = false;
				Configuration.Save();
			}, "Toggles whether you can use a keybind to activate third person", Configuration.GetConfig().thirdPersonKeybind);
			basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().thirdPersonKeybindNum;
			tpKeyButton = new QMSingleButton(qMNestedButton, 4f, 0.93f, basicFlyKeybindNum.ToString(), delegate
			{
				waitingForTP = true;
				tpKeyButton.SetButtonText("Waiting..");
				PlayerUtils.ToggleInput(b: false);
			}, "Waits for an input for your Thirdperson Keybind", null, halfBtn: true);
		}
	}
}
