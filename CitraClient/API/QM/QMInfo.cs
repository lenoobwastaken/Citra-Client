using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;

namespace CitraClient.API.QM
{
	public class QMInfo
	{
		public readonly QMSingleButton baseButton;

		public readonly GameObject gameObject;

		public readonly RectTransform rectTransform;

		public QMInfo(string btnMenu, Vector2 position, Vector2 sizeDelta, Sprite sprite = null, bool bgEnabled = true, string buttonText = "", bool wingMenu = false)
		{
			baseButton = new QMSingleButton(btnMenu, 0f, 0f, buttonText, delegate
			{
			}, "", null, halfBtn: false, null, wingMenu);
			gameObject = baseButton.GetGameObject();
			gameObject.name = string.Format("{0}-QMInfo-{1}", "CitraClient", APIStuff.RandomNumbers());
			Object.Destroy(gameObject.transform.Find("Icon").gameObject);
			Object.Destroy(gameObject.transform.Find("Badge_MMJump").gameObject);
			Object.Destroy(gameObject.GetComponent<Button>());
			Object.Destroy(gameObject.GetComponent<UiTooltip>());
			Object.Destroy(gameObject.GetComponent<StyleElement>());
			if (sprite != null && bgEnabled)
			{
				gameObject.GetComponentInChildren<Image>().sprite = sprite;
			}
			if (!bgEnabled)
			{
				gameObject.GetComponentInChildren<Image>().enabled = false;
			}
			rectTransform = gameObject.GetComponent<RectTransform>();
			rectTransform.anchoredPosition = position;
			rectTransform.sizeDelta = sizeDelta;
			Object.Destroy(gameObject.transform.Find("Text_H4").gameObject);
		}

		public QMInfo(GameObject menu, Vector2 position, Vector2 sizeDelta, Sprite sprite = null, bool bgEnabled = true, string buttonText = "")
			: this(menu.name, position, sizeDelta, sprite = null, bgEnabled, buttonText)
		{
		}

		public QMInfo(QMNestedButton btnMenu, Vector2 position, Vector2 sizeDelta, Sprite sprite = null, bool bgEnabled = true, string buttonText = "")
			: this(btnMenu.GetMenuName(), position, sizeDelta, sprite = null, bgEnabled, buttonText)
		{
		}
	}
}
