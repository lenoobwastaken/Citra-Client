using System;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace CitraClient.API.QM
{
	public static class APIStuff
	{
		public enum SMLocations
		{
			Worlds,
			Avatars,
			Social,
			Settings,
			Safety,
			UserInfo
		}

		private static VRC.UI.Elements.QuickMenu QuickMenuInstance;

		private static GameObject qmBase;

		private static GameObject qmParent;

		private static GameObject SingleButtonReference;

		private static GameObject MenuPageReference;

		private static GameObject menuTabBase;

		private static GameObject sliderBase;

		private static GameObject labelBase;

		private static GameObject quarterBase;

		private static SelectedUserMenuQM SelectedUserMenuQM;

		private static GameObject leftWing;

		private static GameObject rightWing;

		private static GameObject panelBase;

		private static GameObject panelParent;

		private static GameObject textBase;

		private static Sprite OnIconReference;

		private static Sprite OffIconReference;

		private static readonly System.Random rnd = new System.Random();

		private static MenuStateController MenuStateControllerInstance { get; set; }

		private static GameObject SocialMenuInstance { get; set; }

		public static SelectedUserMenuQM GetSelectedUserMenuQM
		{
			get
			{
				if (SelectedUserMenuQM == null)
				{
					SelectedUserMenuQM = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
				}
				return SelectedUserMenuQM;
			}
		}

		public static GameObject GetQuarterBase()
		{
			if (quarterBase == null)
			{
				quarterBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/MicButton/").gameObject;
			}
			return quarterBase;
		}

		public static GameObject GetTextBase()
		{
			if (textBase == null)
			{
				textBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").gameObject;
			}
			return textBase;
		}

		public static GameObject GetTabBase()
		{
			if (menuTabBase == null)
			{
				menuTabBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
			}
			return menuTabBase;
		}

		public static GameObject GetSocialMenuInstance()
		{
			if (SocialMenuInstance == null)
			{
				SocialMenuInstance = GameObject.Find("UserInterface/MenuContent/Screens");
			}
			return SocialMenuInstance;
		}

		public static MenuStateController GetMenuStateControllerInstance()
		{
			if (MenuStateControllerInstance == null)
			{
				MenuStateControllerInstance = GetQuickMenuInstance().transform.GetComponent<MenuStateController>();
			}
			return MenuStateControllerInstance;
		}

		internal static VRC.UI.Elements.QuickMenu GetQuickMenuInstance()
		{
			if (QuickMenuInstance == null)
			{
				QuickMenuInstance = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>()[0];
			}
			return QuickMenuInstance;
		}

		internal static GameObject GetQMParent()
		{
			if (qmParent == null)
			{
				qmParent = FindInactive("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/").gameObject;
			}
			return qmParent;
		}

		internal static GameObject GetQMBase()
		{
			if (qmBase == null)
			{
				qmBase = GetQuickMenuInstance().transform.Find("Canvas_QuickMenu(Clone)/Container").gameObject;
			}
			return qmBase;
		}

		internal static GameObject GetLeftWing()
		{
			if (leftWing == null)
			{
				leftWing = GetQuickMenuInstance().transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left").gameObject;
			}
			return leftWing;
		}

		internal static GameObject GetRightWing()
		{
			if (rightWing == null)
			{
				rightWing = GetQuickMenuInstance().transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right").gameObject;
			}
			return rightWing;
		}

		internal static GameObject SingleButtonTemplate()
		{
			if (SingleButtonReference == null)
			{
				Il2CppArrayBase<Button> componentsInChildren = GetQuickMenuInstance().GetComponentsInChildren<Button>(includeInactive: true);
				foreach (Button item in componentsInChildren)
				{
					if (item.name == "Button_Screenshot")
					{
						SingleButtonReference = item.gameObject;
					}
				}
			}
			return SingleButtonReference;
		}

		internal static GameObject LabelTemplate()
		{
			if (labelBase == null)
			{
				labelBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/QM_Foldout_UI_Elements/Label").gameObject;
			}
			return labelBase;
		}

		internal static GameObject PanelTemplate()
		{
			if (panelBase == null)
			{
				panelBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/").gameObject;
			}
			return panelBase;
		}

		internal static GameObject PanelParentTemplate()
		{
			if (panelParent == null)
			{
				panelParent = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/").gameObject;
			}
			return panelParent;
		}

		internal static GameObject SliderTemplate()
		{
			if (sliderBase == null)
			{
				sliderBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_AudioSettings/Content/Audio/VolumeSlider_Master").gameObject;
			}
			return sliderBase;
		}

		internal static GameObject GetMenuPageTemplate()
		{
			if (MenuPageReference == null)
			{
				MenuPageReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard").gameObject;
			}
			return MenuPageReference;
		}

		public static Sprite GetOnIconSprite()
		{
			if (OnIconReference == null)
			{
				OnIconReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<Image>().sprite;
			}
			return OnIconReference;
		}

		public static Sprite GetOffIconSprite()
		{
			if (OffIconReference == null)
			{
				OffIconReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo/Icon_Off").GetComponent<Image>().sprite;
			}
			return OffIconReference;
		}

		internal static int RandomNumbers()
		{
			return rnd.Next(10000, 99999);
		}

		internal static void DestroyChildren(this Transform transform)
		{
			transform.DestroyChildren(null);
		}

		internal static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude)
		{
			for (int num = transform.childCount - 1; num >= 0; num--)
			{
				if (exclude == null || exclude(transform.GetChild(num)))
				{
					UnityEngine.Object.DestroyImmediate(transform.GetChild(num).gameObject);
				}
			}
		}

		public static GameObject FindInactive(string path)
		{
			string[] array = path.Split(new char[1] { '/' }, 2);
			Transform transform = GameObject.Find("/" + array[0])?.transform;
			if (transform == null)
			{
				return null;
			}
			return Transform.FindRelativeTransformWithPath(transform, array[1], isActiveOnly: false)?.gameObject;
		}
	}
}
