using System;
using System.Reflection;
using CitraClient.Config;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppSystem;
using Photon.Realtime;

namespace CitraClient.Utils.Harmony
{
	public class Patches
	{
		private static HarmonyMethod GetPatch(string name)
		{
			return new HarmonyMethod(typeof(Patches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
		}

		public static void InitPatch()
		{
			try
			{
				new Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetPatch("OpRaiseEvent"));
			}
			catch (System.Exception ex)
			{
				ConsoleUtils.OnLogError("Error during patching\n " + ex.Message);
			}
			finally
			{
				Patch.PatchAll();
				ConsoleUtils.OnLogSuccess($"Successfully applied {Patch.patches.Count} patches.");
			}
		}

		private static bool OpRaiseEvent(ref byte __0, ref Il2CppSystem.Object __1, ref RaiseEventOptions __2, ref SendOptions __3)
		{
			try
			{
				if (__1 != null && __2 != null)
				{
					byte b = __0;
					byte b2 = b;
					if (b2 == 7)
					{
						byte[] array = (byte[])Serialization.FromIL2CPPToManaged<object>(__1);
						if (array.Length > 75)
						{
							if (Configuration.GetConfig().fpsSpoof)
							{
								byte[] array2 = Serialization.FloatToBytes(1f / (float)Configuration.GetConfig().pingSpoofAmount);
								if (Configuration.GetConfig().randomPing)
								{
									array2 = Serialization.IntToBytes(PatchUtils.RandomSpoofInt());
								}
								else if (Configuration.GetConfig().infinityPing)
								{
									array2 = Serialization.IntToBytes(int.MinValue);
								}
								else if (Configuration.GetConfig().legitFPS)
								{
									array2 = Serialization.IntToBytes(PatchUtils.LegitPingSpoof());
								}
							}
							if (Configuration.GetConfig().fpsSpoof)
							{
								byte[] array3 = Serialization.FloatToBytes(1f / Configuration.GetConfig().fpsSpoofAmount);
								if (Configuration.GetConfig().randomFPS)
								{
									array3 = Serialization.FloatToBytes(1f / (float)PatchUtils.RandomSpoofInt());
								}
								else if (Configuration.GetConfig().infinityFPS)
								{
									array3 = Serialization.FloatToBytes(-0f);
								}
								else if (Configuration.GetConfig().legitFPS)
								{
									array3 = Serialization.FloatToBytes(1f / (float)PatchUtils.LegitFPSSpoof());
								}
							}
						}
						Il2CppSystem.Object @object = (__1 = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(array));
					}
				}
			}
			catch
			{
			}
			return true;
		}
	}
}
