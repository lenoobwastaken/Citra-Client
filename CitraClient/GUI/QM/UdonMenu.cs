using System.Collections;
using CitraClient.API.QM;
using CitraClient.GUI.QM.Console;
using CitraClient.Utils;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.Udon;

namespace CitraClient.GUI.QM
{
	public class UdonMenu
	{
		public static QMNestedButton udonMenu;

		public static void InitUdonMenu()
		{
			int x = 0;
			int y = 5;
			udonMenu = new QMNestedButton(MainMenu.MainPage, "Udon Viewer", 2f, 3f, "View all udon behaviour in the world.", "<color=#CE389C>Citra:</color> <color=#00d1ed>Udon Viewer</color>");
			QMScrollMenu udonScroll = new QMScrollMenu(udonMenu, string.Empty, 1, 0, delegate
			{
			}, string.Empty, string.Empty);
			udonScroll.SetAction(delegate
			{
				Il2CppArrayBase<UdonBehaviour> il2CppArrayBase = Object.FindObjectsOfType<UdonBehaviour>();
				for (int i = 0; i < il2CppArrayBase.Count; i++)
				{
					UdonBehaviour udonBehaviour2 = il2CppArrayBase[i];
					udonScroll.Add(new QMSingleButton(udonMenu, x, y, udonBehaviour2.name, delegate
					{
						udonBehaviour2.Interact();
					}, "Interact with " + udonBehaviour2.name));
				}
			});
			new QMSingleButton(udonMenu, 4f, 0.12f, "Nuke Udon", delegate
			{
				int count = 0;
				NukeUdon().Start();
				IEnumerator NukeUdon()
				{
					Il2CppArrayBase<UdonBehaviour> type = Object.FindObjectsOfType<UdonBehaviour>();
					for (int index = 0; index < type.Count; index++)
					{
						UdonBehaviour udonBehaviour = type[index];
						udonBehaviour.Interact();
						count++;
						yield return new WaitForSeconds(0.1f);
					}
					CenterConsole.Log(CenterConsole.LogsType.UDON, $"Sent {count} Udon Events");
				}
			}, "Interact with all udon events", null, halfBtn: true);
		}
	}
}
