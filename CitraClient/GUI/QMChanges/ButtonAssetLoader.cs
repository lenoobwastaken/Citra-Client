using System.Collections;
using System.Collections.Generic;
using CitraClient.API.QM;
using CitraClient.GUI.QM;
using CitraClient.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.GUI.QMChanges
{
	public static class ButtonAssetLoader
	{
		private const string _interfaceString = "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/";

		public static Material AmplifyMat;

		public static IEnumerator ButtonStuff()
		{
			if (MainMenu.qmChangesBtn != null)
			{
				MainMenu.qmChangesBtn.DestroyMe();
			}
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Foreground")?.gameObject.SetActive(value: true);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Foreground")?.gameObject.SetActive(value: true);
			yield return new WaitForEndOfFrame();
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Foreground");
			yield return new WaitForEndOfFrame();
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Foreground");
			AddButtonMaterial("VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Foreground");
			yield return new WaitForEndOfFrame();
			QMConsole.RemoveCarousel();
			QMConsole.RemoveHeaders();
			QMConsole.ChangeUiImage();
			QMConsole.CreateConsole().Start();
		}

		public static void AddButtonMaterial(string locationString)
		{
			List<GameObject> list = new List<GameObject>();
			GameObject gameObject = APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/");
			list.Add(gameObject.transform.Find(locationString).gameObject);
			try
			{
				foreach (GameObject item in list)
				{
					item.GetComponent<Image>().material = AmplifyMat;
				}
			}
			catch
			{
			}
		}
	}
}
