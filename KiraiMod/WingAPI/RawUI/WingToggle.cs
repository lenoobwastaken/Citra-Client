using System;
using UnityEngine;

namespace KiraiMod.WingAPI.RawUI
{
	public class WingToggle
	{
		private bool state;

		private readonly Action<bool> onClick;

		public Wing.BaseWing wing;

		public WingButton button;

		public Color on;

		public Color off;

		public bool State
		{
			get
			{
				return state;
			}
			set
			{
				if (state != value)
				{
					button.text.color = (value ? on : off);
					onClick(state = value);
				}
			}
		}

		public WingToggle(Wing.BaseWing wing, string name, Transform parent, int pos, Color on, Color off, bool initial, Action<bool> onClick)
		{
			this.wing = wing;
			this.on = on;
			this.off = off;
			this.onClick = onClick;
			button = new WingButton(wing, name, parent, pos, delegate
			{
				State = !State;
			});
			button.text.color = (initial ? on : off);
		}

		public WingToggle(WingPage page, string name, int index, Color on, Color off, bool initial, Action<bool> onClick)
		{
			wing = page.wing;
			this.on = on;
			this.off = off;
			this.onClick = onClick;
			button = new WingButton(page, name, index, delegate
			{
				State = !State;
			});
			button.text.color = (initial ? on : off);
		}
	}
}
