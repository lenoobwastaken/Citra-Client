using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.Modules.Misc.TitleChanger
{
	public static class TitleHandler
	{
		[DllImport("user32.dll")]
		private static extern bool SetWindowText(IntPtr hWnd, string text);

		public static void ChangeWindowTitle(string text)
		{
			SetWindowText(Process.GetCurrentProcess().MainWindowHandle, text);
		}

		public static void FetchTitle()
		{
			System.Random random = new System.Random();
			Pattern[] array = new Pattern[4]
			{
				Patterns.PatternOne,
				Patterns.PatternTwo,
				Patterns.PatternThree,
				Patterns.PatternFour
			};
			int num = random.Next(array.Length);
			Pattern pattern = array[num];
			AnimatedTitle(pattern.PatternArray, pattern.WaitTime).Start();
		}

		private static IEnumerator AnimatedTitle(string[] titleArray, float waitForSeconds)
		{
			if (titleArray.Length == 0)
			{
				throw new ArgumentException("Value cannot be an empty collection.", "titleArray");
			}
			while (true)
			{
				foreach (string s in titleArray)
				{
					if (s == null)
					{
						yield break;
					}
					ChangeWindowTitle(s);
					yield return new WaitForSeconds(waitForSeconds);
				}
				yield return null;
			}
		}
	}
}
