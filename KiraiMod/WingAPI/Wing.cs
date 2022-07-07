using System.Collections.Generic;
using KiraiMod.WingAPI.RawUI;
using UnityEngine;

namespace KiraiMod.WingAPI
{
	public static class Wing
	{
		public static class Misc
		{
			public static Transform UserInterface;

			public static Transform QuickMenu;
		}

		public class BaseWing
		{
			public List<WingPage> openedPages = new List<WingPage>();

			public Transform Wing;

			public Transform WingOpen;

			public Transform WingPages;

			public Transform WingMenu;

			public Transform WingButtons;

			public Transform ProfilePage;

			public Transform ProfileButton;

			internal void Setup(Transform wing)
			{
				Wing = wing;
				WingOpen = wing.Find("Button");
				WingPages = wing.Find("Container/InnerContainer");
				WingMenu = WingPages.Find("WingMenu");
				WingButtons = WingPages.Find("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
				ProfilePage = WingPages.Find("Profile");
				ProfileButton = WingButtons.Find("Button_Profile");
			}
		}

		public static BaseWing Left = new BaseWing();

		public static BaseWing Right = new BaseWing();
	}
}
