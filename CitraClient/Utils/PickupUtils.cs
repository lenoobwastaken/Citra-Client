using System.Collections;
using System.Linq;
using CitraClient.Config;
using CitraClient.GUI.QM.Console;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace CitraClient.Utils
{
	public static class PickupUtils
	{
		public static GameObject Plunger { get; set; }

		public static VRC_Pickup[] GetAllPickups { get; set; }

		public static void FindPickupsOnSceneLoad()
		{
			GetAllPickups = Object.FindObjectsOfType<VRC_Pickup>().ToArray();
			Plunger = GameObject.Find("plunger");
		}

		public static void TakeOwnerShipPickup(VRC_Pickup pickup)
		{
			if (!(pickup == null))
			{
				Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
			}
		}

		public static void CheckForOwnerShipPickups(VRC_Pickup pickup)
		{
			if (!(pickup == null) && !Networking.LocalPlayer.IsOwner(pickup.gameObject))
			{
				Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
			}
		}

		public static void TakeOwnerShipAllPickups()
		{
			for (int i = 0; i < GetAllPickups.Length; i++)
			{
				VRC_Pickup pickup = GetAllPickups[i];
				TakeOwnerShipPickup(pickup);
			}
		}

		public static string GetPickupOwnerUsername(VRC_Pickup pickup)
		{
			Player playerWithPlayerID = PlayerUtils.GetPlayerWithPlayerID(pickup.currentPlayer.playerId);
			return playerWithPlayerID.field_Private_VRCPlayerApi_0.IsOwner(pickup.gameObject) ? playerWithPlayerID.field_Private_APIUser_0.displayName : "NO CURRENT OWNER";
		}

		public static IEnumerator GetAllPickupsOwnerUsername()
		{
			List<Player>.Enumerator enumerator = PlayerUtils.GetAllPlayersToList().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player player = enumerator.Current;
				VRC_Pickup[] getAllPickups = GetAllPickups;
				foreach (VRC_Pickup pickup in getAllPickups)
				{
					if (player.field_Private_VRCPlayerApi_0.IsOwner(pickup.gameObject))
					{
						ConsoleUtils.OnLogInfo("Pickup: " + pickup.name + " Current Owner: " + player.prop_APIUser_0.displayName);
						yield return null;
					}
				}
			}
		}

		public static void AntiPickupTheift(VRC_Pickup pickup, bool state)
		{
			if (state && pickup != null)
			{
				pickup.DisallowTheft = true;
			}
		}

		public static IEnumerator LogWhenPickedUp()
		{
			int count = 0;
			while (RuntimeConfig.isLoggingPickups && RuntimeConfig.isLoggingPickups && WorldUtils.IsInWorld())
			{
				VRC_Pickup[] getAllPickups = GetAllPickups;
				foreach (VRC_Pickup pickup in getAllPickups)
				{
					if (!(pickup == null) && pickup.IsHeld)
					{
						Player player = PlayerUtils.GetPlayerWithPlayerID(pickup.currentPlayer.playerId);
						ConsoleUtils.OnLogInfo("Player " + player.field_Private_VRCPlayerApi_0.displayName + " picked up \"" + pickup.name + "\"");
						CenterConsole.Log(CenterConsole.LogsType.PLAYER, "Player " + player.field_Private_VRCPlayerApi_0.displayName + " picked up \"" + pickup.name + "\"");
						count++;
					}
				}
				yield return null;
			}
		}
	}
}
