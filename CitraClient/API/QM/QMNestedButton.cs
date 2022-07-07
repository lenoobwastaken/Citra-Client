using System;
using System.Collections.Generic;
using System.Linq;
using CitraClient.Utils.UI;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace CitraClient.API.QM
{
	public class QMNestedButton
	{
		protected GameObject BackButton;

		protected string btnQMLoc;

		protected bool IsMenuRoot;

		protected QMSingleButton MainButton;

		protected string MenuName;

		protected GameObject MenuObject;

		protected UIPage MenuPage;

		protected TextMeshProUGUI MenuTitleText;

		public QMNestedButton(QMNestedButton location, string btnText, float posX, float posY, string toolTipText, string menuTitle, bool halfButton = false, Sprite icon = null)
		{
			btnQMLoc = location.GetMenuName();
			Initialize(isRoot: false, btnText, posX, posY, toolTipText, menuTitle, halfButton, icon);
		}

		public QMNestedButton(string location, string btnText, float posX, float posY, string toolTipText, string menuTitle, bool halfButton = false, Sprite icon = null)
		{
			btnQMLoc = location;
			Initialize(location.StartsWith("Menu_"), btnText, posX, posY, toolTipText, menuTitle, halfButton, icon);
		}

		private void Initialize(bool isRoot, string btnText, float btnPosX, float btnPosY, string btnToolTipText, string menuTitle, bool halfButton = false, Sprite icon = null)
		{
			MenuName = string.Format("{0}-Menu-{1}", "CitraClient", APIStuff.RandomNumbers());
			MenuObject = UnityEngine.Object.Instantiate(APIStuff.GetMenuPageTemplate(), APIStuff.GetMenuPageTemplate().transform.parent);
			MenuObject.name = MenuName;
			MenuObject.SetActive(value: false);
			UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadQMMenu>());
			MenuPage = MenuObject.AddComponent<UIPage>();
			MenuPage.field_Public_String_0 = MenuName;
			MenuPage.field_Private_Boolean_1 = true;
			MenuPage.field_Protected_MenuStateController_0 = APIStuff.GetMenuStateControllerInstance();
			MenuPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
			MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
			APIStuff.GetMenuStateControllerInstance().field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);
			if (isRoot)
			{
				System.Collections.Generic.List<UIPage> list = APIStuff.GetMenuStateControllerInstance().field_Public_ArrayOf_UIPage_0.ToList();
				list.Add(MenuPage);
				APIStuff.GetMenuStateControllerInstance().field_Public_ArrayOf_UIPage_0 = list.ToArray();
			}
			MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
			MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
			MenuTitleText.text = menuTitle;
			MenuTitleText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(95f, -50f);
			IsMenuRoot = isRoot;
			BackButton = MenuObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
			BackButton.SetActive(value: true);
			BackButton.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
			BackButton.GetComponentInChildren<Button>().onClick.AddListener((Action)delegate
			{
				if (isRoot)
				{
					if (btnQMLoc.StartsWith("Menu_"))
					{
						APIStuff.GetMenuStateControllerInstance().Method_Public_Void_String_Boolean_Boolean_0("QuickMenu" + btnQMLoc.Remove(0, 5));
					}
					else
					{
						APIStuff.GetMenuStateControllerInstance().Method_Public_Void_String_Boolean_Boolean_0(btnQMLoc);
					}
				}
				else
				{
					MenuPage.Method_Protected_Virtual_New_Void_0();
				}
			});
			MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(value: false);
			MainButton = new QMSingleButton(btnQMLoc, btnPosX, btnPosY, btnText, OpenMe, btnToolTipText, null, halfButton, icon);
			MainButton.SetTextColor(UIColors.textColor);
			for (int i = 0; i < MenuObject.transform.childCount; i++)
			{
				if (MenuObject.transform.GetChild(i).name != "Header_H1" && MenuObject.transform.GetChild(i).name != "ScrollRect")
				{
					UnityEngine.Object.Destroy(MenuObject.transform.GetChild(i).gameObject);
				}
			}
			CitraQMAPI.allQMNestedButtons.Add(this);
		}

		public void OpenMe()
		{
			APIStuff.GetMenuStateControllerInstance().Method_Public_Void_String_UIContext_Boolean_TransitionType_0(MenuPage.field_Public_String_0, null, param_3: false, UIPage.TransitionType.None);
		}

		public void CloseMe()
		{
			MenuPage.Method_Public_Virtual_New_Void_0();
		}

		public string GetMenuName()
		{
			return MenuName;
		}

		public GameObject GetMenuObject()
		{
			return MenuObject;
		}

		public QMSingleButton GetMainButton()
		{
			return MainButton;
		}

		public GameObject GetBackButton()
		{
			return BackButton;
		}
	}
}
