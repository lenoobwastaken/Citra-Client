using CitraClient.Config;
using CitraClient.Modules.Exploits;
using CitraClient.Utils;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;

namespace CitraClient.GUI.GUITabs
{
	public static class PlayersTab
	{
		private static int _yOffset;

		private static Player _targetPlayer;

		public static void PlayerTab(Player player)
		{
			APIUser aPIUser = ((player != null) ? player.prop_APIUser_0 : null);
			_yOffset = 70;
			if (aPIUser != null)
			{
				UnityEngine.GUI.Label(new Rect(200f, _yOffset, 500f, 20f), "<b>Selected player:</b> <b><color=" + PlayerUtils.GetRankColorHex(aPIUser) + ">" + aPIUser.displayName + "</color></b>");
			}
			_yOffset += 20;
			if (!(player == null))
			{
				UnityEngine.GUI.Label(new Rect(200f, _yOffset, 500f, 20f), "<b>Position:</b> " + player.transform.position.ToString());
				_yOffset += 15;
				UnityEngine.GUI.Label(new Rect(200f, _yOffset, 500f, 20f), "<b>Rotation:</b> " + player.transform.rotation.ToString());
				_yOffset += 30;
				if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b><color=red>[</color>Back<color=red>]</color></b>") && UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), RuntimeConfig.isPlungerHat ? "<b>Plunger Hat ON</b>" : "<b>Plunger Hat OFF</b>"))
				{
					RuntimeConfig.isPlungerHat = !RuntimeConfig.isPlungerHat;
					PickupExploits.PlungerHat(player).Start();
				}
				_yOffset += 30;
				if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b><color=red>[</color>Back<color=red>]</color></b>"))
				{
					_targetPlayer = null;
				}
				_yOffset += 30;
			}
		}

		public static void PlayerTab()
		{
			if (_targetPlayer == null)
			{
				_yOffset = 100;
				List<Player> allPlayersToList = PlayerUtils.GetAllPlayersToList();
				List<Player>.Enumerator enumerator = allPlayersToList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player current = enumerator.Current;
					APIUser aPIUser = ((current != null) ? current.prop_APIUser_0 : null);
					if (!(current == null))
					{
						if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 300f, 20f), "<b><color=" + PlayerUtils.GetRankColorHex(aPIUser) + ">" + aPIUser?.displayName + "</color></b>"))
						{
							_targetPlayer = current;
						}
						_yOffset += 20;
					}
				}
			}
			else
			{
				PlayerTab(_targetPlayer);
			}
		}
	}
}
