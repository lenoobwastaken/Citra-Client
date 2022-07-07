using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.Utils.UI
{
	public static class MezLogger
	{
		private static readonly string _clientName = "CitraClient";

		private static readonly string _primaryColour = "#6A329F";

		private static readonly string _secondaryColour = "#a1dcff";

		private static readonly Vector3 _uiPosition = new Vector3(-20f, -300f, 0f);

		private static readonly float _textSpacing = 25f;

		public static IEnumerator MakeUI()
		{
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return new WaitForSeconds(1f);
			}
			GameObject gameObject;
			GameObject gui = (gameObject = GameObject.Find("/UserInterface").transform.Find("UnscaledUI/HudContent_Old/Hud/AlertTextParent/Capsule").gameObject);
			gameObject.SetActive(value: true);
			GameObject text = gui.transform.Find("Text").gameObject;
			yield return new WaitForEndOfFrame();
			gui.transform.localPosition = _uiPosition;
			Object.DestroyImmediate(gui.transform.GetComponent<HorizontalLayoutGroup>());
			Object.DestroyImmediate(gui.transform.GetComponent<ContentSizeFitter>());
			Object.DestroyImmediate(gui.transform.GetComponent<ContentSizeFitter>());
			Object.DestroyImmediate(gui.transform.GetComponent<ImageThreeSlice>());
			gui.gameObject.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			gui.gameObject.AddComponent<VerticalLayoutGroup>().spacing = _textSpacing;
			TextMeshProUGUI textMesh = text.GetComponent<TextMeshProUGUI>();
			textMesh.alignment = TextAlignmentOptions.Left;
			textMesh.text = "<color=" + _primaryColour + ">[" + _clientName + "]</color> ";
			yield return new WaitForEndOfFrame();
			text.SetActive(value: false);
		}

		public static void HudMsg(string text, float timer)
		{
			MezText(text, 1, timer).Start();
		}

		public static void HudError(string text, float timer)
		{
			MezText(text, 2, timer).Start();
		}

		public static void HudWarn(string text, float timer)
		{
			MezText(text, 3, timer).Start();
		}

		public static void HudServer(string text, float timer)
		{
			MezText(text, 4, timer).Start();
		}

		public static void HudFriend(string text, float timer)
		{
			MezText(text, 5, timer).Start();
		}

		private static IEnumerator MezText(string text, int textType, float timeBeforeDeletion)
		{
			TextMeshProUGUI textMesh;
			try
			{
				GameObject textObj = Object.Instantiate(GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/AlertTextParent/Capsule/Text"), GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/AlertTextParent/Capsule").transform);
				textMesh = textObj.GetComponent<TextMeshProUGUI>();
				FadeInFadeOut(textMesh, state: false).Start();
				TextMeshProUGUI textMeshProUGUI = textMesh;
				TextMeshProUGUI textMeshProUGUI2 = textMeshProUGUI;
				string text2 = textMeshProUGUI.text;
				if (1 == 0)
				{
				}
				string text3 = textType switch
				{
					1 => "", 
					2 => "<color=red>[ERROR]</color> ", 
					3 => "<color=yellow>[WARNING]</color> ", 
					4 => "<color=green>[SERVER]</color> ", 
					5 => "<color=black>[FRIEND]</color> ", 
					_ => "Something broke whoops", 
				};
				if (1 == 0)
				{
				}
				textMeshProUGUI2.text = text2 + text3 + "<color=" + _secondaryColour + ">" + text + "</color>";
				textObj.SetActive(value: true);
			}
			catch
			{
				yield break;
			}
			yield return new WaitForSeconds(timeBeforeDeletion);
			FadeInFadeOut(textMesh, state: true).Start();
		}

		private static IEnumerator FadeInFadeOut(TextMeshProUGUI textMesh, bool state)
		{
			if (state)
			{
				for (float index2 = 1f; index2 >= 0f; index2 -= Time.deltaTime)
				{
					if (textMesh != null)
					{
						textMesh.color = new Color(1f, 1f, 1f, index2);
					}
					yield return null;
				}
				Object.Destroy(textMesh.gameObject);
				yield break;
			}
			for (float index = 0f; index <= 1f; index += Time.deltaTime)
			{
				if (textMesh != null)
				{
					textMesh.color = new Color(1f, 1f, 1f, index);
				}
				yield return null;
			}
		}
	}
}
