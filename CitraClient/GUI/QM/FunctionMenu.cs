using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Playerlist;
using CitraClient.GUI.QM.Submenus;
using CitraClient.GUI.QM.Submenus.Sliders;
using CitraClient.GUI.QMChanges;
using CitraClient.Modules.Misc;
using CitraClient.Modules.Misc.TitleChanger;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;

namespace CitraClient.GUI.QM
{
	public class FunctionMenu
	{
		public static QMNestedButton functionMenu;

		public static QMSingleButton LogPickupsButton;

		private static float _coolDownTime;

		public static QMToggleButton muteQuestToggleButton;

		public static void InitFunctionMenu()
		{
			functionMenu = new QMNestedButton(MainMenu.MainPage, "Functions", 3f, 2f, "Main functions of <color=#CE389C>Citra:</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Functions</color>");
			new QMSingleButton(functionMenu, 4f, 3f, "Force Quit", Process.GetCurrentProcess().Kill, "Quits the game", null, halfBtn: true);
			new QMSingleButton(functionMenu, 3f, 3.5f, "Restart", ApplicationUtils.RestartGame, "Restarts the game", null, halfBtn: true);
			new QMSingleButton(functionMenu, 1f, 0f, "Random VRC Cat", delegate
			{
				ImageUtils.SetRandomCatSprite();
			}, "Changes the VRC+ Cat to a random one.");
			new QMSingleButton(functionMenu, 2f, 0f, "Clear Center\nConsole", delegate
			{
				QMConsole.ClearConsole();
			}, "Clears all text in the center console");
			new QMSingleButton(functionMenu, 3f, 0f, "Clear Melon\nConsole", delegate
			{
				ConsoleUtils.ClearConsole();
			}, "Clears all text in the ML Console.");
			new QMSingleButton(functionMenu, 4f, 0f, "Change Window Title", delegate
			{
				VRCUiPopupManager.prop_VRCUiPopupManager_0.ShowInputPopupWithCancel("Please Enter Title Text", string.Empty, InputField.InputType.Standard, useNumericKeypad: false, "Change", delegate(string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
				{
					TitleHandler.ChangeWindowTitle(s);
				}, Popups.HideCurrentPopup);
			}, "Change the Title of the VRChat window.");
			new QMSingleButton(functionMenu, 3f, 3f, "Restart\nRejoin", delegate
			{
				ApplicationUtils.RestartRejoin();
			}, "Restart VRChat and join instance you were in at the time of restarting.", null, halfBtn: true);
			new QMToggleButton(functionMenu, 1f, 1f, "VRChat Priority", delegate
			{
				ApplicationUtils.SetVRChatPriority(0);
				Configuration.GetConfig().isHighPriority = true;
				Configuration.Save();
			}, delegate
			{
				ApplicationUtils.SetVRChatPriority(1);
				Configuration.GetConfig().isHighPriority = false;
				Configuration.Save();
			}, "Toggles the Priority for VRChat. Set to normal for default.", Configuration.GetConfig().isHighPriority);
			new QMToggleButton(functionMenu, 2f, 1f, "Uncap Frames", delegate
			{
				Configuration.GetConfig().isMaxFrames = true;
				Configuration.Save();
				ApplicationUtils.MaxFrames();
			}, delegate
			{
				Configuration.GetConfig().isMaxFrames = false;
				Configuration.Save();
				ApplicationUtils.MaxFrames();
			}, "Allows you to uncap the 90 frames per second limit.", Configuration.GetConfig().isMaxFrames);
			LogPickupsButton = new QMSingleButton(functionMenu, 3f, 1f, "Log Pickup Owner", delegate
			{
				if (Time.time > _coolDownTime)
				{
					_coolDownTime = Time.time + 7f;
				}
				else
				{
					float num = _coolDownTime - Time.time;
					MezLogger.HudWarn($"Check the Melon Console. Cooldown: {Math.Floor(num)}", 5f);
				}
				MiscUtils.CountDownButton(LogPickupsButton, 7, "Log Pickup Owner").Start();
				PickupUtils.GetAllPickupsOwnerUsername().Start();
			}, "Logs the owner of each pickup to the console.");
			new QMSingleButton(functionMenu, 4f, 1f, "Russian\nRoulette", delegate
			{
				RussianRoulette.RouletteStart();
			}, "Starts a game of russian roulette lmfao..");
			new QMSingleButton(functionMenu, 1f, 2f, "Save Friends\nTo File", delegate
			{
				System.Collections.Generic.List<string> contents = APIUser.CurrentUser.friendIDs.ToArray().ToList();
				File.WriteAllLines("Citra\\FriendsIDBackups.txt", contents);
				ConsoleUtils.OnLogInfo("Created friends text file: Citra\\FriendsIDBackups.txt");
			}, "Saves your entire friends list to a file. Credits go to Lime.");
			new QMSingleButton(functionMenu, 2f, 2f, "Clear Ram", delegate
			{
				ApplicationUtils.RamClear();
			}, "Attempts to clear your ram. Potentially improving performance.");
			muteQuestToggleButton = new QMToggleButton(functionMenu, 3f, 2f, "Mute Quest Users", delegate
			{
				Configuration.GetConfig().muteQuestUsers = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().muteQuestUsers = false;
				Configuration.Save();
			}, "Automatically mutes Quest Users when they load in.", Configuration.GetConfig().muteQuestUsers);
			muteQuestToggleButton = new QMToggleButton(functionMenu, 4f, 2f, "Toggle PlayerList", delegate
			{
				Configuration.GetConfig().togglePlayerList = true;
				Configuration.Save();
				CitraClient.GUI.QM.Playerlist.Playerlist.TogglePlayerList();
			}, delegate
			{
				Configuration.GetConfig().togglePlayerList = false;
				Configuration.Save();
				CitraClient.GUI.QM.Playerlist.Playerlist.TogglePlayerList();
			}, "Toggles the Player List.", Configuration.GetConfig().muteQuestUsers);
			FunctionSliders.Init();
			MovementMenu.InitMovementMenu();
			SafetyMenu.InitSafetyMenu();
		}
	}
}
