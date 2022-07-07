using System;
using System.Collections;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace KiraiMod.WingAPI
{
	public class WingAPI : ModuleBase
	{
		public static Action<Wing.BaseWing> OnWingInit = delegate
		{
		};

		private static bool hasInitialized = false;

		private static Action Init_L = delegate
		{
			Init_L = delegate
			{
			};
			ConsoleUtils.OnLogInfo("Creating Left Wing UI");
			OnWingInit(Wing.Left);
		};

		private static Action Init_R = delegate
		{
			Init_R = delegate
			{
			};
			ConsoleUtils.OnLogInfo("Creating Right Wing UI");
			OnWingInit(Wing.Right);
		};

		public override void Start()
		{
			Initialize();
		}

		public static void Initialize()
		{
			if (!hasInitialized)
			{
				hasInitialized = true;
				MelonCoroutines.Start(FindUI());
			}
		}

		private static IEnumerator FindUI()
		{
			while ((object)(Wing.Misc.UserInterface = GameObject.Find("UserInterface")?.transform) == null)
			{
				yield return null;
			}
			while ((object)(Wing.Misc.QuickMenu = Wing.Misc.UserInterface.Find("Canvas_QuickMenu(Clone)")) == null)
			{
				yield return null;
			}
			Wing.Left.Setup(Wing.Misc.QuickMenu.Find("Container/Window/Wing_Left"));
			Wing.Right.Setup(Wing.Misc.QuickMenu.Find("Container/Window/Wing_Right"));
			Wing.Left.WingOpen.GetComponent<Button>().onClick.AddListener((Action)delegate
			{
				Init_L();
			});
			Wing.Right.WingOpen.GetComponent<Button>().onClick.AddListener((Action)delegate
			{
				Init_R();
			});
		}
	}
}
