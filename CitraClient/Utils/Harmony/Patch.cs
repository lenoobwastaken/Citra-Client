using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace CitraClient.Utils.Harmony
{
	public class Patch
	{
		public static readonly List<Patch> patches = new List<Patch>();

		public MethodInfo TargetMethod { get; set; }

		public HarmonyMethod PrefixMethod { get; set; }

		public HarmonyMethod PostfixMethod { get; set; }

		public HarmonyLib.Harmony Instance { get; set; }

		public Patch(MethodInfo targetMethod, HarmonyMethod Before = null, HarmonyMethod After = null)
		{
			if (targetMethod == null || (Before == null && After == null))
			{
				ConsoleUtils.OnLogError("[Patch] TargetMethod is NULL or Pre And PostFix are Null");
				return;
			}
			Instance = new HarmonyLib.Harmony("Patch:" + targetMethod.DeclaringType.FullName + "." + targetMethod.Name);
			TargetMethod = targetMethod;
			PrefixMethod = Before;
			PostfixMethod = After;
			patches.Add(this);
		}

		public static void PatchAll()
		{
			foreach (Patch patch in patches)
			{
				try
				{
					patch.Instance.Patch(patch.TargetMethod, patch.PrefixMethod, patch.PostfixMethod);
					ConsoleUtils.OnLogSuccess($"Successfully patched {patch.TargetMethod}");
				}
				catch (Exception arg)
				{
					ConsoleUtils.OnLogError($"Failed Patch {patch.TargetMethod}.\n {arg}");
				}
			}
		}
	}
}
