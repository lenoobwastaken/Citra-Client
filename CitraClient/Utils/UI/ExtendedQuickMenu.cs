using System.Collections;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace CitraClient.Utils.UI
{
	public static class ExtendedQuickMenu
	{
		public static GameObject QuickMenuBase;

		public static GameObject MenuTabBase;

		public static GameObject MenuContent;

		public static GameObject QmParent;

		public static GameObject LeftWingMenu;

		private static VRC.UI.Elements.QuickMenu _quickMenuInstance;

		private static MenuStateController _menuStateCtrl;

		private static SelectedUserMenuQM _selectedUserLocal;

		private static Wing[] _wings;

		private static Wing _leftWing;

		private static Wing _rightWing;

		private static Transform _cameraMenu;

		private static Sprite _onIconSprite;

		private static Sprite _offIconSprite;

		private static GameObject _togglePrefab;

		public static VRC.UI.Elements.QuickMenu Instance
		{
			get
			{
				if (_quickMenuInstance == null)
				{
					_quickMenuInstance = GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(includeInactive: true);
				}
				return _quickMenuInstance;
			}
		}

		public static MenuStateController MenuStateCtrl
		{
			get
			{
				if (_menuStateCtrl == null)
				{
					_menuStateCtrl = Instance.transform.GetComponent<MenuStateController>();
				}
				return _menuStateCtrl;
			}
		}

		public static SelectedUserMenuQM SelectedUserLocal
		{
			get
			{
				if (_selectedUserLocal == null)
				{
					_selectedUserLocal = Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
				}
				return _selectedUserLocal;
			}
		}

		public static Wing[] Wings
		{
			get
			{
				if (_wings == null || _wings.Length == 0)
				{
					_wings = GameObject.Find("UserInterface").GetComponentsInChildren<Wing>(includeInactive: true);
				}
				return _wings;
			}
		}

		public static Transform CameraMenu
		{
			get
			{
				if (_cameraMenu == null)
				{
					_cameraMenu = Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Camera");
				}
				return _cameraMenu;
			}
		}

		public static Sprite OnIconSprite
		{
			get
			{
				if (_onIconSprite == null)
				{
					_onIconSprite = Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<Image>().sprite;
				}
				return _onIconSprite;
			}
		}

		public static Sprite OffIconSprite
		{
			get
			{
				if (_offIconSprite == null)
				{
					_offIconSprite = TogglePrefab.transform.Find("Icon_Off").GetComponent<Image>().sprite;
				}
				return _offIconSprite;
			}
		}

		public static GameObject TogglePrefab
		{
			get
			{
				if (_togglePrefab == null)
				{
					_togglePrefab = Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Settings/Panel_QM_ScrollRect").GetComponent<ScrollRect>().content.Find("Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject;
				}
				return _togglePrefab;
			}
		}

		public static IEnumerator GetElements()
		{
			while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/") == null)
			{
				yield return new WaitForEndOfFrame();
			}
			LeftWingMenu = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/").gameObject;
			QuickMenuBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container").gameObject;
			MenuTabBase = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/").gameObject;
			MenuContent = GameObject.Find("UserInterface").transform.Find("MenuContent").gameObject;
			QmParent = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/").gameObject;
		}

		public static GameObject GetQmBase()
		{
			MelonCoroutines.Start(GetElements());
			return QuickMenuBase;
		}

		public static GameObject GetMenuTabBase()
		{
			MelonCoroutines.Start(GetElements());
			return MenuTabBase;
		}

		public static GameObject GetQmParent()
		{
			MelonCoroutines.Start(GetElements());
			return QmParent;
		}

		public static GameObject GetMenuContent()
		{
			MelonCoroutines.Start(GetElements());
			return MenuContent;
		}

		public static GameObject GetLwMenu()
		{
			MelonCoroutines.Start(GetElements());
			return LeftWingMenu;
		}
	}
}
