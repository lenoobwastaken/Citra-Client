using System;
using KiraiMod.WingAPI.RawUI;
using UnityEngine;

namespace KiraiMod.WingAPI
{
	public static class Extensions
	{
		public static WingPage CreatePage(this Wing.BaseWing page, string name)
		{
			return new WingPage(page, name);
		}

		public static WingPage CreateNestedPage(this WingPage page, string name, int index)
		{
			return new WingPage(page, name, index);
		}

		public static WingButton CreateButton(this WingPage page, string name, int index, Action onClick)
		{
			return new WingButton(page, name, index, onClick);
		}

		public static WingToggle CreateToggle(this WingPage page, string name, int index, Color on, Color off, bool initial, Action<bool> onClick)
		{
			return new WingToggle(page, name, index, on, off, initial, onClick);
		}
	}
}
