using System;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;

namespace CitraClient.API.QM
{
	public class QMQuarterButton
	{
		public readonly RectTransform btnRect;

		public readonly Button buttonComponent;

		public readonly GameObject gameObject;

		public readonly UiToggleTooltip uiToggleTooltip;

		public QMQuarterButton(float posX, float posY, string tooltip, Action action, Sprite icon)
		{
			gameObject = UnityEngine.Object.Instantiate(APIStuff.GetQuarterBase(), APIStuff.GetQuarterBase().transform.parent);
			uiToggleTooltip = gameObject.GetComponent<UiToggleTooltip>();
			btnRect = gameObject.GetComponent<RectTransform>();
			UnityEngine.Object.Destroy(gameObject.GetComponent<EventTrigger>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<Toggle>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<ToggleBinding>());
			gameObject.AddComponent<Button>();
			buttonComponent = gameObject.GetComponent<Button>();
			SetPosition(posX, posY);
			SetTooltip(tooltip);
			SetIcon(icon);
		}

		public void SetTooltip(string tooltip)
		{
			uiToggleTooltip.field_Public_String_0 = tooltip;
			uiToggleTooltip.field_Public_String_1 = tooltip;
		}

		public void SetPosition(float posX, float posY)
		{
			btnRect.anchoredPosition = new Vector2(posX, posY);
		}

		public void SetAction(Action action)
		{
			buttonComponent.onClick = new Button.ButtonClickedEvent();
			if (action != null)
			{
				buttonComponent.GetComponent<Button>().onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(action));
			}
		}

		public void SetIcon(Sprite icon)
		{
			gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
		}
	}
}
