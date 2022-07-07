using System.IO;
using CitraClient.Modules.Base;
using CitraClient.Utils.Managers;
using Newtonsoft.Json;
using VRC;
using VRC.Core;

namespace CitraClient.Modules.Logger
{
	public class LoggerHandler : ModuleBase
	{
		public override void Start()
		{
			string[] array = File.ReadAllLines(FileManager.AviLogFile);
			foreach (string item in array)
			{
				LoggerComponent.AvatarLogs.Add(item);
			}
			string[] array2 = File.ReadAllLines(FileManager.PlayerLogFile);
			foreach (string item2 in array2)
			{
				LoggerComponent.PlayerLogs.Add(item2);
			}
		}

		public override void VRCJoin(VRC.Player player)
		{
			if (player != null)
			{
				ApiAvatar apiAvatar = player.prop_ApiAvatar_0;
				APIUser aPIUser = player.prop_APIUser_0;
				if (!File.ReadAllText(FileManager.AviLogFile).Contains(apiAvatar.id))
				{
					AvatarObj value = new AvatarObj
					{
						Name = apiAvatar.name,
						ID = apiAvatar.id,
						AssetURL = apiAvatar.assetUrl,
						ImageURL = apiAvatar.thumbnailImageUrl,
						AuthorName = apiAvatar.authorName,
						ReleaseStatus = apiAvatar.releaseStatus,
						Platform = apiAvatar.platform
					};
					LoggerComponent.LogAvatar(JsonConvert.SerializeObject(value, Formatting.Indented) + ",".TrimEnd(','));
				}
				if (!File.ReadAllText(FileManager.PlayerLogFile).Contains(aPIUser.id))
				{
					PlayerObj value2 = new PlayerObj
					{
						DisplayName = aPIUser.displayName,
						UserID = aPIUser.id,
						ModerationPowers = aPIUser.hasModerationPowers.ToString(),
						IsFriend = aPIUser.isFriend.ToString()
					};
					LoggerComponent.LogPlayer(JsonConvert.SerializeObject(value2, Formatting.Indented) + ",".TrimEnd(','));
				}
			}
		}
	}
}
