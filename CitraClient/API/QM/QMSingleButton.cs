using System;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CitraClient.API.QM
{
	public class QMSingleButton : QMButtonBase
	{
		public QMSingleButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, Color? btnTextColor = null, bool halfBtn = false, Sprite icon = null, bool wingMenu = false)
		{
			btnQMLoc = btnMenu.GetMenuName();
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnTextColor, icon, wingMenu);
			if (halfBtn)
			{
				button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
				button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0f, 22f);
			}
		}

		public QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, Color? btnTextColor = null, bool halfBtn = false, Sprite icon = null, bool wingMenu = false)
		{
			btnQMLoc = btnMenu;
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnTextColor, icon, wingMenu);
			if (halfBtn)
			{
				button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
				button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0f, 22f);
			}
		}

		private protected void InitButton(float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, Color? btnTextColor = null, Sprite icon = null, bool wingMenu = false)
		{
			btnType = "SingleButton";
			if (!wingMenu)
			{
				button = UnityEngine.Object.Instantiate(APIStuff.SingleButtonTemplate(), APIStuff.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/" + btnQMLoc).transform, worldPositionStays: true);
			}
			else
			{
				button = UnityEngine.Object.Instantiate(APIStuff.SingleButtonTemplate(), APIStuff.FindInactive(btnQMLoc).transform);
			}
			RandomNumb = APIStuff.RandomNumbers();
			button.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30f;
			button.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 176f);
			button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68f, 796f);
			if (icon == null)
			{
				button.transform.Find("Icon").GetComponentInChildren<Image>().gameObject.SetActive(value: false);
				button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition += new Vector2(0f, 50f);
				button.transform.Find("Background").GetComponent<Image>().sprite = ImageUtils.CitraButtonSprite;
				button.transform.Find("Background").GetComponent<Image>().overrideSprite = ImageUtils.CitraButtonSprite;
			}
			else
			{
				button.transform.Find("Icon").GetComponentInChildren<Image>().sprite = icon;
			}
			initShift[0] = 0;
			initShift[1] = 0;
			SetLocation(btnXLocation, btnYLocation);
			SetButtonText(btnText);
			SetToolTip(btnToolTip);
			SetAction(btnAction);
			if (btnTextColor.HasValue)
			{
				SetTextColor(btnTextColor.Value);
			}
			else
			{
				SetTextColor(UIColors.textColor);
			}
			SetTooltipColor("#00d1ed");
			SetActive(state: true);
			CitraQMAPI.allQMSingleButtons.Add(this);
		}

		public void SetBackgroundImage(Sprite newImg)
		{
			button.transform.Find("Background").GetComponent<Image>().sprite = newImg;
			button.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
		}

		public void SetCitraBackgroundImage()
		{
			button.transform.Find("Background").GetComponent<Image>().sprite = ImageUtils.CitraButtonSprite;
			button.transform.Find("Background").GetComponent<Image>().overrideSprite = ImageUtils.CitraButtonSprite;
		}

		public void SetButtonText(string buttonText)
		{
			button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
		}

		public void SetAction(Action buttonAction)
		{
			button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			if (buttonAction != null)
			{
				button.GetComponent<Button>().onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
			}
		}

		public void ClickMe()
		{
			button.GetComponent<Button>().onClick.Invoke();
		}
	}
}
