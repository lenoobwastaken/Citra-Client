namespace CitraClient.Modules.Misc.TitleChanger
{
	public static class Patterns
	{
		public static readonly Pattern PatternOne = new Pattern(new string[10] { "MelonLoader v0.5.4 Open-Beta | o    | Citra Client v1.0.1 | o    |", "MelonLoader v0.5.4 Open-Beta |  o   | Citra Client v1.0.1 |  o   |", "MelonLoader v0.5.4 Open-Beta |   o  | Citra Client v1.0.1 |   o  |", "MelonLoader v0.5.4 Open-Beta |    o | Citra Client v1.0.1 |    o |", "MelonLoader v0.5.4 Open-Beta |     o| Citra Client v1.0.1 |     o|", "MelonLoader v0.5.4 Open-Beta |    o | Citra Client v1.0.1 |    o |", "MelonLoader v0.5.4 Open-Beta |   o  | Citra Client v1.0.1 |   o  |", "MelonLoader v0.5.4 Open-Beta |  o   | Citra Client v1.0.1 |  o   |", "MelonLoader v0.5.4 Open-Beta | o    | Citra Client v1.0.1 | o    |", "MelonLoader v0.5.4 Open-Beta |o     | Citra Client v1.0.1 |o     |" }, 0.2f);

		public static readonly Pattern PatternTwo = new Pattern(new string[15]
		{
			"MelonLoader v0.5.4 Open-Beta [    ] Citra Client v1.0.1 [    ]", "MelonLoader v0.5.4 Open-Beta [=   ] Citra Client v1.0.1 [=   ]", "MelonLoader v0.5.4 Open-Beta [==  ] Citra Client v1.0.1 [==  ]", "MelonLoader v0.5.4 Open-Beta [=== ] Citra Client v1.0.1 [=== ]", "MelonLoader v0.5.4 Open-Beta [ ===] Citra Client v1.0.1 [ ===]", "MelonLoader v0.5.4 Open-Beta [  ==] Citra Client v1.0.1 [  ==]", "MelonLoader v0.5.4 Open-Beta [   =] Citra Client v1.0.1 [   =]", "MelonLoader v0.5.4 Open-Beta [    ] Citra Client v1.0.1 [    ]", "MelonLoader v0.5.4 Open-Beta [   =] Citra Client v1.0.1 [   =]", "MelonLoader v0.5.4 Open-Beta [  ==] Citra Client v1.0.1 [  ==]",
			"MelonLoader v0.5.4 Open-Beta [ ===] Citra Client v1.0.1 [ ===]", "MelonLoader v0.5.4 Open-Beta [====] Citra Client v1.0.1 [====]", "MelonLoader v0.5.4 Open-Beta [=== ] Citra Client v1.0.1 [=== ]", "MelonLoader v0.5.4 Open-Beta [==  ] Citra Client v1.0.1 [==  ]", "MelonLoader v0.5.4 Open-Beta [=   ] Citra Client v1.0.1 [=   ]"
		}, 0.2f);

		public static readonly Pattern PatternThree = new Pattern(new string[5] { "MelonLoader v0.5.4 Open-Beta .   Citra Client v1.0.1 .  ", "MelonLoader v0.5.4 Open-Beta ..  Citra Client v1.0.1 .. ", "MelonLoader v0.5.4 Open-Beta ... Citra Client v1.0.1 ...", "MelonLoader v0.5.4 Open-Beta  .. Citra Client v1.0.1  ..", "MelonLoader v0.5.4 Open-Beta   . Citra Client v1.0.1   ." }, 0.2f);

		public static readonly Pattern PatternFour = new Pattern(new string[5] { "MelonLoader v0.5.4 Open-Beta (>'-')> Citra Client v1.0.1 (>'-')>", "MelonLoader v0.5.4 Open-Beta <('-'<) Citra Client v1.0.1 <('-'<)", "MelonLoader v0.5.4 Open-Beta ^('-')^ Citra Client v1.0.1 ^('-')^", "MelonLoader v0.5.4 Open-Beta v('-')v Citra Client v1.0.1 v('-')v", "MelonLoader v0.5.4 Open-Beta <('-')> Citra Client v1.0.1 <('-')>" }, 0.2f);

		public static string StaticTitle { get; } = "MelonLoader v0.5.4 Open-Beta Citra Client v1.0.1";

	}
}
