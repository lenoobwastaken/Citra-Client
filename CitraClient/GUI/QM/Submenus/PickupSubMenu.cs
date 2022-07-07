using CitraClient.API.QM;
using CitraClient.Utils;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.GUI.QM.Submenus
{
	public class PickupSubMenu
	{
		private static QMNestedButton _pickupMenu;

		public static void InitPickupSubMenu(VRC_Pickup pickup)
		{
			if (_pickupMenu != null)
			{
				Object.Destroy(_pickupMenu.GetMenuObject().gameObject);
				return;
			}
			_pickupMenu = new QMNestedButton(PickupMenu.pickupMenu, string.Empty, 1f, 1f, string.Empty, string.Empty);
			new QMSingleButton(_pickupMenu, 4f, 3.5f, "TakeOwnerShipPickup", delegate
			{
				PickupUtils.TakeOwnerShipPickup(pickup);
				ConsoleUtils.OnLogInfo("Button Pressed");
			}, "Movement Options");
		}
	}
}
