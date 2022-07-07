using CitraClient.Modules.Base;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;

namespace CitraClient.Modules.ESP
{
	internal class CapsuleESP : ModuleBase
	{
		internal static void PlayerMeshEsp(VRC.Player player, bool state)
		{
			try
			{
				Il2CppArrayBase<Renderer> componentsInChildren = player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>();
				foreach (Renderer item in componentsInChildren)
				{
					HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(item, state);
				}
			}
			catch
			{
			}
		}

		internal static void HighlightPlayer(VRC.Player player, bool state)
		{
			HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(player.transform.Find("SelectRegion").GetComponent<Renderer>(), state);
		}

		internal static void PlayerCapsuleEsp(VRC.Player player, bool state)
		{
			try
			{
				Renderer component = player.gameObject.transform.Find("SelectRegion").GetComponent<Renderer>();
				HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(component, state);
			}
			catch
			{
			}
		}

		public override void VRCJoin(VRC.Player player)
		{
			HighlightPlayer(player, state: true);
		}
	}
}
