using VRC.Core;
using VRC.SDKBase;

namespace CitraClient.Utils
{
	public static class WorldUtils
	{
		public static ApiWorld GetWorld()
		{
			return RoomManager.field_Internal_Static_ApiWorld_0;
		}

		public static ApiWorldInstance GetWorldInstance()
		{
			return RoomManager.field_Internal_Static_ApiWorldInstance_0;
		}

		public static bool IsInWorld()
		{
			if (GetWorld() == null)
			{
				return GetWorldInstance() != null;
			}
			return true;
		}

		public static void GoToRoom(string id)
		{
			Networking.GoToRoom(id);
		}
	}
}
