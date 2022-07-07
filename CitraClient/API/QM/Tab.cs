using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;

namespace CitraClient.API.QM
{
	public class Tab : QMButtonBase
	{
		public readonly MenuTab menuTab;

		public readonly Image tabIcon;

		public GameObject gameObject;

		public Tab(QMNestedButton menu, string tooltip, Sprite icon = null, Transform parent = null)
		{
			if (parent == null)
			{
				parent = APIStuff.GetTabBase().transform.parent;
			}
			button = UnityEngine.Object.Instantiate(APIStuff.GetTabBase(), parent);
			gameObject = button;
			button.name = menu.GetMenuName() + "Tab";
			tabIcon = button.transform.Find("Icon").GetComponent<Image>();
			tabIcon.sprite = icon;
			tabIcon.overrideSprite = icon;
			menuTab = button.GetComponent<MenuTab>();
			menuTab.field_Private_MenuStateController_0 = APIStuff.GetMenuStateControllerInstance();
			menuTab.field_Public_String_0 = menu.GetMenuName();
			UnityEngine.Object.Destroy(button.transform.FindChild("Badge"));
			menuTab.gameObject.GetComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetComponent<Button>();
			menuTab.gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
			SetTooltipColor("#00d1ed");
			menuTab.gameObject.GetComponent<Button>().onClick.AddListener((Action)delegate
			{
				menuTab.gameObject.GetComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetComponent<Button>();
			});
		}
	}
}
