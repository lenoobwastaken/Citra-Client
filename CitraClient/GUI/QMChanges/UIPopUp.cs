using System.Collections;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.DataModel.Core;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;

namespace CitraClient.GUI.QMChanges
{
	public static class UIPopUp
	{
		public static Transform InstMemoryDisplay;

		public static Sprite memoryDisplaySprite;

		private static TextMeshProUGUI _textComponent;

		private static TextMeshProUGUI _textComponent2;

		private static GameObject _fadeHelperObj;

		public static IEnumerator SetupMemoryDisplay()
		{
			GameObject qmBase = ExtendedQuickMenu.GetQmParent();
			memoryDisplaySprite = ImageUtils.PopupSprite;
			if (InstMemoryDisplay != null)
			{
				Object.Destroy(InstMemoryDisplay.gameObject);
			}
			InstMemoryDisplay = Object.Instantiate(qmBase.transform.Find("VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars"), ExtendedQuickMenu.GetLwMenu().transform);
			InstMemoryDisplay.name = "Citra_Memory_Display";
			InstMemoryDisplay.transform.Find("Icon").gameObject.SetActive(value: false);
			InstMemoryDisplay.transform.Find("Badge_MMJump").gameObject.SetActive(value: false);
			InstMemoryDisplay.transform.Find("Foreground").gameObject.SetActive(value: false);
			InstMemoryDisplay.GetComponentInChildren<Image>().sprite = memoryDisplaySprite;
			Object.Destroy(InstMemoryDisplay.GetComponent<CanvasGroup>());
			Object.Destroy(InstMemoryDisplay.GetComponent<BindingComponent>());
			Object.Destroy(InstMemoryDisplay.GetComponent<Button>());
			Object.Destroy(InstMemoryDisplay.GetComponent<StyleElement>());
			Object.Destroy(InstMemoryDisplay.transform.Find("Text_H4").gameObject.GetComponent<StyleElement>());
			InstMemoryDisplay.transform.Find("Text_H4").gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-390f, 900f);
			GameObject mdText = Object.Instantiate(ExtendedQuickMenu.GetQmBase().transform.Find("Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").gameObject, InstMemoryDisplay.transform);
			mdText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(350f, -190f, 0f);
			mdText.SetActive(value: true);
			_textComponent = mdText.GetComponent<TextMeshProUGUI>();
			_textComponent.richText = true;
			_textComponent.fontSize = 22f;
			_textComponent.text = Memory.Str10;
			_textComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(1600f, 100f);
			GameObject mdTextTwo = Object.Instantiate(ExtendedQuickMenu.GetQmBase().transform.Find("Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").gameObject, InstMemoryDisplay.transform);
			mdTextTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(80f, -200f, 0f);
			mdTextTwo.SetActive(value: true);
			_textComponent2 = mdTextTwo.GetComponent<TextMeshProUGUI>();
			_textComponent2.richText = true;
			_textComponent2.fontSize = 22f;
			_textComponent2.text = Memory.source;
			_textComponent2.GetComponent<RectTransform>().sizeDelta = new Vector2(1600f, 100f);
			MemoryDisplayCheck().Start();
			yield return null;
		}

		private static IEnumerator MemoryDisplayCheck()
		{
			while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null)
			{
				yield return null;
			}
			while (true)
			{
				InstMemoryDisplay.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(740f, 400f);
				InstMemoryDisplay.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(830f, 300f);
				_textComponent.text = Memory.Str10;
				_textComponent2.text = Memory.source;
				yield return new WaitForEndOfFrame();
			}
		}

		public static IEnumerator FadeInFadeOut(bool state)
		{
			Material mat = new Material(Shader.Find("VRChat/UI/Default"));
			GameObject leftWingMenu = ExtendedQuickMenu.GetLwMenu();
			_fadeHelperObj = leftWingMenu.transform.Find("Citra_Memory_Display/Background").gameObject;
			_fadeHelperObj.GetComponent<Image>().material = mat;
			if (state)
			{
				for (float index2 = 1f; index2 >= 0f; index2 -= Time.deltaTime)
				{
					_fadeHelperObj.GetComponent<Image>().material.color = new Color(1f, 1f, 1f, index2);
					_textComponent.color = new Color(1f, 1f, 1f, index2);
					_textComponent2.color = new Color(1f, 1f, 1f, index2);
					yield return null;
				}
			}
			else
			{
				for (float index = 0f; index <= 1f; index += Time.deltaTime)
				{
					_fadeHelperObj.GetComponent<Image>().material.color = new Color(1f, 1f, 1f, index);
					_textComponent.color = new Color(1f, 1f, 1f, index);
					_textComponent2.color = new Color(1f, 1f, 1f, index);
					yield return null;
				}
			}
		}
	}
}
