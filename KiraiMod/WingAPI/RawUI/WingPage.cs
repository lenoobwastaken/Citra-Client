using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KiraiMod.WingAPI.RawUI
{
	public class WingPage
	{
		public Wing.BaseWing wing;

		public Transform transform;

		public TextMeshProUGUI text;

		public Button closeButton;

		public Button openButton;

		public WingPage(Wing.BaseWing wing, string name)
		{
			WingPage wingPage = this;
			this.wing = wing;
			this.transform = UnityEngine.Object.Instantiate(wing.ProfilePage, wing.WingPages);
			Transform transform = this.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
			this.transform.gameObject.SetActive(value: false);
			for (int i = 0; i < transform.GetChildCount(); i++)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
			this.transform.GetComponentInChildren<TextMeshProUGUI>().text = name;
			closeButton = this.transform.GetComponentInChildren<Button>();
			closeButton.onClick = new Button.ButtonClickedEvent();
			closeButton.onClick.AddListener((Action)delegate
			{
				wingPage.transform.gameObject.SetActive(value: false);
				wing.openedPages.RemoveAt(wing.openedPages.Count - 1);
				if (wing.openedPages.Count > 0)
				{
					WingPage wingPage3 = wing.openedPages[wing.openedPages.Count - 1];
					wingPage3.transform.gameObject.SetActive(value: true);
				}
				else
				{
					wing.WingMenu.gameObject.SetActive(value: true);
				}
			});
			Transform transform2 = UnityEngine.Object.Instantiate(wing.ProfileButton, wing.WingButtons);
			(text = transform2.GetComponentInChildren<TextMeshProUGUI>()).text = name;
			openButton = transform2.GetComponent<Button>();
			openButton.onClick = new Button.ButtonClickedEvent();
			openButton.onClick.AddListener((Action)delegate
			{
				wingPage.transform.gameObject.SetActive(value: true);
				wing.openedPages.Add(wingPage);
				if (wing.openedPages.Count > 1)
				{
					WingPage wingPage2 = wing.openedPages[wing.openedPages.Count - 2];
					wingPage2.transform.gameObject.SetActive(value: false);
				}
				else
				{
					wing.WingMenu.gameObject.SetActive(value: false);
				}
			});
		}

		public WingPage(WingPage page, string name, int index)
		{
			wing = page.wing;
			this.transform = UnityEngine.Object.Instantiate(wing.ProfilePage, wing.WingPages);
			Transform transform = this.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
			this.transform.gameObject.SetActive(value: false);
			for (int i = 0; i < transform.GetChildCount(); i++)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
			this.transform.GetComponentInChildren<TextMeshProUGUI>().text = name;
			closeButton = this.transform.GetComponentInChildren<Button>();
			closeButton.onClick = new Button.ButtonClickedEvent();
			closeButton.onClick.AddListener((Action)delegate
			{
				this.transform.gameObject.SetActive(value: false);
				wing.openedPages.RemoveAt(wing.openedPages.Count - 1);
				if (wing.openedPages.Count > 0)
				{
					WingPage wingPage2 = wing.openedPages[wing.openedPages.Count - 1];
					wingPage2.transform.gameObject.SetActive(value: true);
				}
				else
				{
					wing.WingMenu.gameObject.SetActive(value: true);
				}
			});
			Transform transform2 = UnityEngine.Object.Instantiate(wing.ProfileButton, page.transform);
			(text = transform2.GetComponentInChildren<TextMeshProUGUI>()).text = name;
			openButton = transform2.GetComponent<Button>();
			openButton.GetComponent<RectTransform>().sizeDelta = new Vector2(420f, 144f);
			openButton.transform.localPosition = new Vector3(0f, 320 - index * 120, this.transform.transform.localPosition.z);
			openButton.onClick = new Button.ButtonClickedEvent();
			openButton.onClick.AddListener((Action)delegate
			{
				this.transform.gameObject.SetActive(value: true);
				wing.openedPages.Add(this);
				if (wing.openedPages.Count > 1)
				{
					WingPage wingPage = wing.openedPages[wing.openedPages.Count - 2];
					wingPage.transform.gameObject.SetActive(value: false);
				}
				else
				{
					wing.WingMenu.gameObject.SetActive(value: false);
				}
			});
		}
	}
}
