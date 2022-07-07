using System.Collections.Generic;

namespace CitraClient.API.QM
{
	public static class CitraQMAPI
	{
		internal const string Identifier = "CitraClient";

		internal static List<QMSingleButton> allQMSingleButtons = new List<QMSingleButton>();

		internal static List<QMNestedButton> allQMNestedButtons = new List<QMNestedButton>();

		internal static List<QMToggleButton> allQMToggleButtons = new List<QMToggleButton>();
	}
}
