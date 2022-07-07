using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.GUI.GUITabs
{
	public static class MainTab
	{
		private static int _yOffset;

		private static bool _state;

		public static void Tab()
		{
			_yOffset = 100;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Restart Rejoin</b>"))
			{
				ApplicationUtils.RestartRejoin();
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Cool Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(0);
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Happy Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(1);
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Pizza Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(2);
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Hoodie Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(3);
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Karen Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(4);
			}
			_yOffset += 30;
			if (UnityEngine.GUI.Button(new Rect(200f, _yOffset, 200f, 20f), "<b>Funny Cat</b>"))
			{
				ImageUtils.ChangeCatSprite(6);
			}
		}
	}
}
