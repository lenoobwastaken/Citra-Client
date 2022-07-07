using UnityEngine;

namespace CitraClient.API.SM
{
	public class SMAPI
	{
		public static GameObject labelBase;

		public static GameObject buttonBase;

		public static GameObject smBase;

		public static GameObject GetSM()
		{
			if (smBase == null)
			{
				smBase = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo");
			}
			return smBase;
		}

		public static GameObject GetLabelBase()
		{
			if (labelBase == null)
			{
				labelBase = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/TrustLevel/TrustText");
			}
			return labelBase;
		}

		public static GameObject GetButtonBase()
		{
			if (buttonBase == null)
			{
				buttonBase = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn/EditStatusButton/");
			}
			return buttonBase;
		}
	}
}
