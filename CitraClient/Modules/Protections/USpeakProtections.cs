using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CitraClient.Config;
using CitraClient.Utils;
using ExitGames.Client.Photon;
using MelonLoader;
using UnityEngine;

namespace CitraClient.Modules.Protections
{
	public static class USpeakProtections
	{
		public static List<int> blockedSenders = new List<int>();

		public static bool CheckUSpeak(EventData __0)
		{
			try
			{
				if (Configuration.GetConfig().uspeakProtection)
				{
					object obj = Serialization.FromIL2CPPToManaged<object>(__0.CustomData);
					byte[] array = (byte[])obj;
					byte[] second = new byte[4];
					byte[] first = array.Skip(11).Take(array.Length - 40).ToArray();
					byte[] second2 = new byte[31]
					{
						0, 248, 125, 232, 192, 92, 160, 82, 254, 48,
						228, 30, 187, 149, 196, 177, 215, 140, 223, 127,
						209, 66, 60, 0, 226, 53, 180, 176, 97, 104,
						4
					};
					if (blockedSenders.Contains(__0.Sender))
					{
						return false;
					}
					if (obj != null && array != null)
					{
						if (BitConverter.ToInt32(array, 0) == 0)
						{
							blockedSenders.Add(__0.Sender);
							MelonCoroutines.Start(ForgiveUser(__0.Sender));
							return false;
						}
						if (array.Take(4).SequenceEqual(second))
						{
							blockedSenders.Add(__0.Sender);
							MelonCoroutines.Start(ForgiveUser(__0.Sender));
							return false;
						}
						if (first.SequenceEqual(second2))
						{
							blockedSenders.Add(__0.Sender);
							MelonCoroutines.Start(ForgiveUser(__0.Sender));
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ConsoleUtils.OnLogError("Error in USpeak Protection: \n" + ex.Message);
			}
			return true;
		}

		public static IEnumerator ForgiveUser(int sender)
		{
			yield return new WaitForSeconds(15f);
			if (blockedSenders.Contains(sender) && PlayerUtils.GetLocalPlayer() != null)
			{
				blockedSenders.Remove(sender);
			}
		}
	}
}
