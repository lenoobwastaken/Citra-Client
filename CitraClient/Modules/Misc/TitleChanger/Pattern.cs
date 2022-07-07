namespace CitraClient.Modules.Misc.TitleChanger
{
	public class Pattern
	{
		public string[] PatternArray { get; }

		public float WaitTime { get; }

		public Pattern(string[] patternArray, float waitTime)
		{
			PatternArray = patternArray;
			WaitTime = waitTime;
		}
	}
}
