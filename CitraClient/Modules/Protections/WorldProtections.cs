using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CitraClient.GUI.QM.Console;
using CitraClient.Modules.Base;
using CitraClient.Modules.World;
using CitraClient.Utils;
using UnityEngine;
using UnityEngine.Video;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace CitraClient.Modules.Protections
{
	public class WorldProtections : ModuleBase
	{
		private const bool IsDebug = true;

		private static Vector3 _currentVector;

		private static Quaternion _currentQuaternion;

		private static int _countedPickups;

		private static int _countedPens;

		private static int _countedVideoPlayers;

		private static int _countedMirrors;

		private static VideoPlayer[] _videoPlayerArray;

		private static VRC_MirrorReflection[] _mirrorReflectionArray;

		private static VRCMirrorReflection[] _sdk3MirrorReflectionArray;

		private static List<VRC_Pickup> _penComponentList;

		private static readonly List<string> PenComponentNameList = new List<string> { "pen", "marker", "grip" };

		private static IEnumerator FindComponentsOnSceneLoad()
		{
			_countedPickups = 0;
			_countedPens = 0;
			_countedVideoPlayers = 0;
			_countedMirrors = 0;
			_penComponentList = new List<VRC_Pickup>();
			_videoPlayerArray = Resources.FindObjectsOfTypeAll<VideoPlayer>().ToArray();
			_mirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRC_MirrorReflection>().ToArray();
			_sdk3MirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRCMirrorReflection>().ToArray();
			VRC_Pickup[] getAllPickups = PickupUtils.GetAllPickups;
			foreach (VRC_Pickup pickup in getAllPickups)
			{
				foreach (string item in PenComponentNameList.Where((string name) => pickup.name.ToLower().Contains(name) && !pickup.transform.parent.name.ToLower().Contains("eraser")))
				{
					_ = item;
					_penComponentList.Add(pickup);
				}
				_countedPens++;
			}
			VRC_Pickup[] getAllPickups2 = PickupUtils.GetAllPickups;
			foreach (VRC_Pickup pickup2 in getAllPickups2)
			{
				if (pickup2 != null)
				{
					_countedPickups++;
				}
			}
			VideoPlayer[] videoPlayerArray = _videoPlayerArray;
			foreach (VideoPlayer videoPlayer in videoPlayerArray)
			{
				if (videoPlayer != null)
				{
					_countedVideoPlayers++;
				}
			}
			yield break;
		}

		public static void EnableDisablePickups(bool state)
		{
			if (!WorldUtils.IsInWorld())
			{
				return;
			}
			VRC_Pickup[] getAllPickups = PickupUtils.GetAllPickups;
			if (state)
			{
				VRC_Pickup[] array = getAllPickups;
				foreach (VRC_Pickup vRC_Pickup in array)
				{
					if (vRC_Pickup != null)
					{
						vRC_Pickup.gameObject.SetActive(value: false);
					}
				}
				bool flag = true;
				ConsoleUtils.OnLogSuccess($"toggled off [{_countedPickups}] pickups.");
				CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"toggled off [{_countedPickups}] pickups.");
			}
			else
			{
				if (state)
				{
					return;
				}
				VRC_Pickup[] array2 = getAllPickups;
				foreach (VRC_Pickup vRC_Pickup2 in array2)
				{
					if (vRC_Pickup2 != null)
					{
						vRC_Pickup2.gameObject.SetActive(value: true);
					}
				}
				ConsoleUtils.OnLogSuccess($"toggled on [{_countedPickups}] pickups.");
				CenterConsole.Log(CenterConsole.LogsType.INFO, $"toggled on [{_countedPickups}] pickups.");
			}
		}

		public static void EnableDisablePens(bool state)
		{
			if (!WorldUtils.IsInWorld())
			{
				return;
			}
			if (state)
			{
				foreach (VRC_Pickup penComponent in _penComponentList)
				{
					if (penComponent != null)
					{
						penComponent.gameObject.SetActive(value: false);
					}
				}
				bool flag = true;
				ConsoleUtils.OnLogSuccess($"Toggled off [{_countedPens}] pens.");
				CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled off [{_countedPens}] pens.");
				return;
			}
			foreach (VRC_Pickup penComponent2 in _penComponentList)
			{
				if (penComponent2 != null)
				{
					penComponent2.gameObject.SetActive(value: true);
				}
			}
			bool flag2 = true;
			ConsoleUtils.OnLogSuccess($"Toggled on [{_countedPens}] pens.");
			CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled on [{_countedPens}] pens.");
		}

		public static void EnableDisableMirrors(bool state)
		{
			if (state)
			{
				VRC_MirrorReflection[] mirrorReflectionArray = _mirrorReflectionArray;
				foreach (VRC_MirrorReflection vRC_MirrorReflection in mirrorReflectionArray)
				{
					if (vRC_MirrorReflection != null)
					{
						vRC_MirrorReflection.GetComponent<VRC_MirrorReflection>().enabled = false;
					}
				}
				VRCMirrorReflection[] sdk3MirrorReflectionArray = _sdk3MirrorReflectionArray;
				foreach (VRCMirrorReflection vRCMirrorReflection in sdk3MirrorReflectionArray)
				{
					if (vRCMirrorReflection != null)
					{
						vRCMirrorReflection.GetComponent<VRCMirrorReflection>().enabled = false;
					}
				}
				bool flag = true;
				ConsoleUtils.OnLogSuccess($"Toggled off [{_countedMirrors}] mirrors.");
				CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled off [{_countedMirrors}] mirrors.");
				return;
			}
			VRC_MirrorReflection[] mirrorReflectionArray2 = _mirrorReflectionArray;
			foreach (VRC_MirrorReflection vRC_MirrorReflection2 in mirrorReflectionArray2)
			{
				if (vRC_MirrorReflection2 != null)
				{
					vRC_MirrorReflection2.GetComponent<VRC_MirrorReflection>().enabled = true;
				}
			}
			VRCMirrorReflection[] sdk3MirrorReflectionArray2 = _sdk3MirrorReflectionArray;
			foreach (VRCMirrorReflection vRCMirrorReflection2 in sdk3MirrorReflectionArray2)
			{
				if (vRCMirrorReflection2 != null)
				{
					vRCMirrorReflection2.GetComponent<VRCMirrorReflection>().enabled = true;
				}
			}
			bool flag2 = true;
			ConsoleUtils.OnLogSuccess($"Toggled on [{_countedMirrors}] mirrors.");
			CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled on [{_countedMirrors}] mirrors.");
		}

		public static void EnableDisableVideoPlayers(bool state)
		{
			if (state)
			{
				VideoPlayer[] videoPlayerArray = _videoPlayerArray;
				foreach (VideoPlayer videoPlayer in videoPlayerArray)
				{
					if (videoPlayer != null)
					{
						videoPlayer.GetComponent<VideoPlayer>().enabled = false;
					}
				}
				bool flag = true;
				ConsoleUtils.OnLogSuccess($"Toggled off [{_countedVideoPlayers}] Video Players.");
				CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled off [{_countedVideoPlayers}] Video Players.");
				return;
			}
			VideoPlayer[] videoPlayerArray2 = _videoPlayerArray;
			foreach (VideoPlayer videoPlayer2 in videoPlayerArray2)
			{
				if (videoPlayer2 != null)
				{
					videoPlayer2.GetComponent<VideoPlayer>().enabled = true;
				}
			}
			bool flag2 = true;
			ConsoleUtils.OnLogSuccess($"Toggled on [{_countedVideoPlayers}] Video Players.");
			CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Toggled on [{_countedVideoPlayers}] Video Players.");
		}

		public static IEnumerator TeleportToSpace(float delay)
		{
			_currentVector = PlayerUtils.GetLocalPlayer().transform.position;
			_currentQuaternion = PlayerUtils.GetLocalPlayer().transform.rotation;
			yield return new WaitForEndOfFrame();
			PlayerUtils.GetLocalPlayer().transform.position = new Vector3(0f, 999999f, 0f);
			yield return new WaitForSeconds(delay);
			WorldModules.CopyInstanceToClipboard();
			WorldModules.JoinInstanceFromClipboard();
		}

		public static void DestroyAllPortals()
		{
			int num = 0;
			if (!Resources.FindObjectsOfTypeAll<PortalInternal>().Any())
			{
				return;
			}
			foreach (PortalInternal item in Resources.FindObjectsOfTypeAll<PortalInternal>())
			{
				Object.Destroy(item.gameObject);
				num++;
			}
			CenterConsole.Log(CenterConsole.LogsType.DEBUG, $"Successfully Destroyed {num} Portals.");
		}

		public override void OnSceneLoad(int buildIndex, string sceneName)
		{
		}
	}
}
