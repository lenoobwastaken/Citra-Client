using System;
using System.Collections;
using System.Collections.Generic;
using CitraClient.API.QM;
using CitraClient.CitraNamePlates;
using CitraClient.Config;
using CitraClient.GUI.GUITabs;
using CitraClient.GUI.QM;
using CitraClient.GUI.QM.Console;
using CitraClient.GUI.QM.Playerlist;
using CitraClient.GUI.QM.Submenus;
using CitraClient.GUI.QMChanges;
using CitraClient.Modules.Exploits;
using CitraClient.Modules.Misc;
using CitraClient.Modules.Misc.TitleChanger;
using CitraClient.Modules.Movement;
using CitraClient.Modules.Protections;
using CitraClient.Modules.World;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using UnityEngine;
using VRC.Core;
using VRC.UI.Core;
using VRC.UI.Elements;

namespace CitraClient.Modules.Base
{
	public class CitraMonoBehaviour : MonoBehaviour
	{
		public static readonly List<ModuleBase> Modules = new List<ModuleBase>();

		public static LoadButton RestartButton;

		public static LoadButton QuitButton;

		public CitraMonoBehaviour(IntPtr ptr)
			: base(ptr)
		{
		}

		public void Start()
		{
			Modules.Add(new Thirdperson());
			Modules.Add(new PickupExploits());
			Modules.Add(new CenterConsole());
			Modules.Add(new Swastika());
			Modules.Add(new Memory());
			Modules.Add(new WorldProtections());
			Modules.Add(new MediaControl());
			Modules.Add(new InstanceHistory());
			Modules.Add(new VideoBG());
			Modules.Add(new WorldExploits());
			Modules.Add(new TempMovement());
			Modules.Add(new MirrorMod());
			foreach (ModuleBase module in Modules)
			{
				module.Start();
			}
			APIAwait(delegate
			{
				WelcomeBack(10f).Start();
			});
			UIAwait(delegate
			{
				foreach (ModuleBase module2 in Modules)
				{
					module2.QMLoaded();
				}
				TitleHandler.ChangeWindowTitle(Patterns.StaticTitle);
				MainMenu.CreateMainMenu();
				PlayerlistManager.LoopPlayerlist().Start();
				CustomPlates.Initialize();
				RestartButton = new LoadButton(new Vector3(-1269.999f, 1175.384f, 0f), new Vector3(1f, 1f, 1f), "Restart", "RestartLoadButton", delegate
				{
					ApplicationUtils.RestartGame();
				}, Color.white, Color.cyan);
				QuitButton = new LoadButton(new Vector3(-1269.999f, 1041.384f, 0f), new Vector3(1f, 1f, 1f), "Quit", "QuitLoadButton", delegate
				{
					ApplicationUtils.QuitGame();
				}, Color.white, Color.cyan);
			});
			RPCProtections.ClearRatelimit().Start();
		}

		private void Update()
		{
			foreach (ModuleBase module in Modules)
			{
				module.Update();
			}
		}

		private void OnGUI()
		{
			HandleInputs();
			MainGUI.Tab();
			foreach (ModuleBase module in Modules)
			{
				module.GUI();
			}
		}

		private static void UIAwait(Action action)
		{
			WaitForUI(action).Start();
		}

		private static IEnumerator WaitForUI(Action action)
		{
			while (VRCUiManager.prop_VRCUiManager_0 == null || UIManager.field_Private_Static_UIManager_0 == null || GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(includeInactive: true) == null || APIStuff.GetMenuStateControllerInstance() == null || APIStuff.GetMenuPageTemplate() == null)
			{
				yield return null;
			}
			action();
		}

		public static void APIAwait(Action action)
		{
			WaitForAPI(action).Start();
		}

		private static IEnumerator WaitForAPI(Action action)
		{
			while (APIUser.CurrentUser == null)
			{
				yield return null;
			}
			action();
		}

