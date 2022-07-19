using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using CitraClient.Config;
using CitraClient.GUI.QM.Playerlist;
using CitraClient.Modules.Base;
using CitraClient.Modules.Protections;
using CitraClient.Utils;
using CitraClient.Utils.AssetBundleManager;
using CitraClient.Utils.Harmony;
using CitraClient.Utils.Managers;
using CitraClient.Utils.PhotonUtils;
using CitraClient.Utils.UI;
using MelonLoader;
using Photon.Realtime;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC;
using CitraClient.Discord;
namespace CitraClient
{
	public class MainLoader : MelonMod
	{
        public override void OnApplicationLateStart()
        {
			DiscordRPC.Start();

		}
        public override void OnApplicationStart()
		{
			OnStartUp();
		}

		private static void OnStartUp()
		{
			PlayerUtils.LoadAvatarMethod = typeof(VRCPlayer).GetMethods().First((MethodInfo methodInfo) => methodInfo.Name.StartsWith("Method_Private_Void_Boolean_") && methodInfo.Name.Length < 31 && methodInfo.GetParameters().Any((ParameterInfo info) => info.IsOptional) && ApplicationUtils.CheckUsedBy(methodInfo, "ReloadAvatarNetworkedRPC"));
			ClassInjector.RegisterTypeInIl2Cpp<CitraMonoBehaviour>();
			GameObject gameObject = new GameObject("CitraMonoBehaviour");
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.AddComponent<CitraMonoBehaviour>();
			AudioUtils.StartSong(0).Start();
			FileManager.Initialize();
			Configuration.Initialize();
			NativePatches.HookAll();
			Patches.InitPatch();
			AssetBundleLoaders.LoadMaterialAssetBundle();
			MezLogger.MakeUI().Start();
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			RPCProtections.blockedSenders.Clear();
			USpeakProtections.blockedSenders.Clear();
			WaitForObject().Start();
			foreach (ModuleBase module in CitraMonoBehaviour.Modules)
			{
				module.OnSceneLoad(buildIndex, sceneName);
			}
			static IEnumerator WaitForObject()
			{
				while (PlayerUtils.GetLocalPlayer() == null)
				{
					yield return null;
				}
				while (Playerlist.playerList == null)
				{
					yield return null;
				}
				foreach (Photon.Realtime.Player plr in PhotonUtils.GetPhotonInstance().GetAllPhotonPlayers())
				{
					int currentId = plr.GetPhotonID();
					VRC.Player currentPlayer = plr.GetPlayer();
					if (!PlayerlistManager.players.ContainsKey(currentId))
					{
						try
						{
							PhotonPlayerObject photonPlayerObject = new PhotonPlayerObject
							{
								actorId = currentId,
								isInstantiated = false,
								player = currentPlayer
							};
							PlayerlistManager.players.Add(currentId, photonPlayerObject);
						}
						catch (Exception ex)
						{
							Exception e = ex;
							ConsoleUtils.OnLogError(e.Message);
						}
					}
				}
			}
		}
	}
}
