using System.Collections;
using System.Collections.Generic;
using CitraClient.Utils;
using CitraClient.Utils.PhotonUtils;
using Photon.Realtime;
using UnityEngine;
using VRC;

namespace CitraClient.GUI.QM.Playerlist
{
	public class PlayerlistManager
	{
		public static Dictionary<int, PhotonPlayerObject> players = new Dictionary<int, PhotonPlayerObject>();

		public static int updatesSinceEvent = 0;

		public static IEnumerator LoopPlayerlist()
		{
			while (true)
			{
				if (PlayerUtils.GetLocalPlayer() == null)
				{
					yield return null;
					continue;
				}
				while (Playerlist.playerList == null)
				{
					yield return null;
				}
				string final = "";
				int count = 0;
				foreach (Photon.Realtime.Player plr in PhotonUtils.GetPhotonInstance().GetAllPhotonPlayers())
				{
					while (plr == null)
					{
						yield return null;
					}
					while (plr.GetPlayer() == null)
					{
						yield return null;
					}
					while (plr.GetPlayer().field_Private_APIUser_0 == null)
					{
						yield return null;
					}
					_ = PlayerUtils.GetPlayerStability(updatesSinceEvent) ?? "";
					int currentId = plr.GetPhotonID();
					VRC.Player currentPlayer = plr.GetPlayer();
					try
					{
						final += $"[{currentId}] <color={PlayerUtils.GetRankColorHex(currentPlayer.field_Private_APIUser_0)}>{plr.GetDisplayName()}</color> [{PlayerUtils.GetPlatform(currentPlayer)}] [P: {PlayerUtils.GetPingColored(currentPlayer.prop_VRCPlayer_0)}] [F: {PlayerUtils.GetFPSColored(currentPlayer.prop_VRCPlayer_0)}]";
					}
					catch
					{
					}
					finally
					{
						final += "\n";
						count++;
						if (count != 23)
						{
						}
					}
				}
				Playerlist.playersComponent.text = $"Players : {count}";
				Playerlist.listComponent.text = final;
				yield return new WaitForSeconds(3f);
			}
		}
	}
}
