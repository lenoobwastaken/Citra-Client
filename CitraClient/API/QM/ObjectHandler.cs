using System;
using MelonLoader;
using UnityEngine;

namespace CitraClient.API.QM
{
	[RegisterTypeInIl2Cpp]
	public class ObjectHandler : MonoBehaviour
	{
		private bool IsEnabled;

		public Action<GameObject> OnDestroyed = null;

		public Action<GameObject> OnDisabled = null;

		public Action<GameObject> OnEnabled;

		public Action<GameObject, bool> OnUpdate = null;

		public Action<GameObject, bool> OnUpdateEachSecond = null;

		private float UpdateDelay;

		public ObjectHandler(IntPtr instance)
			: base(instance)
		{
		}

		private void Update()
		{
			OnUpdate?.Invoke(base.gameObject, IsEnabled);
			if (UpdateDelay < Time.time)
			{
				UpdateDelay = Time.time + 1f;
				OnUpdateEachSecond?.Invoke(base.gameObject, IsEnabled);
			}
		}

		private void OnEnable()
		{
			OnEnabled?.Invoke(base.gameObject);
			IsEnabled = true;
		}

		private void OnDisable()
		{
			OnDisabled?.Invoke(base.gameObject);
			IsEnabled = false;
		}

		private void OnDestroy()
		{
			OnDestroyed?.Invoke(base.gameObject);
		}
	}
}
