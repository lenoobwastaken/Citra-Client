using System.Collections.Generic;
using CitraClient.Config;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using CitraClient.Utils.PhotonUtils;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using VRC;
using VRC.SDKBase;

namespace CitraClient.GUI.QM.Console
{
	public class CenterConsole : ModuleBase
	{
		public enum LogsType
		{
			AC,
			INFO,
			VOICE,
			AVATAR,
			BLOCK,
			JOIN,
			LEFT,
			WARN,
			KICK,
			LOGOUT,
			BAN,
			FRIEND,
			EVENT,
			RPC,
			EMPTY,
			SERVER,
			CRASH,
			MODERATION,
			API,
			BOT,
			PORTAL,
			ROOM,
			CITRA,
			EXPLOIT,
			DEBUG,
			MEDIA,
			UDON,
			PLAYER
		}

		public static Text LogText;

		public static List<TextMeshProUGUI> AllLogsText = new List<TextMeshProUGUI>();

		public static int YPos;

		public static void Log(LogsType type, string text)
		{
			try
			{
				AllLogsText[16].text = AllLogsText[15].text;
				AllLogsText[15].text = AllLogsText[14].text;
				AllLogsText[14].text = AllLogsText[13].text;
				AllLogsText[13].text = AllLogsText[12].text;
				AllLogsText[12].text = AllLogsText[11].text;
				AllLogsText[11].text = AllLogsText[10].text;
				AllLogsText[10].text = AllLogsText[9].text;
				AllLogsText[9].text = AllLogsText[8].text;
				AllLogsText[8].text = AllLogsText[7].text;
				AllLogsText[7].text = AllLogsText[6].text;
				AllLogsText[6].text = AllLogsText[5].text;
				AllLogsText[5].text = AllLogsText[4].text;
				AllLogsText[4].text = AllLogsText[3].text;
				AllLogsText[3].text = AllLogsText[2].text;
				AllLogsText[2].text = AllLogsText[1].text;
				AllLogsText[1].text = AllLogsText[0].text;
				switch (type)
				{
				case LogsType.ROOM:
					AllLogsText[0].text = "<color=blue>[Room]:</color>  " + text;
					break;
				case LogsType.AC:
					AllLogsText[0].text = "<color=#00f7ff>[AntiCrash]:</color>  " + text;
					break;
				case LogsType.SERVER:
					AllLogsText[0].text = "<color=#00f7ff>[Server]:</color>  " + text;
					break;
				case LogsType.API:
					AllLogsText[0].text = "<color=#00ffe5>[API]:</color>  " + text;
					break;
				case LogsType.BOT:
					AllLogsText[0].text = "<color=#5900ff>[Bot]:</color>  " + text;
					break;
				case LogsType.EMPTY:
					AllLogsText[0].text = text ?? "";
					break;
				case LogsType.RPC:
					AllLogsText[0].text = "<color=#d900ff>[RPC]:</color>  " + text;
					break;
				case LogsType.EVENT:
					AllLogsText[0].text = "<color=#6f00ff>[Event]:</color>  " + text;
					break;
				case LogsType.INFO:
					AllLogsText[0].text = "<color=#949494>[Info]:</color>  " + text;
					break;
				case LogsType.AVATAR:
					AllLogsText[0].text = "<color=#00FF62>[Avatar]:</color>  " + text;
					break;
				case LogsType.JOIN:
					AllLogsText[0].text = "<color=#1aff00>[+]:</color>  " + text;
					break;
				case LogsType.LEFT:
					AllLogsText[0].text = "<color=#ff0000>[-]:</color>  " + text;
					break;
				case LogsType.WARN:
					AllLogsText[0].text = "<color=#ffea00>[Warn]:</color>  " + text;
					break;
				case LogsType.CRASH:
					AllLogsText[0].text = "<color=#ff00e6>[Crash]:</color>  " + text;
					break;
				case LogsType.MODERATION:
					AllLogsText[0].text = "<color=#ffea00>[Moderation]:</color>  " + text;
					break;
				case LogsType.PORTAL:
					AllLogsText[0].text = "<color=#0026ff>[Portal]:</color>  " + text;
					break;
				case LogsType.CITRA:
					AllLogsText[0].text = "<color=#ffe600>[Citra]:</color> " + text;
					break;
				case LogsType.EXPLOIT:
					AllLogsText[0].text = "<color=#ffe600>[Exploit]:</color> " + text;
					break;
				case LogsType.DEBUG:
					AllLogsText[0].text = "<color=#ffa500>[Debug]:</color> " + text;
					break;
				case LogsType.MEDIA:
					AllLogsText[0].text = "<color=#5832a8>[Media]:</color> " + text;
					break;
				case LogsType.UDON:
					AllLogsText[0].text = "<color=#ffa5008>[Udon]:</color> " + text;
					break;
				case LogsType.PLAYER:
					AllLogsText[0].text = "<color=#6f00ff>[Players]:</color> " + text;
					break;
				case LogsType.VOICE:
				case LogsType.BLOCK:
				case LogsType.KICK:
				case LogsType.LOGOUT:
				case LogsType.BAN:
				case LogsType.FRIEND:
					break;
				}
			}
			catch
			{
			}
		}

		public override void PhotonJoin(Photon.Realtime.Player photonPlayer)
		{
			string displayName = photonPlayer.GetDisplayName();
			if (displayName != Networking.LocalPlayer.displayName)
			{
				Log(LogsType.JOIN, "Player " + displayName + " joined.");
			}
			if (Configuration.GetConfig().muteQuestUsers)
			{
				PlayerUtils.CheckForQuestUserOnLoad();
			}
		}

		public override void PhotonLeave(Photon.Realtime.Player photonPlayer)
		{
			string displayName = photonPlayer.GetDisplayName();
			if (displayName != Networking.LocalPlayer.displayName)
			{
				Log(LogsType.LEFT, "Player " + displayName + " left.");
			}
		}

		public override void VRCJoin(VRC.Player player)
		{
		}
	}
}
