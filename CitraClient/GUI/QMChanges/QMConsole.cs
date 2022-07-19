using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CitraClient.API.QM;
using CitraClient.GUI.QM.Console;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.DataModel.Core;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;

namespace CitraClient.GUI.QMChanges
{
	public static class QMConsole
	{
		private const string InterfaceString = "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/";

		private const string HorizontalLayoutGroupString = "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/";

		private const string VerticalLayoutGroupString = "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/";

		public static readonly List<GameObject> LinkButtons = new List<GameObject>();

		public static readonly List<GameObject> ActionButtons = new List<GameObject>();

		public static readonly List<GameObject> ImageList = new List<GameObject>();

		public static readonly List<GameObject> ImageListTwo = new List<GameObject>();

		public static readonly List<GameObject> BottomButtonList = new List<GameObject>();

		public static readonly List<GameObject> WingButtonsList = new List<GameObject>();

		public static Transform InstConsole;

		public static Transform InstPlayerlist;

		public static TextMeshProUGUI Text;

		public static Transform MainText;

		public static AssetBundle MaterialAssetBundle;

		public static Material AmplifyMat;

		public static IEnumerator CreateConsole()
		{
			GameObject qmBase = ExtendedQuickMenu.GetQmParent();
			Sprite consoleSprite = ImageUtils.ConsoleSprite;
			try
			{
				GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").SetActive(true);
				MainText = ExtendedQuickMenu.GetQmBase().transform.Find("Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer");
				LinkButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds").gameObject);
				LinkButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars").gameObject);
				LinkButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Social").gameObject);
				LinkButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety").gameObject);
				ActionButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome").gameObject);
				ActionButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn").gameObject);
				ActionButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser").gameObject);
				ActionButtons.Add(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState").gameObject);
				InstConsole = UnityEngine.Object.Instantiate(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars"), qmBase.transform.Find("VerticalLayoutGroup"));
				InstConsole.name = "Citra_Console";
				InstConsole.transform.Find("Icon").gameObject.SetActive(value: false);
				InstConsole.transform.Find("Badge_MMJump").gameObject.SetActive(value: false);
				InstConsole.transform.Find("Foreground").gameObject.SetActive(value: false);
				InstConsole.GetComponentInChildren<Image>().sprite = consoleSprite;
				UnityEngine.Object.Destroy(InstConsole.GetComponent<CanvasGroup>());
				UnityEngine.Object.Destroy(InstConsole.GetComponent<BindingComponent>());
				UnityEngine.Object.Destroy(InstConsole.GetComponent<Button>());
				UnityEngine.Object.Destroy(InstConsole.GetComponent<StyleElement>());
				UnityEngine.Object.Destroy(InstConsole.transform.Find("Text_H4").gameObject.GetComponent<StyleElement>());
				InstConsole.transform.Find("Text_H4").gameObject.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
				for (int I = 0; I < 17; I++)
				{
					GameObject newText = UnityEngine.Object.Instantiate(ExtendedQuickMenu.GetQmBase().transform.Find("Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").gameObject, InstConsole.transform);
					newText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(150f, -100 - CenterConsole.YPos, 0f);
					newText.SetActive(value: true);
					TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();
					textComponent.richText = true;
					textComponent.fontSize = 22f;
					textComponent.text = "";
					textComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(1600f, 100f);
					CenterConsole.YPos += 20;
					CenterConsole.AllLogsText.Add(textComponent);
				}
				InstConsole.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(964f, 500f);
				InstConsole.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(512f, -450f);
				InstConsole.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = "";
				qmBase.transform.Find("VerticalLayoutGroup/Header_QuickActions").gameObject.SetActive(value: false);
				qmBase.transform.Find("VerticalLayoutGroup/Header_QuickLinks/LeftItemContainer/Text_Title").gameObject.SetActive(value: false);
				UnityEngine.Object.Destroy(qmBase.GetComponent<RectMask2D>());
				foreach (GameObject go2 in LinkButtons)
				{
					go2.transform.Find("Icon").gameObject.SetActive(value: false);
					go2.transform.Find("Badge_MMJump").gameObject.SetActive(value: false);
				}
				foreach (GameObject go in ActionButtons)
				{
					go.transform.Find("Icon").gameObject.SetActive(value: false);
				}
				GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/").transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = "IPWS";
				GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = string.Empty;
				ConsoleCheck().Start();
				SizeCheck().Start();
				ScrollDisable().Start();
				PositionCheck().Start();
				HandleButtonColors();
			}
			catch (Exception ex)
			{
				Exception e = ex;
				ConsoleUtils.OnLogError(e.ToString());
				throw;
			}
			yield return null;
		}

		private static IEnumerator SizeCheck()
		{
			while (ExtendedQuickMenu.Instance == null)
			{
				yield return null;
			}
			while (Math.Abs(ExtendedQuickMenu.GetQmParent().transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds").GetComponent<RectTransform>().sizeDelta.y - 88f) < 88f)
			{
				yield return null;
			}
			foreach (GameObject go2 in LinkButtons)
			{
				go2.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 88f);
				go2.transform.parent.GetComponent<GridLayoutGroup>().enabled = false;
			}
			foreach (GameObject go in ActionButtons)
			{
				go.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 88f);
				go.transform.parent.GetComponent<GridLayoutGroup>().enabled = false;
			}
		}

		private static IEnumerator PositionCheck()
		{
			while (UnityEngine.Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null)
			{
				yield return null;
			}
			foreach (GameObject go2 in LinkButtons)
			{
				go2.GetComponent<RectTransform>().anchoredPosition = new Vector2(go2.GetComponent<RectTransform>().anchoredPosition.x, -140f);
				foreach (TextMeshProUGUI text in go2.GetComponentsInChildren<TextMeshProUGUI>())
				{
					text.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(text.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 20f);
					text.fontSize = 32f;
				}
			}
			foreach (GameObject go in ActionButtons)
			{
				go.GetComponent<RectTransform>().anchoredPosition = new Vector2(go.GetComponent<RectTransform>().anchoredPosition.x, -60f);
				foreach (TextMeshProUGUI textMeshProUGUI in go.GetComponentsInChildren<TextMeshProUGUI>())
				{
					textMeshProUGUI.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(textMeshProUGUI.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 20f);
					textMeshProUGUI.fontSize = 32f;
				}
			}
		}

		private static IEnumerator ConsoleCheck()
		{
			while (UnityEngine.Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null)
			{
				yield return null;
			}
			while (true)
			{
				InstConsole.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(990f, 500f);
				InstConsole.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(512f, -550f);
				yield return new WaitForEndOfFrame();
			}
		}

		private static void HandleButtonColors()
		{
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Background"));
			ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Background"));
			if (ApplicationUtils.IsInVR)
			{
				ImageList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Background"));
			}
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Dashboard/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Notifications/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Here/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Camera/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings/Background"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Toggle_SafeMode"));
			BottomButtonList.Add(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/MicButton"));
			foreach (GameObject item in ImageList.Where((GameObject gameObj) => gameObj != null))
			{
				item.GetComponent<Image>().color = Color.magenta;
			}
			foreach (TextMeshProUGUI item2 in LinkButtons.SelectMany((GameObject gameObj) => gameObj.GetComponentsInChildren<TextMeshProUGUI>()))
			{
				item2.GetComponent<TextMeshProUGUI>().color = Color.magenta;
			}
			foreach (TextMeshProUGUI item3 in ActionButtons.SelectMany((GameObject gameObj) => gameObj.GetComponentsInChildren<TextMeshProUGUI>()))
			{
				item3.GetComponent<TextMeshProUGUI>().color = Color.magenta;
			}
			foreach (GameObject item4 in BottomButtonList.Where((GameObject gameObj) => gameObj != null))
			{
				item4.GetComponent<Image>().sprite = ImageUtils.CitraButtonSprite;
			}
		}

		public static void ChangeUiImage()
		{
			Image component = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component.gameObject.GetComponent<StyleElement>());
			}
			GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().sprite = ImageUtils.QMBackgroundSprite;
			GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer02").gameObject.SetActive(value: false);
			GameObject gameObject = APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/");
			GameObject gameObject2 = gameObject.transform.Find("Background").gameObject;
			gameObject2.GetComponent<Image>().sprite = ImageUtils.CitraWingSprite;
			gameObject2.GetComponent<Graphic>().color = new Color(1f, 1f, 1f, 1f);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Button/Icon").GetComponent<Image>().sprite = ImageUtils.CitraLogoSprite;
			GameObject gameObject3 = APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/");
			GameObject gameObject4 = gameObject3.transform.Find("Background").gameObject;
			gameObject4.GetComponent<Image>().sprite = ImageUtils.CitraWingSprite;
			gameObject4.GetComponent<Graphic>().color = new Color(1f, 1f, 1f, 1f);
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Button/Icon").GetComponent<Image>().sprite = ImageUtils.CitraLogoSprite;
			ChangeFPSPanel();
		}

		public static void RemoveCarousel()
		{
			APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/")?.SetActive(value: false);
		}

		public static void RemoveHeaders()
		{
			APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks/").SetActive(value: false);
			APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions/").SetActive(value: false);
		}

		public static IEnumerator ScrollDisable()
		{
			while (ExtendedQuickMenu.Instance == null)
			{
				yield return null;
			}
			while (!ExtendedQuickMenu.GetQmBase().transform.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().enabled)
			{
				yield return null;
			}
			APIStuff.GetQMBase().transform.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().enabled = false;
		}

		public static void ClearConsole()
		{
			foreach (TextMeshProUGUI item in CenterConsole.AllLogsText.Where((TextMeshProUGUI tmp) => tmp != null))
			{
				item.text = string.Empty;
			}
		}

		public static void ChangeFPSPanel()
		{
			if (ImageUtils.FPSSprite != null)
			{
				GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Background").GetComponent<Image>().sprite = ImageUtils.FPSSprite;
			}
			if (ImageUtils.FPSSprite != null)
			{
				GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Background").GetComponent<Image>().color = new Color(0.3f, 1f, 0.91f, 1f);
			}
		}
	}
}
