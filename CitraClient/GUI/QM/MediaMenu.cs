using CitraClient.API.QM;
using CitraClient.Modules.Misc;

namespace CitraClient.GUI.QM
{
	public class MediaMenu
	{
		public static QMNestedButton mediaMenu;

		public static void InitMediaMenu()
		{
			mediaMenu = new QMNestedButton(MainMenu.MainPage, "Media Controls", 1f, 2f, "Media Controls for <color=#CE389C>Citra:</color>", "<color=#CE389C>Citra:</color> <color=#00d1ed>Media Control</color>");
			new QMSingleButton(mediaMenu, 1f, 0f, "Play/Pause", delegate
			{
				MediaControl.PlayPause();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 2f, 0f, "Previous Song", delegate
			{
				MediaControl.PrevTrack();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 3f, 0f, "Next Song", delegate
			{
				MediaControl.NextTrack();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 4f, 0f, "Stop", delegate
			{
				MediaControl.Stop();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 1f, 1f, "Volume Up", delegate
			{
				MediaControl.VolumeUp();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 2f, 1f, "Volume Down", delegate
			{
				MediaControl.VolumeDown();
			}, string.Empty);
			new QMSingleButton(mediaMenu, 3f, 1f, "Mute", delegate
			{
				MediaControl.VolumeMute();
			}, string.Empty);
		}
	}
}
