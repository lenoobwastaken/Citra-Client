using System;
using System.Collections;
using CitraClient.API.QM;
using TMPro;
using UnityEngine;

namespace CitraClient.Utils
{
	public static class MiscUtils
	{
		public static string MultiCharacterString(string character, int amount)
		{
			return new string(Convert.ToChar(character), amount);
		}

		public static IEnumerator CountDownButton(QMSingleButton button, int countDown, string defaultText)
		{
			while (countDown > 0)
			{
				button.GetGameObject().GetComponentInChildren<TextMeshProUGUI>().text = $"CoolDown\n{countDown}";
				countDown--;
				yield return new WaitForSeconds(1f);
			}
			if (countDown == 0)
			{
				button.GetGameObject().GetComponentInChildren<TextMeshProUGUI>().text = defaultText;
			}
			yield return null;
		}
	}
}
