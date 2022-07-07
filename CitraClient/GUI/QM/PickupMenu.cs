using CitraClient.API.QM;
using CitraClient.Utils;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.GUI.QM
{
	public class PickupMenu
	{
		public static QMNestedButton pickupMenu;

		public static void InitPickupMenu()
		{
			int x = 0;
			int y = 5;
			pickupMenu = new QMNestedButton(MainMenu.MainPage, "Pickup Viewer", 1f, 3f, "View all Pickups in the world.", "<color=#CE389C>Citra:</color> <color=#00d1ed>Pickup Viewer</color>");
			QMScrollMenu pickupScroll = new QMScrollMenu(pickupMenu, string.Empty, 1, 0, delegate
			{
			}, string.Empty, string.Empty);
			pickupScroll.SetAction(delegate
			{
				Il2CppArrayBase<VRC_Pickup> il2CppArrayBase = Object.FindObjectsOfType<VRC_Pickup>();
				for (int i = 0; i < il2CppArrayBase.Count; i++)
				{
					VRC_Pickup pickup = il2CppArrayBase[i];
					pickupScroll.Add(new QMSingleButton(pickupMenu, x, y, pickup.name, delegate
					{
						VRCPlayer localPlayer = PlayerUtils.GetLocalPlayer();
						float y2 = localPlayer.field_Private_VRCPlayerApi_0.GetBonePosition(HumanBodyBones.Head).y;
						PickupUtils.TakeOwnerShipPickup(pickup);
						Transform transform = localPlayer.transform;
						Vector3 position = transform.position + transform.forward + Vector3.up * y2;
						pickup.transform.position = position;
					}, "Teleport " + pickup.name + " to self"));
					GameObject gameObject = new GameObject();
					gameObject.name = "Pickup";
					gameObject.transform.position = new Vector3(0f, 0f, 0f);
					gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					GameObject gameObject2 = gameObject;
				}
			});
		}
	}
}
