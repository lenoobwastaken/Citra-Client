using System;
using System.Reflection;
using CitraClient.GUI.QM.Playerlist;
using CitraClient.Modules.Base;
using CitraClient.Modules.Protections;
using CitraClient.Utils.PhotonUtils;
using ExitGames.Client.Photon;
using MelonLoader;
using Photon.Realtime;
using VRC;

namespace CitraClient.Utils.Harmony
{
	public class NativePatches
	{
		private delegate IntPtr OnEvent(IntPtr _instance, IntPtr eventData, IntPtr _nativeMethodInfoPtr);

		private delegate IntPtr OnPhotonJoin(IntPtr _instance, IntPtr user, IntPtr _nativeMethodInfoPtr);

		private delegate IntPtr OnPlayerJoin(IntPtr _instance, IntPtr user, IntPtr _nativeMethodInfoPtr);

		private delegate IntPtr OnPhotonLeave(IntPtr _instance, IntPtr user, IntPtr _nativeMethodInfoPtr);

		private delegate IntPtr OnPlayerLeave(IntPtr _instance, IntPtr user, IntPtr _nativeMethodInfoPtr);

		private static OnEvent _onEvent;

		private static OnPhotonJoin _onPhotonJoin;

		private static OnPlayerJoin _onPlayerJoin;

		private static OnPhotonLeave _onPhotonLeave;

		private static OnPlayerLeave _onPlayerLeave;

		public static void HookAll()
		{
			try
			{
				MethodInfo method = typeof(LoadBalancingClient).GetMethod("OnEvent");
				_onEvent = Hook.NativeHook<OnEvent>(method, typeof(NativePatches).GetMethod("OnPhotonEvent", BindingFlags.Static | BindingFlags.NonPublic));
				MethodInfo method2 = typeof(NetworkManager).GetMethod("Method_Public_Virtual_Final_New_Void_Player_0");
				_onPhotonJoin = Hook.NativeHook<OnPhotonJoin>(method2, typeof(NativePatches).GetMethod("OnPhotonPlayerJoin", BindingFlags.Static | BindingFlags.NonPublic));
				MethodInfo method3 = typeof(NetworkManager).GetMethod("Method_Public_Void_Player_0");
				_onPlayerJoin = Hook.NativeHook<OnPlayerJoin>(method3, typeof(NativePatches).GetMethod("OnVRCPlayerJoin", BindingFlags.Static | BindingFlags.NonPublic));
				MethodInfo method4 = typeof(NetworkManager).GetMethod("Method_Public_Virtual_Final_New_Void_Player_1");
				_onPhotonLeave = Hook.NativeHook<OnPhotonLeave>(method4, typeof(NativePatches).GetMethod("OnPhotonPlayerLeave", BindingFlags.Static | BindingFlags.NonPublic));
				MethodInfo method5 = typeof(NetworkManager).GetMethod("Method_Public_Void_Player_1");
				_onPlayerLeave = Hook.NativeHook<OnPlayerLeave>(method5, typeof(NativePatches).GetMethod("OnVRCPlayerLeave", BindingFlags.Static | BindingFlags.NonPublic));
			}
			catch (Exception ex)
			{
				ConsoleUtils.OnLogError("Error during hooking:\n" + ex.Message);
			}
			finally
			{
				ConsoleUtils.OnLogSuccess($"Successfully applied {Hook.hooks.Count} native patches.");
			}
		}

		private static IntPtr OnPhotonEvent(IntPtr instance, IntPtr eventData, IntPtr nativeMethodInfoPtr)
		{
			try
			{
				EventData eventData2 = UnhollowerSupport.Il2CppObjectPtrToIl2CppObject<EventData>(eventData);
				ParameterDictionary parameters = eventData2.Parameters;
				switch (eventData2.Code)
				{
				case 1:
					if (!USpeakProtections.CheckUSpeak(eventData2))
					{
						return IntPtr.Zero;
					}
					break;
				case 6:
					if (!RPCProtections.CheckRPC(eventData2))
					{
						return IntPtr.Zero;
					}
					break;
				}
			}
			catch
			{
			}
			return _onEvent(instance, eventData, nativeMethodInfoPtr);
		}

		private static IntPtr OnPhotonPlayerJoin(IntPtr instance, IntPtr user, IntPtr nativeMethodInfoPtr)
		{
			Photon.Realtime.Player player = UnhollowerSupport.Il2CppObjectPtrToIl2CppObject<Photon.Realtime.Player>(user);
			foreach (ModuleBase module in CitraMonoBehaviour.Modules)
			{
				module.PhotonJoin(player);
			}
			int photonID = player.GetPhotonID();
			VRC.Player player2 = player.GetPlayer();
			PhotonPlayerObject value = new PhotonPlayerObject
			{
				actorId = photonID,
				isInstantiated = false,
				player = player2
			};
			PlayerlistManager.players.Add(photonID, value);
			return _onPhotonJoin(instance, user, nativeMethodInfoPtr);
		}

		private static IntPtr OnVRCPlayerJoin(IntPtr instance, IntPtr user, IntPtr nativeMethodInfoPtr)
		{
			try
			{
				VRC.Player player = UnhollowerSupport.Il2CppObjectPtrToIl2CppObject<VRC.Player>(user);
				foreach (ModuleBase module in CitraMonoBehaviour.Modules)
				{
					module.VRCJoin(player);
				}
				if (PlayerlistManager.players.ContainsKey(player.field_Private_VRCPlayerApi_0.playerId))
				{
					PlayerlistManager.players[player.field_Private_VRCPlayerApi_0.playerId].isInstantiated = true;
				}
			}
			catch
			{
			}
			return _onPlayerJoin(instance, user, nativeMethodInfoPtr);
		}

		private static IntPtr OnPhotonPlayerLeave(IntPtr instance, IntPtr user, IntPtr nativeMethodInfoPtr)
		{
			Photon.Realtime.Player player = UnhollowerSupport.Il2CppObjectPtrToIl2CppObject<Photon.Realtime.Player>(user);
			foreach (ModuleBase module in CitraMonoBehaviour.Modules)
			{
				module.PhotonLeave(player);
			}
			return _onPhotonLeave(instance, user, nativeMethodInfoPtr);
		}

		private static IntPtr OnVRCPlayerLeave(IntPtr instance, IntPtr user, IntPtr nativeMethodInfoPtr)
		{
			try
			{
				VRC.Player player = UnhollowerSupport.Il2CppObjectPtrToIl2CppObject<VRC.Player>(user);
				foreach (ModuleBase module in CitraMonoBehaviour.Modules)
				{
					module.VRCLeave(player);
				}
			}
			catch
			{
			}
			return _onPlayerLeave(instance, user, nativeMethodInfoPtr);
		}
	}
}
