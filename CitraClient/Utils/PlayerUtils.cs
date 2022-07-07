using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using CitraClient.API.QM;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Management;
using VRC.UI;

namespace CitraClient.Utils
{
	public static class PlayerUtils
	{
		private static readonly Dictionary<string, int> QuestUserDictionary = new Dictionary<string, int>();

		private static int _questCounter = 1;

		private static readonly string[] CitraStaff = new string[3] { "usr_cf45a4de-a115-4e8c-98eb-49d53004df7b", "usr_7d310a5b-4a64-4a3e-8730-add841d24efd", "usr_36854f8e-a1bf-43d7-adb5-ebdf02ea6b1e" };

		public static MethodInfo LoadAvatarMethod { get; set; }

		public static VRCPlayer GetLocalPlayer()
		{
			return VRCPlayer.field_Internal_Static_VRCPlayer_0;
		}

		public static APIUser GetLocalAPIUser()
		{
			return APIUser.CurrentUser;
		}

		public static Player GetPlayer(this PlayerManager instance)
		{
			return (instance == null) ? null : instance.field_Private_Player_0;
		}

		public static VRCPlayer GetVrcPlayer()
		{
			return VRCPlayer.field_Internal_Static_VRCPlayer_0;
		}

		public static Player[] GetAllPlayers(this PlayerManager instance)
		{
			return instance.field_Private_List_1_Player_0.ToArray();
		}

		public static Player GetPlayerWithPlayerID(this PlayerManager instance, int playerID)
		{
			return GetPlayerManager().GetAllPlayers().FirstOrDefault((Player allPlayer) => allPlayer.field_Private_VRCPlayerApi_0.playerId == playerID);
		}

		public static APIUser GetAPIUser(this PlayerManager instance)
		{
			return instance?.GetPlayer().GetAPIUser();
		}

		public static APIUser GetAPIUser(this Player instance)
		{
			return (instance == null) ? null : instance.field_Private_APIUser_0;
		}

		public static APIUser GetAPIUser(this VRCPlayer instance)
		{
			return (instance == null) ? null : instance._player.field_Private_APIUser_0;
		}

		public static APIUser GetAPIUser(this PlayerManager instance, string userID)
		{
			return (from player in instance.GetAllPlayers()
				where player.GetAPIUser().id == userID
				select player.field_Private_APIUser_0).FirstOrDefault();
		}

		public static Player SelectedPlayer()
		{
			return GetPlayerByUserID(APIStuff.GetSelectedUserMenuQM.field_Private_IUser_0.prop_String_0);
		}

		public static Player LocalPlayer()
		{
			return Player.prop_Player_0;
		}

		public static PageUserInfo SelectedSocialPlayer()
		{
			return GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/").GetComponent<PageUserInfo>();
		}

		public static Player GetPlayerByUserID(string userID)
		{
			return (from p in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList()
				where p.GetAPIUser().id == userID
				select p).FirstOrDefault();
		}

		public static PlayerManager GetPlayerManager()
		{
			return PlayerManager.field_Private_Static_PlayerManager_0;
		}

		public static Player GetPlayerWithPlayerID(int playerID)
		{
			return GetPlayerManager().GetAllPlayers().FirstOrDefault((Player allPlayer) => allPlayer.field_Private_VRCPlayerApi_0.playerId == playerID);
		}

		public static bool IsMaster(this Player player)
		{
			return player.prop_VRCPlayerApi_0.isMaster;
		}

		public static int GetPlayerFPS(VRCPlayer player)
		{
			if (player._playerNet.prop_Byte_0 == 0)
			{
				return 0;
			}
			return (int)(1000f / (float)(int)player._playerNet.prop_Byte_0);
		}

		public static int GetPlayerPing(VRCPlayer player)
		{
			return player._playerNet.field_Private_Int16_0;
		}

		public static int GetSentPingAmount(VRCPlayer player)
		{
			return player._playerNet.field_Private_Byte_1;
		}

		public static int GetSentFPSAmount(VRCPlayer player)
		{
			return player._playerNet.field_Private_Byte_0;
		}

		public static string GetPlayerStability(int updatesSinceEvent)
		{
			if (updatesSinceEvent < 150 && updatesSinceEvent > 45)
			{
				return "<color=#f29500>Lagging</color>";
			}
			if (updatesSinceEvent > 100)
			{
				return "<color=red>Crashed</color>";
			}
			return "<color=green>Stable</color>";
		}

