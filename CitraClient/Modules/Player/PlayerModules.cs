using CitraClient.Utils;
using CitraClient.Utils.UI;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.UI;

namespace CitraClient.Modules.Player
{
	public class PlayerModules
	{
		private static Quaternion _localQuaternion;

		private static NeckRange _nexkRange;

		public static void TeleportSelfToPlayer(VRC.Player player)
		{
			if (!(player == null))
			{
				PlayerUtils.GetLocalPlayer().transform.position = player.transform.position;
			}
		}

		public static void ForceCloneAvatar(VRC.Player player)
		{
			ApiAvatar apiAvatar = player.prop_ApiAvatar_0;
			if (apiAvatar.releaseStatus != "private")
			{
				Transform transform = GameObject.Find("UserInterface/MenuContent/Screens/")?.transform;
				if (transform != null)
				{
					PageAvatar pageAvatar = transform.Find("Avatar")?.GetComponent<PageAvatar>();
					if (pageAvatar != null)
					{
						pageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
						{
							id = apiAvatar.id
						};
						pageAvatar.ChangeToSelectedAvatar();
					}
				}
				ConsoleUtils.OnLogSuccess("Cloned Players avatar. (ID: " + apiAvatar.id + ")");
			}
			else
			{
				ConsoleUtils.OnLogError("Failed to clone Players Avatar. Most Likely due to it having private release status");
				MezLogger.HudError("Failed to clone Players Avatar. Most Likely due to it having private release status", 3f);
			}
		}

		public static void PortalToFriendsInstance(VRC.Player player)
		{
			string text = ((player != null) ? player.prop_APIUser_0 : null)?.instanceId;
			if (text != null)
			{
				WorldUtils.GoToRoom(text);
			}
		}

		public static void EnableDisableHeadFlipper(bool flipped)
		{
			VRC.Player player = PlayerUtils.LocalPlayer();
			if (flipped)
			{
				_nexkRange = player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
				player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0f);
			}
			else
			{
				player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = _nexkRange;
			}
		}
	}
}
