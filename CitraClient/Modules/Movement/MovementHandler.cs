using CitraClient.Modules.Base;
using CitraClient.Utils;
using UnhollowerRuntimeLib;
using VRC;

namespace CitraClient.Modules.Movement
{
	public class MovementHandler : ModuleBase
	{
		public override void Start()
		{
			ClassInjector.RegisterTypeInIl2Cpp<MovementComponent>();
		}

		public override void VRCJoin(VRC.Player player)
		{
			if (player.prop_VRCPlayer_0 == PlayerUtils.GetLocalPlayer())
			{
				player.gameObject.AddComponent<MovementComponent>();
			}
		}
	}
}