		public static string GetFPSColored(VRCPlayer player)
		{
			return $"<color={GeneralUtils.GetFPSColor(GetPlayerFPS(player))}>{GetPlayerFPS(player)}</color>";
		}

		public static string GetPingColored(VRCPlayer player)
		{
			return $"<color={GeneralUtils.GetPingColor(GetPlayerPing(player))}>{GetPlayerPing(player)}</color>";
		}

		public static string GetPlatform(Player player)
		{
			string result = null;
			if (player == null)
			{
				return string.Empty;
			}
			if (player.IsPlayerQuest())
			{
				result = "<color=green>Q</color>";
			}
			if (player.IsInVR())
			{
				result = "<color=blue>VR</color>";
			}
			if (!player.IsInVR() && !player.IsPlayerQuest())
			{
				result = "<color=red>PC</color>";
			}
			return result;
		}

		public static string GetPlayerRank(APIUser instance)
		{
			if (instance.hasModerationPowers || instance.tags.Contains("admin_moderator"))
			{
				return "Moderator";
			}
			if (instance.hasSuperPowers || instance.tags.Contains("admin_"))
			{
				return "Admin";
			}
			if (instance.hasVeteranTrustLevel)
			{
				return "Trusted";
			}
			if (instance.hasTrustedTrustLevel)
			{
				return "Known";
			}
			if (instance.hasKnownTrustLevel)
			{
				return "User";
			}
			if (instance.hasBasicTrustLevel || instance.isNewUser)
			{
				return "New User";
			}
			if (instance.hasNegativeTrustLevel)
			{
				return "Visitor";
			}
			return instance.hasVeryNegativeTrustLevel ? "Nuiscance" : "Visitor";
		}

		public static Color GetRankColor(APIUser player)
		{
			return GetPlayerRank(player) switch
			{
				"Friend" => GeneralUtils.HexToColor("#f2ff00"), 
				"Moderator" => Color.blue, 
				"Admin" => Color.blue, 
				"Legend" => GeneralUtils.HexToColor("#4800ff"), 
				"Veteran" => GeneralUtils.HexToColor("##c3ff00"), 
				"Trusted" => GeneralUtils.HexToColor("#9500ff"), 
				"Known" => GeneralUtils.HexToColor("#ff0800"), 
				"User" => GeneralUtils.HexToColor("#00ff55"), 
				"New User" => GeneralUtils.HexToColor("#00fff2"), 
				"Visitor" => Color.gray, 
				"Nuiscance" => Color.black, 
				_ => Color.gray, 
			};
		}

		public static string GetRankColorHex(APIUser player)
		{
			return GetPlayerRank(player) switch
			{
				"Friend" => "#f2ff00", 
				"Moderator" => "#0000FF", 
				"Admin" => "#0000FF", 
				"Veteran" => "#FFFF00", 
				"Trusted" => "#9500ff", 
				"Known" => "#FFA500", 
				"User" => "#00ff55", 
				"New User" => "#00fff2", 
				"Visitor" => "#808080", 
				"Nuiscance" => "#000000", 
				_ => "#808080", 
			};
		}

		public static Color GetTrustColor(this Player player)
		{
			return VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
		}

		public static void ToggleInput(bool b)
		{
			if (!(GetLocalPlayer() != null))
			{
				return;
			}
			if (b)
			{
				GeneralUtils.Delay(0.25f, delegate
				{
					GetLocalPlayer().GetComponent<GamelikeInputController>().enabled = b;
				});
			}
			else
			{
				GetLocalPlayer().GetComponent<GamelikeInputController>().enabled = b;
			}
		}

		public static List<Player> GetAllPlayersToList()
		{
			return (PlayerManager.field_Private_Static_PlayerManager_0 == null) ? null : PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0;
		}

		public static void ReloadAvatar(VRCPlayer player)
		{
			LoadAvatarMethod.Invoke(player, new object[1] { true });
		}

		public static void ReloadAllAvatars()
		{
			List<Player>.Enumerator enumerator = GetAllPlayersToList().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player current = enumerator.Current;
				if (current != null)
				{
					LoadAvatarMethod.Invoke(current, new object[1] { true });
				}
			}
		}

