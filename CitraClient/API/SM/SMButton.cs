using System;
using CitraClient.API.QM;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.API.SM
{
	public class SMButton
	{
		protected Button btnComponent;

		protected RectTransform btnRect;

		protected Text btnText;

		protected GameObject gameObject;

		public SMButton(string text, float posX, float posY, Action action, Color? btnColor = null, Color? textColor = null)
		{
			gameObject = UnityEngine.Object.Instantiate(SMAPI.GetButtonBase(), SMAPI.GetSM().transform);
			gameObject.name = $"Citra-SMButton-{APIStuff.RandomNumbers()}";
			btnText = gameObject.GetComponentInChildren<Text>();
			btnComponent = gameObject.GetComponent<Button>();
			btnRect = gameObject.GetComponent<RectTransform>();
			UnityEngine.Object.Destroy(gameObject.GetComponent<LayoutElement>());
			SetAction(action);
			SetPosition(posX, posY);
			SetText(text);
			if (btnColor.HasValue)
			{
				SetButtonColor(btnColor.Value);
			}
			if (textColor.HasValue)
			{
				SetButtonColor(textColor.Value);
			}
		}

		public void SetAction(Action action)
		{
			btnComponent.onClick = new Button.ButtonClickedEvent();
			btnComponent.onClick.AddListener(action);
		}

		public void SetPosition(float posX, float posY)
		{
			btnRect.anchoredPosition = new Vector2(posX, posY);
		}

		public void SetButtonColor(Color color)
		{
			gameObject.GetComponentInChildren<Image>().color = color;
		}

		public void SetTextColor(Color color)
		{
			btnText.color = color;
		}

		public void SetText(string text)
		{
			btnText.text = text;
		}

		public void SetInteractable(bool b)
		{
			btnComponent.interactable = b;
		}
	}
}
