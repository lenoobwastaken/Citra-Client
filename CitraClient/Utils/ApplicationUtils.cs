using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using CitraClient.Config;
using MelonLoader;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC;
using VRC.Core;

namespace CitraClient.Utils
{
	public static class ApplicationUtils
	{
		public static bool IsInVR
		{
			get
			{
				try
				{
					return Player.prop_Player_0.prop_VRCPlayerApi_0.IsUserInVR();
				}
				catch
				{
					return Environment.GetCommandLineArgs().All((string args) => !args.Equals("--no-vr", StringComparison.OrdinalIgnoreCase));
				}
			}
		}

		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		public static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		public static extern bool SetProcessWorkingSetSize64Bit(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);

		public static void Start(this IEnumerator instance)
		{
			MelonCoroutines.Start(instance);
		}

		public static void Stop(this IEnumerator instance)
		{
			MelonCoroutines.Stop(instance);
		}

		public static void RestartGame()
		{
			Process.Start(Environment.CurrentDirectory + "\\VRChat.exe", Environment.CommandLine);
			Process.GetCurrentProcess().Kill();
		}

		public static void QuitGame()
		{
			Process.GetCurrentProcess().Kill();
		}

		public static void RestartRejoin()
		{
			ApiWorldInstance field_Internal_Static_ApiWorldInstance_ = RoomManager.field_Internal_Static_ApiWorldInstance_0;
			StartProcess(Directory.GetCurrentDirectory() + "/VRChat.exe", (VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0 != null && VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.IsUserInVR()) ? ("vrchat://launch?id=" + field_Internal_Static_ApiWorldInstance_.id) : ("vrchat://launch?id=" + field_Internal_Static_ApiWorldInstance_.id + " --no-vr"));
			Process.GetCurrentProcess().Kill();
		}

		private static void StartProcess(string path, string commands)
		{
			using Process process = new Process();
			process.StartInfo.FileName = path;
			process.StartInfo.Arguments = commands;
			process.StartInfo.UseShellExecute = true;
			process.Start();
		}

		public static bool CheckUsedBy(MethodBase method, Func<MethodBase, bool> predicate)
		{
			return XrefScanner.UsedBy(method).Any((XrefInstance instance) => instance.Type == XrefType.Method && predicate(instance.TryResolve()));
		}

		public static bool CheckUsedBy(MethodBase method, string methodName, Type type = null)
		{
			return CheckUsedBy(method, (MethodBase usedByMethod) => usedByMethod != null && (type == null || usedByMethod.DeclaringType == type) && usedByMethod.Name.Contains(methodName));
		}

		public static void SetVRChatPriority(int state)
		{
			switch (state)
			{
			case 0:
			{
				using Process process2 = Process.GetCurrentProcess();
				process2.PriorityClass = ProcessPriorityClass.Normal;
				break;
			}
			case 1:
			{
				using Process process = Process.GetCurrentProcess();
				process.PriorityClass = ProcessPriorityClass.High;
				break;
			}
			}
		}

		public static void MaxFrames()
		{
			bool isMaxFrames = Configuration.GetConfig().isMaxFrames;
			if (1 == 0)
			{
			}
			int targetFrameRate = ((!isMaxFrames) ? 90 : 999);
			if (1 == 0)
			{
			}
			Application.targetFrameRate = targetFrameRate;
		}

		public static void OpenLinkInBrowser(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					url = url.Replace("&", "^&");
					Process.Start(new ProcessStartInfo("cmd", "/c start " + url)
					{
						CreateNoWindow = true
					});
				}
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start("xdg-open", url);
				}
				if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", url);
					return;
				}
				throw;
			}
		}

		public static void RamClear()
		{
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				SetProcessWorkingSetSize32Bit(Process.GetCurrentProcess().Handle, -1, -1);
			}
		}
	}
}
