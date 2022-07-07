using System.Collections;
using UnityEngine;

namespace CitraClient.Utils
{
	public class CameraUtils
	{
		private static Camera _ourCamera = Camera.main;

		public static float FarClippingValue { get; set; }

		public static void FarClipping(bool state)
		{
			_ourCamera.farClipPlane = (state ? 99999f : 1000f);
		}

		public static IEnumerator AdjustCameraFov()
		{
			if (!(_ourCamera == null))
			{
				_ourCamera.farClipPlane = FarClippingValue;
				yield return null;
			}
		}
	}
}
