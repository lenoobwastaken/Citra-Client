using System;
using System.Collections;
using System.Globalization;
using MelonLoader;
using UnityEngine;

namespace CitraClient.Utils
{
	public static class GeneralUtils
	{
		public static Color HexToColor(string hexColor)
		{
			if (hexColor.IndexOf('#') != -1)
			{
				hexColor = hexColor.Replace("#", "");
			}
			float r = (float)int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier) / 255f;
			float g = (float)int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255f;
			float b = (float)int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255f;
			return new Color(r, g, b);
		}

		public static Color RGBAToColor(int R, int G, int B, int A)
		{
			return new Color(R / 255, G / 255, B / 255, A / 255);
		}

		public static string GetFPSColor(int fps)
		{
			if (fps < 20)
			{
				return "red";
			}
			if (fps < 50)
			{
				return "#ff2600";
			}
			if (fps < 75)
			{
				return "#ff6a00";
			}
			if (fps > 75)
			{
				return "green";
			}
			return "#ff0048";
		}

		public static string GetPingColor(int ping)
		{
			if (ping > 200)
			{
				return "red";
			}
			if (ping > 150)
			{
				return "#ff2600";
			}
			if (ping > 100)
			{
				return "#ff6a00";
			}
			if (ping > 75)
			{
				return "#ebb434";
			}
			if (ping < 75)
			{
				return "green";
			}
			return "#ff0048";
		}

		public static void Delay(float seconds, Action action)
		{
			MelonCoroutines.Start(DelayAction(seconds, action));
		}

		public static IEnumerator DelayAction(float seconds, Action action)
		{
			yield return new WaitForSeconds(seconds);
			action();
		}

		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if ((UnityEngine.Object)component == (UnityEngine.Object)null)
			{
				return gameObject.AddComponent<T>();
			}
			return component;
		}
	}
}
