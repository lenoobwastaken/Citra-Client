using UnityEngine;

namespace CitraClient.GUI.QMChanges
{
	public static class LoadButtonExtensions
	{
		public static GameObject LoadScreen1 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/");

		public static GameObject LoadScreen2 = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music");

		public static GameObject LoadButtonTemplate()
		{
			if (LoadScreen1 == null)
			{
				return null;
			}
			return GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress");
		}
	}
}
