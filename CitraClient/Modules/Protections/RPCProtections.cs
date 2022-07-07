using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CitraClient.Config;
using CitraClient.Utils;
using CitraClient.Utils.PhotonUtils;
using ExitGames.Client.Photon;
using Il2CppSystem;
using Photon.Pun;
using Photon.Realtime;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.Modules.Protections
{
	public static class RPCProtections
	{
		public static List<int> blockedSenders = new List<int>();

		public static Dictionary<int, int> ratelimitedSenders = new Dictionary<int, int>();

		public static Dictionary<int, int> ratelimitHistory = new Dictionary<int, int>();

		public static Dictionary<string, int> ratelimits = new Dictionary<string, int>
		{
			{ "SpawnEmojiRPC", 25 },
			{ "EnableMesh", 25 }
		};

		private static readonly List<string> rpcStrings = new List<string>
		{
			"Generic", "ReceiveVoiceStatsSyncRPC", "InformOfBadConnection", "initUSpeakSenderRPC", "InteractWithStationRPC", "SpawnEmojiRPC", "SanityCheck", "PlayEmoteRPC", "TeleportRPC", "CancelRPC",
			"SetTimerRPC", "_DestroyObject", "_InstantiateObject", "_SendOnSpawn", "ConfigurePortal", "UdonSyncRunProgramAsRPC", "ChangeVisibility", "PhotoCapture", "TimerBloop", "ReloadAvatarNetworkedRPC",
			"InternalApplyOverrideRPC", "AddURL", "Play", "Pause", "SendVoiceSetupToPlayerRPC", "SendStrokeRPC", "SyncWorldInstanceIdRPC", "PlayEffect"
		};

		public static int defaultLimit = 400;

		public static bool CheckRPC(EventData eventData)
		{
			if (!Configuration.GetConfig().rpcProtection)
			{
				return true;
			}
			if (blockedSenders.Contains(eventData.Sender))
			{
				return false;
			}
			if (ratelimitHistory.ContainsKey(eventData.Sender) && ratelimitHistory[eventData.Sender] > 4)
			{
				return false;
			}
			try
			{
				Photon.Realtime.Player photonPlayer = PhotonNetwork.field_Public_Static_LoadBalancingClient_0.GetPhotonPlayer(eventData.Sender);
				Il2CppSystem.Object param_;
				try
				{
					BinarySerializer.Method_Public_Static_Boolean_ArrayOf_Byte_byref_Object_0(Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer).ToArray(), out param_);
				}
				catch (Il2CppException)
				{
					return false;
				}
				if (param_ == null)
				{
					return false;
				}
				VRC_EventLog.EventLogEntry eventLogEntry = param_.TryCast<VRC_EventLog.EventLogEntry>();
				VRC_EventHandler.VrcEvent vrcEvent = eventLogEntry.prop_VrcEvent_0;
				if (!rpcStrings.Contains(vrcEvent.ParameterString))
				{
					blockedSenders.Add(eventData.Sender);
					return false;
				}
				if (ratelimitedSenders.ContainsKey(eventData.Sender) && ratelimitedSenders[eventData.Sender] >= GetLimit(vrcEvent.ParameterString))
				{
					if (ratelimitHistory.ContainsKey(eventData.Sender))
					{
						ratelimitHistory[eventData.Sender]++;
					}
					else
					{
						ratelimitHistory.Add(eventData.Sender, 1);
					}
					return false;
				}
				if (vrcEvent.EventType > VRC_EventHandler.VrcEventType.CallUdonMethod)
				{
					blockedSenders.Add(eventData.Sender);
					return false;
				}
				if (!ratelimitedSenders.ContainsKey(eventData.Sender))
				{
					ratelimitedSenders.Add(eventData.Sender, 1);
				}
				else
				{
					ratelimitedSenders[eventData.Sender]++;
				}
			}
			catch (System.Exception ex2)
			{
				ConsoleUtils.OnLogError("Error in RPC Protection: \n" + ex2.Message);
			}
			return true;
		}

		public static IEnumerator ClearRatelimit()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				ratelimitedSenders.Clear();
			}
		}

		public static int GetLimit(string paramString)
		{
			int value;
			return ratelimits.TryGetValue(paramString, out value) ? value : defaultLimit;
		}
	}
}
