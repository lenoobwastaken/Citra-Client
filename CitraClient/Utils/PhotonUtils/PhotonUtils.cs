using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Il2CppSystem;
using Il2CppSystem.Collections;
using Il2CppSystem.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using VRC;

namespace CitraClient.Utils.PhotonUtils
{
	public static class PhotonUtils
	{
		public static bool OpRaiseEvent(byte Code, Object Data, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			return PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(Code, Data, raiseEventOptions, sendOptions);
		}

		public static string GetDisplayName(this Photon.Realtime.Player player)
		{
			if (player.prop_Hashtable_0.ContainsKey("user") && player.GetHashtable()["user"] is System.Collections.Generic.Dictionary<string, object> dictionary)
			{
				return (string)dictionary["displayName"];
			}
			return "No DisplayName";
		}

		public static int GetPhotonID(this Photon.Realtime.Player player)
		{
			return player.field_Private_Int32_0;
		}

		public static VRC.Player GetPlayer(this Photon.Realtime.Player player)
		{
			return player.field_Public_Player_0;
		}

		public static System.Collections.Hashtable GetHashtable(this Photon.Realtime.Player player)
		{
			return Serialization.FromIL2CPPToManaged<System.Collections.Hashtable>(player.prop_Hashtable_0);
		}

		public static Il2CppSystem.Collections.Hashtable GetRawHashtable(this Photon.Realtime.Player player)
		{
			return player.prop_Hashtable_0;
		}

		public static LoadBalancingClient GetPhotonInstance()
		{
			return PhotonNetwork.field_Public_Static_LoadBalancingClient_0;
		}

		public static System.Collections.Generic.List<Photon.Realtime.Player> GetAllPhotonPlayers(this LoadBalancingClient Instance)
		{
			if (Instance == null)
			{
				return null;
			}
			System.Collections.Generic.List<Photon.Realtime.Player> list = new System.Collections.Generic.List<Photon.Realtime.Player>();
			Il2CppSystem.Collections.Generic.Dictionary<int, Photon.Realtime.Player>.Enumerator enumerator = Instance.prop_Player_0.prop_Room_0.field_Private_Dictionary_2_Int32_Player_0.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Il2CppSystem.Collections.Generic.KeyValuePair<int, Photon.Realtime.Player> current = enumerator.Current;
				list.Add(current.Value);
			}
			return list;
		}

		public static bool IsVR(this Photon.Realtime.Player player)
		{
			if (!player.prop_Hashtable_0.ContainsKey("inVRMode") || !(player.GetHashtable()["inVRMode"] is bool result))
			{
				return false;
			}
			return result;
		}

		public static Photon.Realtime.Player GetPhotonPlayer(this LoadBalancingClient Instance, int photonID)
		{
			foreach (Photon.Realtime.Player allPhotonPlayer in Instance.GetAllPhotonPlayers())
			{
				if (allPhotonPlayer.field_Private_Int32_0 == photonID)
				{
					return allPhotonPlayer;
				}
			}
			return null;
		}

		public static string GetUserID(this Photon.Realtime.Player player)
		{
			if (player.prop_Hashtable_0.ContainsKey("user") && player.GetHashtable()["user"] is System.Collections.Generic.Dictionary<string, object> dictionary)
			{
				return (string)dictionary["id"];
			}
			return "No ID";
		}
	}
}
