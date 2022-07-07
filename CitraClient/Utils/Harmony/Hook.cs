using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;
using UnhollowerBaseLib;

namespace CitraClient.Utils.Harmony
{
	public static class Hook
	{
		public static List<Delegate> hooks = new List<Delegate>();

		public unsafe static TDelegate NativeHook<TDelegate>(MethodInfo targetMethod, MethodInfo patch) where TDelegate : Delegate
		{
			IntPtr ptr = *(IntPtr*)(void*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(targetMethod).GetValue(null);
			try
			{
				MelonUtils.NativeHookAttach((IntPtr)(&ptr), patch.MethodHandle.GetFunctionPointer());
				hooks.Add(Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr));
			}
			catch (Exception ex)
			{
				ConsoleUtils.OnLogError("Failed native patch " + targetMethod.Name + ".\n" + ex.Message);
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
		}

		public unsafe static TDelegate NativeHook<TDelegate>(IntPtr pointer, MethodInfo patch) where TDelegate : Delegate
		{
			MelonUtils.NativeHookAttach((IntPtr)(&pointer), patch.MethodHandle.GetFunctionPointer());
			hooks.Add(Marshal.GetDelegateForFunctionPointer<TDelegate>(pointer));
			return Marshal.GetDelegateForFunctionPointer<TDelegate>(pointer);
		}
	}
}
