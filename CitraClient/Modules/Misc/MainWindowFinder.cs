using System;
using System.Runtime.InteropServices;

namespace CitraClient.Modules.Misc
{
	internal class MainWindowFinder
	{
		private delegate bool CallBackPtr(IntPtr hwnd, int lParam);

		private enum GetWindowType : uint
		{
			GW_HWNDFIRST,
			GW_HWNDLAST,
			GW_HWNDNEXT,
			GW_HWNDPREV,
			GW_OWNER,
			GW_CHILD,
			GW_ENABLEDPOPUP
		}

		private IntPtr _bestHandle;

		private int _processId;

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumWindows(CallBackPtr lpEnumFunc, int lParam);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		public IntPtr FindMainWindow(int processId)
		{
			_bestHandle = IntPtr.Zero;
			_processId = processId;
			EnumWindows(EnumWindowsThunk, _processId);
			return _bestHandle;
		}

		private bool EnumWindowsThunk(IntPtr hWnd, int processId)
		{
			GetWindowThreadProcessId(hWnd, out var lpdwProcessId);
			if (lpdwProcessId != processId || !IsMainWindow(hWnd))
			{
				return true;
			}
			_bestHandle = hWnd;
			return false;
		}

		private static bool IsMainWindow(IntPtr hWnd)
		{
			if (GetWindow(hWnd, GetWindowType.GW_OWNER) == IntPtr.Zero && IsWindowVisible(hWnd))
			{
				return true;
			}
			return false;
		}
	}
}
