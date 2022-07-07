using CitraClient.API.QM;
using CitraClient.GUI.QM.Playerlist;
using CitraClient.GUI.QM.Submenus;
using CitraClient.GUI.QMChanges;
using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.GUI.QM
{
	public class MainMenu
	{
		public static Tab MainTab;

		public static QMNestedButton MainPage;

		public static QMSingleButton qmChangesBtn;

		public static void CreateMainMenu()
		{
			ImageUtils.SetSprites();
			CitraClient.GUI.QM.Playerlist.Playerlist.Init();
			Sprite citraLogoSprite = ImageUtils.CitraLogoSprite;
			MainPage = new QMNestedButton("Menu_Dashboard", "", 1000f, 1000f, "", "<color=#CE389C>Citra:</color>", halfButton: true);
			MainTab = new Tab(MainPage, "Main menu for Citra", citraLogoSprite);
			new QMLabel(MainPage, "Features", 65f, 375f);
			qmChangesBtn = new QMSingleButton("Menu_Dashboard", 3.3f, -0.5f, string.Empty, delegate
			{
				ButtonAssetLoader.ButtonStuff().Start();
			}, "Apply Quick Menu Changes.", null, halfBtn: true);
			InitializeMenus(qmChangesBtn);
		}

		private static void InitializeMenus(QMSingleButton btn)
		{
			btn.SetBackgroundImage(ImageUtils.CitraMenuBtnSprite);
			FunctionMenu.InitFunctionMenu();
			KeybindMenu.Init();
			ExploitMenu.InitExploitMenu();
			TargetMainMenu.InitTargetMenu();
			WorldMenu.InitWorldMenu();
			SkyboxMenu.InitSkyboxMenuMenu();
			MediaMenu.InitMediaMenu();
			SafetyMenu.InitSafetyMenu();
			InstanceHistoryMenu.InitInstanceHistoryMenu();
			PickupMenu.InitPickupMenu();
			UdonMenu.InitUdonMenu();
			TargetCrashMenu.InitTargetCrashMenu();
		}
	}
}
