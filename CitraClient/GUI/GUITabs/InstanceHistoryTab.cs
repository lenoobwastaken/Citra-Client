using CitraClient.Modules.World;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.GUI.GUITabs
{
	public class InstanceHistoryTab
	{
		public static void Tab()
		{
			for (int i = 0; i < InstanceHistory.Instances.Count; i++)
			{
				if (UnityEngine.GUI.Button(new Rect(200f, 100 + 22 * (InstanceHistory.Instances.Count - 1 - i), 200f, 20f), "<b>" + InstanceHistory.Instances[i].Item1 + "</b>"))
				{
					Networking.GoToRoom(InstanceHistory.Instances[i].Item2);
				}
			}
		}
	}
}
