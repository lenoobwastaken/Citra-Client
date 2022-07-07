using CitraClient.API.QM;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.API.SM
{
	public class SMLabel
	{
		protected GameObject gameObject;

		protected RectTransform labelRect;

		protected Text labelText;

		public SMLabel(string text, float posX, float posY, Color? textColor = null)
		{
			gameObject = Object.Instantiate(SMAPI.GetLabelBase(), SMAPI.GetSM().transform);
			gameObject.name = $"Citra-SMLabel-{APIStuff.RandomNumbers()}";
			labelText = gameObject.GetComponent<Text>();
			labelRect = gameObject.GetComponent<RectTransform>();
			SetLabelText(text);
			SetPosition(posX, posY);
			if (textColor.HasValue)
			{
				SetLabelColor(textColor.Value);
			}
		}

		public void SetLabelText(string text)
		{
			labelText.text = text;
		}

		public void SetLabelColor(Color textColor)
		{
			labelText.color = textColor;
		}

		public void SetPosition(float posX, float posY)
		{
			labelRect.anchoredPosition = new Vector2(posX, posY);
		}
	}
}
