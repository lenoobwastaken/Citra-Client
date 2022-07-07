using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Tooltips;

namespace CitraClient.API.QM
{
	public class QMSlider : QMButtonBase
	{
		public readonly GameObject gameObject;

		public readonly Slider sliderComponent;

		public readonly TextMeshProUGUI sliderPercentText;

		public readonly TextMeshProUGUI sliderText;

		public readonly VRC.UI.Elements.Tooltips.UiTooltip sliderTooltip;

		public QMSlider(Transform parent, string text, float x, float y, Action<float> onSliderAdjust, string tooltip, float minValue = 0f, float maxValue = 100f, float defaultValue = 50f)
		{
			button = UnityEngine.Object.Instantiate(APIStuff.SliderTemplate(), parent);
			gameObject = button;
			button.name = string.Format("{0}-{1}-{2}", "CitraClient", text, APIStuff.RandomNumbers());
			RectTransform component = button.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(x, y);
			button.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>(includeInactive: true).text = text;
			sliderPercentText = button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
			sliderComponent = button.GetComponentInChildren<Slider>();
			sliderComponent.onValueChanged = new Slider.SliderEvent();
			sliderComponent.minValue = minValue;
			sliderComponent.maxValue = maxValue;
			sliderComponent.onValueChanged.AddListener((Action<float>)delegate(float val)
			{
				sliderPercentText.text = Mathf.Floor(val).ToString();
				onSliderAdjust?.Invoke(val);
			});
			sliderComponent.Set(defaultValue, sendCallback: false);
			sliderComponent.value = defaultValue;
			ObjectHandler Handler = sliderPercentText.gameObject.AddComponent<ObjectHandler>();
			ObjectHandler objectHandler = Handler;
			objectHandler.OnEnabled = (Action<GameObject>)Delegate.Combine(objectHandler.OnEnabled, (Action<GameObject>)delegate
			{
				sliderPercentText.text = Mathf.Floor(defaultValue).ToString();
				UnityEngine.Object.Destroy(Handler);
			});
			gameObject.GetComponentInChildren<VRC.UI.Elements.Tooltips.UiTooltip>(includeInactive: true).field_Public_String_0 = tooltip;
		}

		public QMSlider(QMNestedButton pge, string text, float x, float y, Action<float> onSliderAdjust, string tooltip, float minValue = 0f, float maxValue = 100f, float defaultValue = 50f)
			: this(pge.GetMenuObject().transform, text, x, y, onSliderAdjust, tooltip, minValue, maxValue, defaultValue)
		{
		}

		public void SetAction(Action<float> newAction)
		{
			sliderComponent.onValueChanged = new Slider.SliderEvent();
			sliderComponent.onValueChanged.AddListener((Action<float>)delegate(float val)
			{
				sliderPercentText.text = Math.Floor(val).ToString();
				newAction(val);
			});
		}

		public void SetText(string newText)
		{
			sliderText.text = newText;
		}

		public void SetTooltip(string newTooltip)
		{
			sliderTooltip.field_Public_String_0 = newTooltip;
		}

		public void SetInteractable(bool val)
		{
			sliderComponent.interactable = val;
		}

		public void SetValue(float newValue, bool invoke = false)
		{
			sliderPercentText.text = Math.Floor(newValue).ToString();
			Slider.SliderEvent onValueChanged = sliderComponent.onValueChanged;
			sliderComponent.onValueChanged = new Slider.SliderEvent();
			sliderComponent.value = newValue;
			sliderComponent.onValueChanged = onValueChanged;
			if (invoke)
			{
				sliderComponent.onValueChanged.Invoke(newValue);
			}
		}
	}
}
