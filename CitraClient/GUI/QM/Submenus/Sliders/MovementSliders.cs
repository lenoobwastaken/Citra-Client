using CitraClient.API.QM;

namespace CitraClient.GUI.QM.Submenus.Sliders
{
	public class MovementSliders
	{
		public static void Init()
		{
			QMNestedButton qMNestedButton = new QMNestedButton(MovementMenu.movementMenu, "Options", 4f, 3.5f, "Options for the movement functions of <color=#CE389C>Citra:</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Movement</color>", halfButton: true);
		}
	}
}
