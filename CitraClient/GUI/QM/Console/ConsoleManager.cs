using System;
using System.Collections.Generic;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using CitraClient.Utils.PhotonUtils;
using Photon.Realtime;
using TMPro;

namespace CitraClient.GUI.QM.Console
{
	public class ConsoleManager : ModuleBase
	{
		public static List<TextMeshProUGUI> consoleText = new List<TextMeshProUGUI>();

		public static int gameObjectPosition = 0;

		public static string lastText;

		private static readonly bool useTimestamp = true;

		public static void LogText(LogType logType, string logText)
		{
			try
			{
				for (int num = 26; num > 0; num--)
				{
					consoleText[num].text = consoleText[num - 1].text;
				}
				string text = DateTime.Now.ToString("HH:mm:ss");
				switch (logType)
				{
				case LogType.None:
					consoleText[0].text = logText ?? "";
					break;
				case LogType.Join:
					consoleText[0].text = "[<color=green>+</color>] " + logText;
					break;
				case LogType.Leave:
					consoleText[0].text = "[<color=#E4113A>-</color>] " + logText;
					break;
				case LogType.Protection:
					consoleText[0].text = "[<color=#00ffd0>Protection</color>] " + logText;
					break;
				case LogType.AvatarLog:
					consoleText[0].text = (useTimestamp ? ("[<color=#4d4d4d>" + text + "</color>] ") : "") + "[<color=#yellow>Avatar Log</color>] " + logText;
					break;
				}
			}
			catch (Exception ex)
			{
				ConsoleUtils.OnLogError("Exception occured in ConsoleManager:\n " + ex.Message);
			}
		}

		public static void WelcomeLog()
		{
			LogText(LogType.None, "Welcome <color=#c170ff>" + PlayerUtils.GetLocalAPIUser().displayName + "</color>, to <color=#00d1ed>Citra</color>");
			LogText(LogType.None, "");
		}

		public override void PhotonJoin(Player photonPlayer)
		{
			LogText(LogType.Join, "Player " + photonPlayer.GetDisplayName() + " joined.");
			CenterConsole.Log(CenterConsole.LogsType.JOIN, "Player " + photonPlayer.GetDisplayName() + " joined.");
		}

		public override void PhotonLeave(Player photonPlayer)
		{
			LogText(LogType.Leave, "Player " + photonPlayer.GetDisplayName() + " left.");
			CenterConsole.Log(CenterConsole.LogsType.LEFT, "Player " + photonPlayer.GetDisplayName() + " left.");
		}
	}
}
