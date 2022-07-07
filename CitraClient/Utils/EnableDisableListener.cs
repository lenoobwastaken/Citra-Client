using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace CitraClient.Utils
{
	public class EnableDisableListener : MonoBehaviour
	{
		[method: HideFromIl2Cpp]
		public event Action? OnEnabled;

		[method: HideFromIl2Cpp]
		public event Action? OnDisabled;

		public EnableDisableListener(IntPtr obj0)
			: base(obj0)
		{
		}

		private void OnEnable()
		{
			this.OnEnabled?.Invoke();
		}

		private void OnDisable()
		{
			this.OnDisabled?.Invoke();
		}
	}
}
