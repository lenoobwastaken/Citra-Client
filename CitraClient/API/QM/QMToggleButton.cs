using System;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;

namespace CitraClient.API.QM
{
	public class QMToggleButton : QMButtonBase
	{
		protected Button btnComp;

		protected Image btnImageComp;

		protected TextMeshProUGUI btnTextComp;

		protected bool currentState;

		protected Action OffAction;

		protected Action OnAction;

		public QMToggleButton(QMNestedButton location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string btnToolTip, bool defaultState = false)
		{
			btnQMLoc = location.GetMenuName();
			Initialize(btnXPos, btnYPos, btnText, onAction, offAction, btnToolTip, defaultState);
		}

		public QMToggleButton(string location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string btnToolTip, bool defaultState = false)
		{
			btnQMLoc = location;
			Initialize(btnXPos, btnYPos, btnText, onAction, offAction, btnToolTip, defaultState);
		}

		private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action onAction, Action offAction, string btnToolTip, bool defaultState)
		{
			btnType = "ToggleButton";
			button = UnityEngine.Object.Instantiate(APIStuff.SingleButtonTemplate(), APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/" + btnQMLoc).transform, worldPositionStays: true);
			RandomNumb = APIStuff.RandomNumbers();
			button.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 176f);
			button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68f, 796f);
			btnTextComp = button.GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
			btnComp = button.GetComponentInChildren<Button>(includeInactive: true);
			btnComp.onClick = new Button.ButtonClickedEvent();
			btnComp.onClick.AddListener((Action)HandleClick);
			btnImageComp = button.transform.Find("Icon").GetComponentInChildren<Image>(includeInactive: true);
			SetBGColor(UIColors.bgColor);
			SetIconColor(UIColors.primaryColor);
			initShift[0] = 0;
			initShift[1] = 0;
			SetLocation(btnXLocation, btnYLocation);
			SetButtonText(btnText);
			SetTextColor(UIColors.textColor);
			SetButtonActions(onAction, offAction);
			SetToolTip(btnToolTip);
			SetTooltipColor("#00d1ed");
			SetActive(state: true);
			currentState = defaultState;
			Sprite sprite = (currentState ? APIStuff.GetOnIconSprite() : APIStuff.GetOffIconSprite());
			btnImageComp.sprite = sprite;
			btnImageComp.overrideSprite = sprite;
			button.transform.Find("Background").GetComponent<Image>().sprite = ImageUtils.CitraButtonSprite;
			button.transform.Find("Background").GetComponent<Image>().overrideSprite = ImageUtils.CitraButtonSprite;
			CitraQMAPI.allQMToggleButtons.Add(this);
		}

		private void HandleClick()
		{
			currentState = !currentState;
			Sprite sprite = (currentState ? APIStuff.GetOnIconSprite() : APIStuff.GetOffIconSprite());
			btnImageComp.sprite = sprite;
			btnImageComp.overrideSprite = sprite;
			if (currentState)
			{
				OnAction();
			}
			else
			{
				OffAction();
			}
		}

		public void SetButtonText(string buttonText)
		{
			button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
		}

		public void SetButtonActions(Action onAction, Action offAction)
		{
			OnAction = onAction;
			OffAction = offAction;
		}

		public void SetToggleState(bool newState, bool shouldInvoke = false)
		{
			try
			{
				Sprite sprite = (newState ? APIStuff.GetOnIconSprite() : APIStuff.GetOffIconSprite());
				btnImageComp.sprite = sprite;
				btnImageComp.overrideSprite = sprite;
				if (shouldInvoke)
				{
					if (newState)
					{
						OnAction();
					}
					else
					{
						OffAction();
					}
				}
			}
			catch
			{
			}
		}

		public void ClickMe()
		{
			HandleClick();
		}

		public bool GetCurrentState()
		{
			return currentState;
		}

		public void SetIconColor(Color color)
		{
			UnityEngine.Object.Destroy(btnImageComp.GetComponent<StyleElement>());
			btnImageComp.color = color;
		}

		public void SetBGColor(Color color)
		{
		}
	}
}
