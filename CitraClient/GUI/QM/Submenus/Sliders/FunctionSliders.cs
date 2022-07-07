using CitraClient.API.QM;
using CitraClient.Config;

namespace CitraClient.GUI.QM.Submenus.Sliders
{
	public class FunctionSliders
	{
		public static void Init()
		{
			QMNestedButton pge = new QMNestedButton(FunctionMenu.functionMenu, "Options", 4f, 3.5f, "Options for the Functions of <color=#CE389C>Citra</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Function Options</color>", halfButton: true);
			new QMSlider(pge, "NamePlate Height", 500f, -150f, delegate(float f)
			{
				Configuration.GetConfig().namePlateHeight = f;
				Configuration.Save();
			}, "Change the volume level of the loading screen volume.", 33f, 100f, Configuration.GetConfig().namePlateHeight);
		}
	}
}
