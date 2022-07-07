using System.IO;
using System.Reflection;
using CitraClient.GUI.QMChanges;
using CitraClient.Utils.Managers;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace CitraClient.Utils.AssetBundleManager
{
	public static class AssetBundleLoaders
	{
		private static GameObject _portalPrefab;

		private static GameObject _portalObj;

		private static Material _skyBoxMaterial;

		private static AssetBundle MaterialAssetBundle { get; set; }

		private static AssetBundle PortalAssetBundle { get; set; }

		private static AssetBundle SkyBoxAssetBundle { get; set; }

		public static void LoadMaterialAssetBundle()
		{
			using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CitraClient.buttonbundle");
			using MemoryStream memoryStream = new MemoryStream((int)stream.Length);
			stream.CopyTo(memoryStream);
			MaterialAssetBundle = AssetBundle.LoadFromMemory_Internal(memoryStream.ToArray(), 0u);
			MaterialAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			ButtonAssetLoader.AmplifyMat = MaterialAssetBundle.LoadAsset_Internal("buttonmat.mat", Il2CppType.Of<Material>()).Cast<Material>();
			ButtonAssetLoader.AmplifyMat.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			Object.Instantiate(ButtonAssetLoader.AmplifyMat);
			if (ButtonAssetLoader.AmplifyMat == null)
			{
				ConsoleUtils.OnLogError("Failed to Load Button Asset Bundle");
			}
			MaterialAssetBundle.Unload(unloadAllLoadedObjects: false);
		}

		public static void LoadPortalAssetBundle()
		{
			GameObject gameObject = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject;
			if (_portalObj != null)
			{
				Object.Destroy(_portalObj);
				return;
			}
			Quaternion rotation = gameObject.transform.rotation;
			GameObject gameObject2 = new GameObject("Citra_Portal_Container");
			gameObject2.transform.position = gameObject.transform.position + gameObject.transform.forward * 2f;
			gameObject2.transform.rotation = Quaternion.Euler(rotation.x, -90f, rotation.z);
			_portalObj = gameObject2;
			using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CitraClient.portalbundle");
			using (MemoryStream memoryStream = new MemoryStream((int)stream.Length))
			{
				stream.CopyTo(memoryStream);
				PortalAssetBundle = AssetBundle.LoadFromMemory_Internal(memoryStream.ToArray(), 0u);
				PortalAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				_portalPrefab = PortalAssetBundle.LoadAsset_Internal("portal", Il2CppType.Of<GameObject>()).Cast<GameObject>();
				_portalPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				if (_portalObj != null)
				{
					Object.Instantiate(_portalPrefab, GameObject.Find("Citra_Portal_Container").transform);
					ConsoleUtils.OnLogInfo((_portalPrefab == null) ? "Failed to Load Portal Asset Bundle" : "Successfully Loaded Portal Asset Bundle");
				}
				_portalObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				PortalAssetBundle.Unload(unloadAllLoadedObjects: false);
			}
			Utils.DropPortal();
		}

		public static void LoadSkyBoxBundle(string prefabName, string bundleName)
		{
			SkyBoxAssetBundle = AssetBundle.LoadFromFile(Path.Combine(FileManager.SkyboxDirectory, bundleName));
			_skyBoxMaterial = SkyBoxAssetBundle.LoadAsset_Internal(prefabName, Il2CppType.Of<Material>()).Cast<Material>();
			Object.Instantiate(_skyBoxMaterial);
			if (_skyBoxMaterial == null)
			{
				ConsoleUtils.OnLogError("Failed to Load SkyBox Asset Bundle");
			}
			RenderSettings.skybox = _skyBoxMaterial;
			SkyBoxAssetBundle.Unload(unloadAllLoadedObjects: false);
		}
	}
}
