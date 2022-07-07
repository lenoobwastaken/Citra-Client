using System.Collections;
using CitraClient.Utils;
using CitraClient.Utils.UI;
using Il2CppSystem;
using MelonLoader;
using UnityEngine;

namespace CitraClient.Modules.Misc
{
	public static class RussianRoulette
	{
		public static void RouletteStart()
		{
			int[] array = new int[6];
			int[] array2 = new int[6];
			Il2CppSystem.Random random = new Il2CppSystem.Random();
			int num = random.Next(0, 6);
			int num2 = random.Next(0, 6);
			array[num] = 1;
			array2[num2] = 1;
			MezLogger.HudMsg("[Russian Roulette]: Good luck!", 5f);
			MezLogger.HudMsg("[Russian Roulette]: Ready?", 5f);
			bool flag = false;
			int num3 = 0;
			int num4 = 1;
			do
			{
				if (array2[num3] == 1)
				{
					MezLogger.HudMsg($"[Russian Roulette] *Bang* - Computer has lost, You win on round {num4}", 5f);
					flag = true;
				}
				else if (array[num3] == 1)
				{
					MezLogger.HudMsg($"[Russian Roulette] *Bang* - Computer wins on round {num4}, you lost!", 5f);
					flag = true;
					MelonCoroutines.Start(RouletteLost());
				}
				else
				{
					MezLogger.HudMsg($"[Russian Roulette] *Click* - You both survived round {num4}", 5f);
					num3++;
					num4++;
				}
			}
			while (!flag);
		}

		private static IEnumerator RouletteLost()
		{
			yield return new WaitForSeconds(5f);
			ApplicationUtils.OpenLinkInBrowser("https://www.youtube.com/watch?v=Xzx3-hKl__8");
			yield return new WaitForSeconds(5f);
			ApplicationUtils.RestartGame();
		}
	}
}
