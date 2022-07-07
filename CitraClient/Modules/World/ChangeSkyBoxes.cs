using System.IO;
using CitraClient.Utils;
using CitraClient.Utils.Managers;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace CitraClient.Modules.World
{
	public static class ChangeSkyBoxes
	{
		private static AssetBundle _assetBundle;

		private static Material _skyBoxMaterial;

		public static string AssetNameOne = "coalsack.mat";

		public static string AssetNameTwo = "Dark_Cygnus.mat";

		public static string AssetNameThree = "Dark_Erebus.mat";

		public static string AssetNameFour = "Nebula_Coral.mat";

		public static string AssetNameFive = "Nebula_Mariana.mat";

		public static string AssetNameSix = "Nebula_Mesa.mat";

		public static string AssetNameSeven = "Nebula_Mirage.mat";

		public static string AssetNameEight = "Nebula_Uluru.mat";

		public static string AssetNameNine = "Purplenebula.mat";

		public static string AssetNameTen = "Skyboxday.mat";

		public static string AssetNameEleven = "Skyboxnight.mat";

		public static string AssetNameTwelve = "Skyboxwarm.mat";

		public static string AssetNameThirteen = "funnymaterial.mat";

		public static string BundleNameOne = "dark_coalsack";

		public static string BundleNameTwo = "dark_cygnus";

		public static string BundleNameThree = "dark_erebus";

		public static string BundleNameFour = "nebula_coral";

		public static string BundleNameFive = "nebula_mariana";

		public static string BundleNameSix = "nebula_mesa";

		public static string BundleNameSeven = "nebula_mirage";

		public static string BundleNameEight = "nebula_uluru";

		public static string BundleNameNine = "purplenebula";

		public static string BundleNameTen = "skyboxday";

		public static string BundleNameEleven = "skyboxnight";

		public static string BundleNameTwelve = "skyboxwarm";

		public static string BundleNameThirteen = "funnybundle";

		public static void LoadBundle(string assetName, string bundleName)
		{
			_assetBundle = AssetBundle.LoadFromFile(Path.Combine(FileManager.SkyboxDirectory, bundleName));
			_skyBoxMaterial = _assetBundle.LoadAsset_Internal(assetName, Il2CppType.Of<Material>()).Cast<Material>();
			Object.Instantiate(_skyBoxMaterial);
			ConsoleUtils.OnLogInfo((_skyBoxMaterial == null) ? "Failed to Load SkyBox Asset Bundle" : "Successfully Loaded SkyBox Asset Bundle");
			RenderSettings.skybox = _skyBoxMaterial;
			_assetBundle.Unload(unloadAllLoadedObjects: false);
		}
	}
}
