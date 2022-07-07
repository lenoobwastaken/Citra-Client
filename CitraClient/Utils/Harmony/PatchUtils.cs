using System;

namespace CitraClient.Utils.Harmony
{
	public static class PatchUtils
	{
		private static readonly Random random = new Random();

		private static readonly int legitPingBase = random.Next(32, 197);

		private static readonly int legitFPSBase = random.Next(17, 240);

		private static int lastPingValue;

		private static int lastFPSValue;

		private static int currentPingValue;

		private static int currentFPSValue;

		public static int RandomSpoofInt()
		{
			return random.Next(int.MinValue, int.MaxValue);
		}

		public static int LegitFPSSpoof()
		{
			if (lastFPSValue == 0)
			{
				lastPingValue = legitFPSBase;
				return legitFPSBase;
			}
			int num = random.Next(0, 100);
			if (num < 50)
			{
				currentFPSValue = lastFPSValue - random.Next(0, 23);
			}
			else
			{
				currentFPSValue = lastFPSValue + random.Next(0, 27);
			}
			lastFPSValue = currentFPSValue;
			return currentFPSValue;
		}

		public static int LegitPingSpoof()
		{
			if (lastPingValue == 0)
			{
				lastPingValue = legitPingBase;
				return legitPingBase;
			}
			int num = random.Next(0, 100);
			if (num < 50)
			{
				currentPingValue = lastPingValue - random.Next(0, 23);
			}
			else
			{
				currentPingValue = lastPingValue + random.Next(0, 27);
			}
			lastPingValue = currentPingValue;
			return currentPingValue;
		}
	}
}
