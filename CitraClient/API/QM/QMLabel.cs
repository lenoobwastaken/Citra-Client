using TMPro;
using UnityEngine;

namespace CitraClient.API.QM
{
	public class QMLabel
	{
		public readonly GameObject gameObject;

		public readonly TextMeshProUGUI labelText;

		public QMLabel(Transform parent, string text, float x, float y, Color? textColor = null)
		{
			gameObject = Object.Instantiate(APIStuff.LabelTemplate(), parent);
			labelText = gameObject.GetComponent<TextMeshProUGUI>();
			labelText.text = text;
			if (textColor.HasValue)
			{
				labelText.color = textColor.Value;
			}
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
		}

		public QMLabel(QMNestedButton pge, string text, float x, float y, Color? textColor = null)
			: this(pge.GetMenuObject().transform, text, x, y, textColor)
		{
		}
	}
}
