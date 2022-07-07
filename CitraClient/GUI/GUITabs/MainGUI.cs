using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.GUI.GUITabs
{
	public static class MainGUI
	{
		private static int _selectedTab;

		private static int _yOffset;

		public static void Tab()
		{
			_yOffset = 200;
			if (Input.GetKey(KeyCode.Tab))
			{
				if (UnityEngine.GUI.Button(new Rect(60f, 100f, 120f, 90f), ImageUtils.CitraLogoSprite.texture))
				{
				}
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 0) ? "<color=magenta><b>[Main]</b></color>" : "<color=cyan><b>Main</b></color>"))
				{
					_selectedTab = 0;
				}
				_yOffset += 50;
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 1) ? "<color=magenta><b>[Players]</b></color>" : "<color=cyan><b>Players</b></color>"))
				{
					_selectedTab = 1;
				}
				_yOffset += 50;
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 2) ? "<color=magenta><b>[Movement]</b></color>" : "<color=cyan><b>Main</b></color>"))
				{
					_selectedTab = 2;
				}
				_yOffset += 50;
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 3) ? "<color=magenta><b>[SkyBoxes]</b></color>" : "<color=cyan><b>SkyBoxes</b></color>"))
				{
					_selectedTab = 3;
				}
				_yOffset += 50;
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 4) ? "<color=magenta><b>[WorldHistory]</b></color>" : "<color=cyan><b>WorldHistory</b></color>"))
				{
					_selectedTab = 4;
				}
				_yOffset += 50;
				if (UnityEngine.GUI.Button(new Rect(70f, _yOffset, 100f, 35f), (_selectedTab == 5) ? "<color=magenta><b>[Exploits]</b></color>" : "<color=cyan><b>Exploits</b></color>"))
				{
					_selectedTab = 5;
				}
				switch (_selectedTab)
				{
				case 0:
					MainTab.Tab();
					break;
				case 1:
					PlayersTab.PlayerTab();
					break;
				case 2:
					MovementTab.Tab();
					break;
				case 3:
					SkyBoxTab.Tab();
					break;
				case 4:
					InstanceHistoryTab.Tab();
					break;
				}
			}
		}
	}
}
