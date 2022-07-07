using UnityEngine;

namespace CitraClient.Modules.World
{
	public static class Util
	{
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			return ((Object)component == (Object)null) ? gameObject.AddComponent<T>() : component;
		}
	}
}
