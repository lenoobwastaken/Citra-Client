using System;
using TMPro;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.CitraNamePlates
{
	public class Object : MonoBehaviour
	{
		public Graphic IconBackground;

		public RawImage Icon;

		public GameObject IconContainer;

		public GameObject Content;

		public GameObject QuickStats;

		public ImageThreeSlice NameBackground;

		public ImageThreeSlice QuickStatsBackground;

		public TextMeshProUGUI Name;

		public ImageThreeSlice NamePulse;

		public Image IconPulse;

		public ImageThreeSlice NameGlow;

		public Image IconGlow;

		public TextMeshProUGUI IconText;

		public bool IsDelusion;

		private bool Delusion;

		private Color NameColor;

		private Color NameColor2;

		private PlayerNameplate PN;

		private bool SetColor;

		public Object(IntPtr ptr)
			: base(ptr)
		{
		}

		[HideFromIl2Cpp]
		public void Reset()
		{
			SetColor = false;
			Delusion = false;
		}

		public void Update()
		{
			Name.color = NameColor;
			if (IsDelusion)
			{
				IconBackground.enabled = true;
				Icon.enabled = true;
				Icon.gameObject.SetActive(value: true);
				IconContainer.SetActive(value: true);
				IconContainer.SetActive(value: true);
			}
		}

		[HideFromIl2Cpp]
		public void SN(PlayerNameplate nameplate)
		{
			PN = nameplate;
		}

		[HideFromIl2Cpp]
		public void SNC(Color color)
		{
			NameColor = color;
			SetColor = true;
		}

		[HideFromIl2Cpp]
		public void ORbld()
		{
			if (PN != null)
			{
				Name.color = NameColor;
			}
		}
	}
}
