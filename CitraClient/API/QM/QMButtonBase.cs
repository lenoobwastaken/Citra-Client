using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Tooltips;

namespace CitraClient.API.QM
{
	public class QMButtonBase
	{
		protected string btnQMLoc;

		protected string btnTag;

		protected string btnType;

		protected GameObject button;

		protected int[] initShift = new int[2];

		protected Color OrigBackground;

		protected Color OrigText;

		protected int RandomNumb;

		public GameObject GetGameObject()
		{
			return button;
		}

		public void SetActive(bool state)
		{
			button.gameObject.SetActive(state);
		}

		public void SetLocation(float buttonXLoc, float buttonYLoc)
		{
			button.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (232f * (buttonXLoc + (float)initShift[0]));
			button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (210f * (buttonYLoc + (float)initShift[1]));
			btnTag = "(" + buttonXLoc + "," + buttonYLoc + ")";
			button.GetComponent<Button>().name = string.Format("{0}-{1}{2}-{3}", "CitraClient", btnType, btnTag, RandomNumb);
		}

		public void SetToolTip(string buttonToolTip)
		{
			button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = buttonToolTip;
			button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = buttonToolTip;
		}

		public void DestroyMe()
		{
			try
			{
				Object.Destroy(button);
			}
			catch
			{
			}
		}

		public void SetTextColor(Color buttonTextColor)
		{
			Object.Destroy(button.transform.Find("Text_H4").GetComponent<StyleElement>());
			button.transform.Find("Text_H4").GetComponent<TextMeshProUGUI>().color = buttonTextColor;
		}

		public void SetTooltipColor(string tooltipColor)
		{
			string field_Public_String_ = button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0;
			string field_Public_String_2 = button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1;
			button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = "<color=" + tooltipColor + ">" + field_Public_String_ + "</color>";
			button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = "<color=" + tooltipColor + ">" + field_Public_String_2 + "</color>";
		}
	}
}
