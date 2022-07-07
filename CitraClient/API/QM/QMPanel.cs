using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;

namespace CitraClient.API.QM
{
	public class QMPanel
	{
		public readonly RectTransform bgRectTransform;

		public readonly DebugInfoPanel debugInfoPanel;

		public readonly GameObject gameObject;

		public readonly TextMeshProUGUI panelText;

		public readonly GameObject textGameObject;

		public readonly RectTransform textRectTransform;

		public QMPanel(string text, Color textColor, float x, float y, float sizeX, float sizeY, float textX, float textY, Color? bgColor = null)
		{
			gameObject = Object.Instantiate(APIStuff.PanelTemplate(), APIStuff.PanelParentTemplate().transform);
			gameObject.name = string.Format("{0}-Panel-{1}", "CitraClient", APIStuff.RandomNumbers());
			textGameObject = gameObject.transform.Find("Panel/Text_FPS").gameObject;
			bgRectTransform = gameObject.transform.Find("Panel/Background").GetComponent<RectTransform>();
			textRectTransform = textGameObject.GetComponent<RectTransform>();
			textRectTransform.sizeDelta = new Vector2(999f, 0f);
			textRectTransform.anchoredPosition = new Vector2(textX, textY);
			bgRectTransform.anchoredPosition = new Vector2(x, y);
			bgRectTransform.sizeDelta = new Vector2(sizeX, sizeY);
			textGameObject.name = string.Format("{0}-PanelText-{1}-{2}", "CitraClient", text, APIStuff.RandomNumbers());
			debugInfoPanel = gameObject.GetComponent<DebugInfoPanel>();
			debugInfoPanel.field_Public_ListCountBinding_1 = null;
			Object.DestroyImmediate(textGameObject.GetComponent<TextBinding>());
			Object.Destroy(gameObject.transform.Find("Panel/Text_Ping"));
			Object.DestroyImmediate(debugInfoPanel.field_Public_ListCountBinding_0);
			Object.DestroyImmediate(textGameObject.GetComponent<StyleElement>());
			panelText = textGameObject.GetComponent<TextMeshProUGUI>();
			panelText.text = text;
			panelText.color = textColor;
			if (bgColor.HasValue)
			{
				debugInfoPanel.GetComponentInChildren<Image>().color = bgColor.Value;
			}
		}
	}
}
