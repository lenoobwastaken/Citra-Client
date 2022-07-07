using System.Windows.Forms;
using CitraClient.Utils;
using VRC.Core;
using VRC.SDKBase;

namespace CitraClient.Modules.World
{
	public static class WorldModules
	{
		private static VRC_EventHandler.VrcEvent _vrcEvent;

		private static VRC_Trigger _vrc_Trigger;

		public static void CopyInstanceToClipboard()
		{
			ApiWorldInstance field_Internal_Static_ApiWorldInstance_ = RoomManager.field_Internal_Static_ApiWorldInstance_0;
			Clipboard.SetText(field_Internal_Static_ApiWorldInstance_.id);
			ConsoleUtils.OnLogInfo("Copied \"" + field_Internal_Static_ApiWorldInstance_.id + "\".");
		}

		public static void JoinInstanceFromClipboard()
		{
			string text = Clipboard.GetText();
			if (string.IsNullOrEmpty(text))
			{
				ConsoleUtils.OnLogError("Clipboard is currently empty.");
			}
			else
			{
				WorldUtils.GoToRoom(text);
			}
		}
	}
}
