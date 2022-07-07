using CitraClient.API.QM;
using CitraClient.Modules.World;
using VRC.SDKBase;

namespace CitraClient.GUI.QM
{
	public static class InstanceHistoryMenu
	{
		public static QMNestedButton instanceHistoryMenu;

		public static void InitInstanceHistoryMenu()
		{
			int x = 0;
			int y = 5;
			if (MainMenu.MainPage != null)
			{
				instanceHistoryMenu = new QMNestedButton(MainMenu.MainPage, "Instance History", 2f, 2f, "View instances you have joined. Press button to join.\nCredits go to Kirai for part of the code used.", "<color=#CE389C>Citra:</color> <color=#00d1ed>Instance History</color>");
			}
			QMScrollMenu instanceScrollMenu = new QMScrollMenu(instanceHistoryMenu, string.Empty, 0, 0, delegate
			{
			}, string.Empty, string.Empty);
			instanceScrollMenu.SetAction(delegate
			{
				for (int i = 0; i < InstanceHistory.Instances.Count; i++)
				{
					int index = i;
					instanceScrollMenu.Add(new QMSingleButton(instanceHistoryMenu, x, y, InstanceHistory.Instances[index].Item1, delegate
					{
						Networking.GoToRoom(InstanceHistory.Instances[index].Item2);
					}, "Press button to join instance: " + InstanceHistory.Instances[index].Item2 + "."));
				}
			});
			new QMSingleButton(instanceHistoryMenu, 4f, 3f, "Clear", delegate
			{
				InstanceHistory.ClearInstanceHistory();
			}, "Clear all current instances saved.", null, halfBtn: true);
		}
	}
}
