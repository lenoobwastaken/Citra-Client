using CitraClient.API.QM;
using CitraClient.Config;
using TMPro;
using UnityEngine;
using VRC.UI.Core.Styles;

namespace CitraClient.GUI.QM.Playerlist
{
	public static class Playerlist
	{
		public static TextMeshProUGUI playersComponent;

		public static TextMeshProUGUI listComponent;

		public static QMInfo playerList;

		public static void Init()
		{
			playerList = new QMInfo("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/", new Vector2(-450f, 510f), new Vector2(755f, 915f), null, bgEnabled: false, "", wingMenu: true);
			GameObject gameObject = Object.Instantiate(APIStuff.GetTextBase(), playerList.gameObject.transform);
			Object.Destroy(gameObject.GetComponent<StyleElement>());
			gameObject.name = string.Format("{0}-QMInfoText-{1}", "CitraClient", APIStuff.RandomNumbers());
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f, -92f);
			playersComponent = gameObject.GetComponent<TextMeshProUGUI>();
			playersComponent.richText = true;
			playersComponent.text = "";
			playersComponent.alignment = TextAlignmentOptions.Baseline;
			playersComponent.fontSize = 35f;
			playersComponent.color = Color.magenta;
			playersComponent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-665f, 25f);
			playersComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(1600f, 100f);
			GameObject gameObject2 = Object.Instantiate(APIStuff.GetTextBase(), playerList.gameObject.transform);
			Object.Destroy(gameObject2.GetComponent<StyleElement>());
			gameObject2.name = string.Format("{0}-QMInfoText-{1}", "CitraClient", APIStuff.RandomNumbers());
			gameObject2.GetComponent<RectTransform>().anchoredPosition = new Vector2(20f, -22f);
			listComponent = gameObject2.GetComponent<TextMeshProUGUI>();
			listComponent.richText = true;
			listComponent.text = "";
			listComponent.verticalAlignment = VerticalAlignmentOptions.Baseline;
			listComponent.fontSize = 25f;
			listComponent.color = Color.magenta;
			listComponent.GetComponent<RectTransform>().anchoredPosition = new Vector2(15f, -90f);
			listComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(2000f, 2000f);
		}

		public static void TogglePlayerList()
		{
			if (Configuration.GetConfig().togglePlayerList)
			{
				playerList?.gameObject.SetActive(value: false);
			}
			else if (!Configuration.GetConfig().togglePlayerList)
			{
				playerList?.gameObject.SetActive(value: true);
			}
		}
	}
}
