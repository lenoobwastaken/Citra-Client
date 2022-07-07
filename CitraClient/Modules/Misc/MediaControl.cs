using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using CitraClient.GUI.QM.Console;
using CitraClient.Modules.Base;
using UnityEngine;

namespace CitraClient.Modules.Misc
{
	public class MediaControl : ModuleBase
	{
		private const byte MediaPlayPause = 179;

		private const byte MediaNextTrack = 176;

		private const byte MediaPreviousTrack = 177;

		private const byte MediaStop = 178;

		private const byte VolUp = 175;

		private const byte VolDown = 174;

		private const byte VolMute = 173;

		private static readonly int KEYEVENTF_KEYUP = 2;

		private readonly bool SongPreviewEnabled = true;

		private string _currentSong;

		private IntPtr _spotifyWindow;

		private float _timer;

		private bool MediaControlsEnabled;

		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		public static void PlayPause()
		{
			keybd_event(179, 179, 0, 0);
			keybd_event(179, 179, KEYEVENTF_KEYUP, 0);
		}

		public static void PrevTrack()
		{
			keybd_event(177, 177, 0, 0);
			keybd_event(177, 177, KEYEVENTF_KEYUP, 0);
		}

		public static void NextTrack()
		{
			keybd_event(176, 176, 0, 0);
			keybd_event(176, 176, KEYEVENTF_KEYUP, 0);
		}

		public static void Stop()
		{
			keybd_event(178, 178, 0, 0);
			keybd_event(178, 178, KEYEVENTF_KEYUP, 0);
		}

		public static void VolumeUp()
		{
			keybd_event(175, 175, 0, 0);
			keybd_event(175, 175, KEYEVENTF_KEYUP, 0);
		}

		public static void VolumeDown()
		{
			keybd_event(174, 174, 0, 0);
			keybd_event(174, 174, KEYEVENTF_KEYUP, 0);
		}

		public static void VolumeMute()
		{
			keybd_event(173, 173, 0, 0);
			keybd_event(173, 173, KEYEVENTF_KEYUP, 0);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindow(IntPtr hWnd);

		public override void QMLoaded()
		{
			new Thread((ThreadStart)delegate
			{
				while (true)
				{
					Thread.Sleep(1000);
					if (!(_spotifyWindow != IntPtr.Zero) || !IsWindow(_spotifyWindow))
					{
						Process process = Process.GetProcessesByName("Spotify").FirstOrDefault();
						if (process != null)
						{
							_spotifyWindow = new MainWindowFinder().FindMainWindow(process.Id);
							if (!(_spotifyWindow == IntPtr.Zero) && IsWindow(_spotifyWindow))
							{
							}
						}
					}
				}
			}).Start();
		}

		public void ToggleMediaControls()
		{
			MediaControlsEnabled = !MediaControlsEnabled;
		}

		public override void Update()
		{
			_timer += Time.deltaTime;
			if (_timer < 1f)
			{
				return;
			}
			_timer = 0f;
			if (!(_spotifyWindow == IntPtr.Zero) && IsWindow(_spotifyWindow))
			{
				int num = GetWindowTextLength(_spotifyWindow) * 2;
				StringBuilder stringBuilder = new StringBuilder(num + 1);
				GetWindowText(_spotifyWindow, stringBuilder, stringBuilder.Capacity);
				string text = stringBuilder.ToString();
				bool flag = !text.StartsWith("Spotify");
				if (flag && _currentSong != text && SongPreviewEnabled)
				{
					CenterConsole.Log(CenterConsole.LogsType.MEDIA, "<color=#a83246>Now Playing on Spotify:</color> <color=#324ea8>" + text + "</color>");
				}
				_currentSong = (flag ? text : string.Empty);
			}
		}
	}
}
