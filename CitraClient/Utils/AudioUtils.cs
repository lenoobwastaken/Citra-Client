using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CitraClient.Utils
{
	public static class AudioUtils
	{
		private static AudioSource _audioSource;

		private static AudioSource _audioSource2;

		public static IEnumerator StartSong(int buildIndex)
		{
			AudioClip audioClip = null;
			string[] urlArray = new string[6] { "https://wtfzammu.xyz/no/latenight.ogg", "https://wtfzammu.xyz/no/Cash-Pills.ogg", "https://wtfzammu.xyz/no/Elijah-Heaps-x-SpaceMan-Zack-I_m-Wrong.ogg", "https://wtfzammu.xyz/no/The_Kid_LAROI_-_MAYBE_Official_Vis__getmp3.pro_.ogg", "https://wtfzammu.xyz/no/Drawn_To_The_Sky_-_She_Once_Whisper__getmp3.pro_-_AudioTrimmer.com_.ogg", "https://wtfzammu.xyz/no/Rezcoast_Grizz_-_Loner_Official_Mu__getmp3.pro_.ogg" };
			if (buildIndex == 0)
			{
				System.Random rand = new System.Random();
				int index = rand.Next(urlArray.Length);
				while (GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup") == null)
				{
					yield return new WaitForEndOfFrame();
				}
				while (GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/LoadingSound") == null)
				{
					yield return new WaitForEndOfFrame();
				}
				_audioSource = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").GetComponentInChildren<AudioSource>(includeInactive: true);
				_audioSource2 = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/LoadingSound").GetComponentInChildren<AudioSource>(includeInactive: true);
				_audioSource.name = "Citra_LoadingMusic_1";
				_audioSource2.name = "Citra_LoadingMusic_2";
				UnityWebRequest www = UnityWebRequest.Get(urlArray[index] ?? "");
				www.SendWebRequest();
				while (!www.isDone)
				{
					yield return null;
				}
				if (www.isHttpError || www.isNetworkError)
				{
					ConsoleUtils.OnLogError("An error occured with the WebRequest for loading music.\nError: " + www.error);
					yield break;
				}
				audioClip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, stream: false, compressed: false, AudioType.OGGVORBIS);
				audioClip.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			}
			_audioSource.clip = audioClip;
			_audioSource2.clip = audioClip;
			_audioSource.Play();
			_audioSource2.Play();
		}
	}
}
