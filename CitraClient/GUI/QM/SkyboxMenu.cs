using CitraClient.API.QM;
using CitraClient.Utils;
using CitraClient.Utils.AssetBundleManager;

namespace CitraClient.GUI.QM
{
	public static class SkyboxMenu
	{
		public static QMNestedButton skyboxMenu;

		public static void InitSkyboxMenuMenu()
		{
			skyboxMenu = new QMNestedButton(MainMenu.MainPage, "SkyBoxes", 4f, 2f, "Menu that contains different SkyBoxes", "<color=#CE389C>Citra:</color> <color=#00d1ed>SkyBoxes</color>");
			new QMSingleButton(skyboxMenu, 1f, 0f, "Purple Nebula", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("Purplenebula.mat", "purplenebula");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 2f, 0f, "City", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("sevmat.mat", "sevbundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 3f, 0f, "Night", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("nightmat.mat", "nightbundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 4f, 0f, "Night_1", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("night_1_mat.mat", "night_1_bundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 1f, 1f, "Night_2", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("night_3_mat.mat", "night_3_bundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 2f, 1f, "Night_3", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("night_4_mat.mat", "night_4_bundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 3f, 1f, "Night_4", delegate
			{
				AssetBundleLoaders.LoadSkyBoxBundle("night_5_mat.mat", "night_5_bundle");
			}, "Changes The SkyBox Locally");
			new QMSingleButton(skyboxMenu, 4f, 3f, "Reset Skybox", delegate
			{
				CitraClient.Utils.AssetBundleManager.Utils.ResetSkybox(3f).Start();
			}, "Resets the Skybox back to its default Material");
		}
	}
}