		public static float GetFrames(this Player player)
		{
			return (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)(int)player._playerNet.prop_Byte_0) : (-1f);
		}

		public static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer)
		{
			return vrcPlayer.prop_ApiAvatar_0;
		}

		public static short GetPing(this Player player)
		{
			return player._playerNet.field_Private_Int16_0;
		}

		public static bool IsPlayerQuest(this Player instance)
		{
			return instance.prop_APIUser_0.IsOnMobile;
		}

		public static bool IsInVR(this Player player)
		{
			return player.field_Private_VRCPlayerApi_0.IsUserInVR();
		}

		public static bool ClientDetect(this Player player)
		{
			return player.GetFrames() > 90f && player.GetFrames() < 1f && player.GetPing() > 665 && player.GetPing() < 0 && player.IsPlayerQuest() && player.IsInVR();
		}

		public static bool GetIsVRChatStaff(this Player instance)
		{
			return instance.prop_APIUser_0.developerType == APIUser.DeveloperType.Moderator || instance.prop_APIUser_0.hasModerationPowers || instance.prop_APIUser_0.hasSuperPowers;
		}

		public static void CheckForQuest(Player player, [Optional] bool state)
		{
			if (state && player.IsPlayerQuest())
			{
				QuestUserDictionary.Add(player.GetAPIUser().id, ++_questCounter);
				player.prop_USpeaker_0.field_Private_SimpleAudioGain_0.field_Public_Single_0 = 0f;
			}
			else if (QuestUserDictionary.ContainsKey(player.GetAPIUser().id))
			{
				QuestUserDictionary.Remove(player.GetAPIUser().id);
				player.prop_USpeaker_0.field_Private_SimpleAudioGain_0.field_Public_Single_0 = 1f;
			}
		}

		public static void CheckForQuestUserOnLoad()
		{
			List<Player>.Enumerator enumerator = GetAllPlayersToList().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player current = enumerator.Current;
				CheckForQuest(current, state: false);
			}
		}

		public static bool IsBlockedEitherWay(string userId)
		{
			ModerationManager moderationManager = ModerationManager.prop_ModerationManager_0;
			if (moderationManager == null)
			{
				return false;
			}
			if (APIUser.CurrentUser.id == userId)
			{
				return false;
			}
			Dictionary<string, List<ApiPlayerModeration>> field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_ = ModerationManager.prop_ModerationManager_0.field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0;
			if (!field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_.ContainsKey(userId))
			{
				return false;
			}
			List<ApiPlayerModeration>.Enumerator enumerator = field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_[userId].GetEnumerator();
			while (enumerator.MoveNext())
			{
				ApiPlayerModeration current = enumerator.Current;
				if (current != null && current.moderationType == ApiPlayerModeration.ModerationType.Block)
				{
					return true;
				}
			}
			return false;
		}

		public static Quaternion GetPlayerRotation()
		{
			return GetLocalPlayer().transform.rotation;
		}

		public static Vector3 GetPlayerPosition()
		{
			return GetLocalPlayer().transform.position;
		}

		public static void SendToLocation(Vector3 pos, Quaternion rot)
		{
			GetLocalPlayer().transform.position = pos;
			GetLocalPlayer().transform.rotation = rot;
		}

		public static void LogInfoOnUser(Player player)
		{
			APIUser aPIUser = ((player != null) ? player.prop_APIUser_0 : null);
			if (aPIUser != null)
			{
				ConsoleUtils.OnLogInfo("\n" + MiscUtils.MultiCharacterString("=", 50) + "\nDisplay Name: " + aPIUser.displayName + "\nUser ID:" + aPIUser.id + "\n" + $"Is Moderator: {aPIUser.hasModerationPowers}\n" + $"Is Troll: {aPIUser.hasVeryNegativeTrustLevel || aPIUser.hasNegativeTrustLevel}\n" + $"Has VRC+: {aPIUser.hasVIPAccess}\n" + "Trust Level: " + GetPlayerRank(aPIUser) + "\nBio: " + aPIUser.bio + "\n" + $"Bio Links: {aPIUser.bioLinks}" + "Current Avatar: " + aPIUser.avatarId + "\nCurrent Avatar Asset URL: " + aPIUser.currentAvatarAssetUrl + "\nCurrent Avatar Image URL: " + aPIUser.currentAvatarImageUrl + "Current Avatar Thumbnail Image URL: " + aPIUser.currentAvatarThumbnailImageUrl + "\nDate Joined: " + aPIUser.date_joined + "\n" + $"Developer Type: {aPIUser.developerType}\n" + "Last Platform: " + aPIUser._last_platform + "\n" + MiscUtils.MultiCharacterString("=", 50));
			}
		}

		public static bool IsCitraStaff()
		{
			bool result = false;
			string id = GetLocalAPIUser().id;
			for (int i = 0; i < CitraStaff.Length; i++)
			{
				string objA = CitraStaff[i];
				if (object.Equals(objA, id))
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}
}
