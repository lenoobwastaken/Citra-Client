using System.Linq;
using Il2CppSystem.Collections.Generic;
using VRC.Core;
using VRC.Management;

namespace CitraClient.Utils
{
	public class ModerationUtils
	{
		private static ModerationManager Instance => ModerationManager.prop_ModerationManager_0;

		public static bool GetIsBlocked(string userID)
		{
			return GetModerationExistsAgainstPlayer(ApiPlayerModeration.ModerationType.Block, userID);
		}

		private static bool GetModerationExistsAgainstPlayer(ApiPlayerModeration.ModerationType moderationType, string userID)
		{
			if (ModerationFromMe(moderationType) != null)
			{
				return ModerationFromMe(moderationType).ToArray().ToList().Exists((ApiPlayerModeration m) => m != null && m.moderationType == moderationType && m.targetUserId == userID);
			}
			return false;
		}

		private static List<ApiPlayerModeration> ModerationFromMe(ApiPlayerModeration.ModerationType type)
		{
			return Instance.Method_Public_List_1_ApiPlayerModeration_ModerationType_0(type);
		}
	}
}
