using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KiraiMod.WingAPI.RawUI
{
	public class WingButton
	{
		public Wing.BaseWing wing;

		public TextMeshProUGUI text;

		public Transform transform;

		public WingButton(Wing.BaseWing wing, string name, Transform parent, int pos, Action onClick)
		{
			this.wing = wing;
			transform = UnityEngine.Object.Instantiate(wing.ProfileButton, parent);
			transform.GetComponent<RectTransform>().sizeDelta = new Vector2(420f, 144f);
			transform.transform.localPosition = new Vector3(0f, pos, transform.transform.localPosition.z);
			(text = transform.GetComponentInChildren<TextMeshProUGUI>()).text = name;
			Button component = transform.GetComponent<Button>();
			component.onClick = new Button.ButtonClickedEvent();
			component.onClick.AddListener(onClick);
		}

		public WingButton(WingPage page, string name, int index, Action onClick)
		{
			wing = page.wing;
			Transform transform = UnityEngine.Object.Instantiate(wing.ProfileButton, page.transform);
			transform.GetComponent<RectTransform>().sizeDelta = new Vector2(420f, 144f);
			transform.transform.localPosition = new Vector3(0f, 320 - index * 120, transform.transform.localPosition.z);
			(text = transform.GetComponentInChildren<TextMeshProUGUI>()).text = name;
			Button component = transform.GetComponent<Button>();
			component.onClick = new Button.ButtonClickedEvent();
			component.onClick.AddListener(onClick);
		}
	}
}