		public void HandleKeybinds()
		{
			if (!(PlayerUtils.GetLocalPlayer() != null))
			{
				return;
			}
			if (Configuration.GetConfig().basicFlyKeybind && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown((KeyCode)Configuration.GetConfig().basicFlyKeybindNum))
			{
				MovementMenu.basicFlyButton.SetToggleState(!RuntimeConfig.basicfly, shouldInvoke: true);
			}
			if (Configuration.GetConfig().directionalFlyKeybind && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown((KeyCode)Configuration.GetConfig().directionalFlyKeybindNum))
			{
				MovementMenu.directionalFlyButton.SetToggleState(!RuntimeConfig.directionalFly, shouldInvoke: true);
			}
			if (Configuration.GetConfig().noclipKeybind && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown((KeyCode)Configuration.GetConfig().noclipKeybindNum))
			{
				MovementMenu.noclipButton.SetToggleState(!RuntimeConfig.noclip, shouldInvoke: true);
			}
			if (Configuration.GetConfig().thirdPersonKeybind && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown((KeyCode)Configuration.GetConfig().thirdPersonKeybindNum))
			{
				int mode = Thirdperson.cameraMode;
				if (mode++ > 1)
				{
					mode = 0;
				}
				Thirdperson.SetMode(mode);
			}
		}

		public void HandleInputs()
		{
			if (PlayerUtils.GetLocalPlayer() != null)
			{
				Event current = Event.current;
				if (KeybindMenu.waitingForBF && current.isKey)
				{
					Configuration.GetConfig().basicFlyKeybindNum = (int)current.keyCode;
					Configuration.Save();
					KeybindMenu.waitingForBF = false;
					QMSingleButton bfKeyButton = KeybindMenu.bfKeyButton;
					KeyCode basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().basicFlyKeybindNum;
					bfKeyButton.SetButtonText(basicFlyKeybindNum.ToString());
					PlayerUtils.ToggleInput(b: true);
				}
				if (KeybindMenu.waitingForDF && current.isKey)
				{
					Configuration.GetConfig().directionalFlyKeybindNum = (int)current.keyCode;
					Configuration.Save();
					KeybindMenu.waitingForDF = false;
					QMSingleButton dfKeyButton = KeybindMenu.dfKeyButton;
					KeyCode basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().directionalFlyKeybindNum;
					dfKeyButton.SetButtonText(basicFlyKeybindNum.ToString());
					PlayerUtils.ToggleInput(b: true);
				}
				if (KeybindMenu.waitingForNC && current.isKey)
				{
					Configuration.GetConfig().noclipKeybindNum = (int)current.keyCode;
					Configuration.Save();
					KeybindMenu.waitingForNC = false;
					QMSingleButton ncKeyButton = KeybindMenu.ncKeyButton;
					KeyCode basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().noclipKeybindNum;
					ncKeyButton.SetButtonText(basicFlyKeybindNum.ToString());
					PlayerUtils.ToggleInput(b: true);
				}
				if (KeybindMenu.waitingForTP && current.isKey)
				{
					Configuration.GetConfig().thirdPersonKeybindNum = (int)current.keyCode;
					Configuration.Save();
					KeybindMenu.waitingForTP = false;
					QMSingleButton tpKeyButton = KeybindMenu.tpKeyButton;
					KeyCode basicFlyKeybindNum = (KeyCode)Configuration.GetConfig().thirdPersonKeybindNum;
					tpKeyButton.SetButtonText(basicFlyKeybindNum.ToString());
					PlayerUtils.ToggleInput(b: true);
				}
			}
		}

		private static IEnumerator WelcomeBack(float msgTime)
		{
			while (!WorldUtils.IsInWorld())
			{
				yield return null;
			}
			yield return new WaitForSeconds(7.5f);
			MezLogger.HudMsg("Welcome Back " + PlayerUtils.GetLocalAPIUser().displayName + "!", msgTime);
		}
	}
}
