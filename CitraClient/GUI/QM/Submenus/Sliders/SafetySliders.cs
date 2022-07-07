using CitraClient.API.QM;
using CitraClient.Config;

namespace CitraClient.GUI.QM.Submenus.Sliders
{
	public class SafetySliders
	{
		public static void Init()
		{
			QMNestedButton qMNestedButton = new QMNestedButton(SafetyMenu.safetyMenu, "Options", 4f, 3.5f, "Options for the safety functions of <color=#CE389C>Citra</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Safety</color>", halfButton: true);
			new QMSlider(qMNestedButton, "Ping Amount", 500f, -150f, delegate(float f)
			{
				Configuration.GetConfig().pingSpoofAmount = (int)f;
				Configuration.Save();
			}, "Change the amount your server ping is spoofed to", -32768f, 32767f, Configuration.GetConfig().pingSpoofAmount);
			new QMSlider(qMNestedButton, "FPS Amount", 500f, -250f, delegate(float f)
			{
				Configuration.GetConfig().fpsSpoofAmount = f;
				Configuration.Save();
			}, "Change the amount your server fps is spoofed to", -32768f, 32767f, Configuration.GetConfig().fpsSpoofAmount);
			new QMToggleButton(qMNestedButton, 1f, 1f, "Random Ping", delegate
			{
				Configuration.GetConfig().randomPing = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().randomPing = false;
				Configuration.Save();
			}, "Sets your ping to random values", Configuration.GetConfig().randomPing);
			new QMToggleButton(qMNestedButton, 2f, 1f, "Infinite Ping", delegate
			{
				Configuration.GetConfig().infinityPing = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().infinityPing = false;
				Configuration.Save();
			}, "Sets your ping to infinity", Configuration.GetConfig().infinityPing);
			new QMToggleButton(qMNestedButton, 3f, 1f, "Legit Ping", delegate
			{
				Configuration.GetConfig().legitPing = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().legitPing = false;
				Configuration.Save();
			}, "Spoofs your ping while making it look legit.", Configuration.GetConfig().legitPing);
			new QMToggleButton(qMNestedButton, 1f, 2f, "Random FPS", delegate
			{
				Configuration.GetConfig().randomFPS = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().randomFPS = false;
				Configuration.Save();
			}, "Sets your FPS to random values", Configuration.GetConfig().randomFPS);
			new QMToggleButton(qMNestedButton, 2f, 2f, "Infinite FPS", delegate
			{
				Configuration.GetConfig().infinityFPS = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().infinityFPS = false;
				Configuration.Save();
			}, "Sets your FPS to infinity", Configuration.GetConfig().infinityFPS);
			new QMToggleButton(qMNestedButton, 3f, 2f, "Legit FPS", delegate
			{
				Configuration.GetConfig().legitFPS = true;
				Configuration.Save();
			}, delegate
			{
				Configuration.GetConfig().legitFPS = false;
				Configuration.Save();
			}, "Spoofs your FPS while making it look legit.", Configuration.GetConfig().legitFPS);
		}
	}
}
