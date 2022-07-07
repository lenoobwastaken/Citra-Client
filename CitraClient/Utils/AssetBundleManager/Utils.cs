using System.Collections;
using CitraClient.Modules.World;
using Il2CppSystem;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.Utils.AssetBundleManager
{
	public static class Utils
	{
		public const string SkyBoxPrefabNameOne = "Purplenebula.mat";

		public const string SkyBoxPrefabNameTwo = "funnymat.mat";

		public const string SkyBoxPrefabNameThree = "SkyboxWarm.mat";

		public const string SkyBoxPrefabNameFour = "sevmat.mat";

		public const string SkyBoxPrefabNameFive = "nightmat.mat";

		public const string SkyBoxPrefabNameSix = "SkyboxDay.mat";

		public const string SkyBoxPrefabNameSeven = "night_1_mat.mat";

		public const string SkyBoxPrefabNameEight = "night_3_mat.mat";

		public const string SkyBoxPrefabNameNine = "night_4_mat.mat";

		public const string SkyBoxPrefabNameTen = "night_5_mat.mat";

		public const string SkyBoxBundleNameOne = "purplenebula";

		public const string SkyBoxBundleNameTwo = "funnybundle";

		public const string SkyBoxBundleNameThree = "skyboxwarm";

		public const string SkyBoxBundleNameFour = "sevbundle";

		public const string SkyBoxBundleNameFive = "nightbundle";

		public const string SkyBoxBundleNameSix = "skyboxday";

		public const string SkyBoxBundleNameSeven = "night_1_bundle";

		public const string SkyBoxBundleNameEight = "night_3_bundle";

		public const string SkyBoxBundleNameNine = "night_4_bundle";

		public const string SkyBoxBundleNameTen = "night_5_bundle";

		public static void DropPortal()
		{
			GameObject gameObject = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject;
			GameObject targetObject = Networking.Instantiate(VRC_EventHandler.VrcBroadcastType.Always, "Portals/PortalInternalDynamic", gameObject.transform.position + gameObject.transform.forward * 1.7f, gameObject.transform.rotation);
			Il2CppSystem.Object[] array = new Il2CppSystem.Object[3] { "wrld_5b89c79e-c340-4510-be1b-476e9fcdedcc", "\r\t\0~region(jp)", null };
			Int32 @int = default(Int32);
			@int.m_value = 0;
			array[2] = @int.BoxIl2CppObject();
			Networking.RPC(RPC.Destination.AllBufferOne, targetObject, "ConfigurePortal", array);
			GameObject gameObject2 = GameObject.Find("PortalGraphics").gameObject;
			if (gameObject2 != null)
			{
				UnityEngine.Object.Destroy(gameObject2.gameObject);
			}
		}

		public static IEnumerator ResetSkybox(float secondsToWait)
		{
			AssetBundleLoaders.LoadSkyBoxBundle("funnymat.mat", "funnybundle");
			yield return new WaitForSeconds(secondsToWait);
			WorldModules.CopyInstanceToClipboard();
			WorldModules.JoinInstanceFromClipboard();
		}
	}
}
