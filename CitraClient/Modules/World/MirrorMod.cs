using System.Collections.Generic;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using UnityEngine;
using VRC.SDKBase;

namespace CitraClient.Modules.World
{
	public class MirrorMod : ModuleBase
	{
		public class OriginalMirror
		{
			public VRC_MirrorReflection MirrorParent;

			public LayerMask OriginalLayers;
		}

		public static List<OriginalMirror> originalMirrors;

		private static readonly LayerMask optimizeMask;

		private static readonly LayerMask beautifyMask;

		public static GameObject _mirror;

		public static float _mirrorScaleX;

		public static float _mirrorScaleY;

		public static bool _optimizedMirror;

		public static bool _canPickupMirror;

		public override void OnSceneLoad(int buildIndex, string sceneName)
		{
			originalMirrors = new List<OriginalMirror>();
			foreach (VRC_MirrorReflection item in Resources.FindObjectsOfTypeAll<VRC_MirrorReflection>())
			{
				originalMirrors.Add(new OriginalMirror
				{
					MirrorParent = item,
					OriginalLayers = item.m_ReflectLayers
				});
			}
		}

		public static void Optimize()
		{
			if (originalMirrors.Count == 0)
			{
				return;
			}
			foreach (OriginalMirror originalMirror in originalMirrors)
			{
				originalMirror.MirrorParent.m_ReflectLayers = optimizeMask;
			}
		}

		public static void Beautify()
		{
			if (originalMirrors.Count == 0)
			{
				return;
			}
			foreach (OriginalMirror originalMirror in originalMirrors)
			{
				originalMirror.MirrorParent.m_ReflectLayers = beautifyMask;
			}
		}

		public static void Revert()
		{
			if (originalMirrors.Count == 0)
			{
				return;
			}
			foreach (OriginalMirror originalMirror in originalMirrors)
			{
				originalMirror.MirrorParent.m_ReflectLayers = originalMirror.OriginalLayers;
			}
		}

		static MirrorMod()
		{
			originalMirrors = new List<OriginalMirror>();
			_mirrorScaleX = 3f;
			_mirrorScaleY = 2.5f;
			_optimizedMirror = false;
			_canPickupMirror = true;
			LayerMask layerMask = (optimizeMask = new LayerMask
			{
				value = 263680
			});
			LayerMask layerMask2 = (beautifyMask = new LayerMask
			{
				value = -1025
			});
		}

		public static void ToggleMirror(bool shouldDestroy)
		{
			if (shouldDestroy)
			{
				if (_mirror != null)
				{
					Object.Destroy(_mirror);
					_mirror = null;
				}
				return;
			}
			VRCPlayer localPlayer = PlayerUtils.GetLocalPlayer();
			Transform transform = localPlayer.transform;
			Vector3 position = transform.position + transform.forward;
			position.y += _mirrorScaleY / 1.7f;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.position = position;
			gameObject.transform.rotation = localPlayer.transform.rotation;
			gameObject.transform.localScale = new Vector3(_mirrorScaleX, _mirrorScaleY, 1f);
			gameObject.name = "PortableMirror";
			Object.Destroy(gameObject.GetComponent<Collider>());
			gameObject.GetOrAddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.05f);
			gameObject.GetOrAddComponent<BoxCollider>().isTrigger = true;
			gameObject.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("FX/MirrorReflection");
			gameObject.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
			{
				value = (_optimizedMirror ? 263680 : (-1025))
			};
			gameObject.GetOrAddComponent<VRC_Pickup>().proximity = 0.3f;
			gameObject.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirror;
			gameObject.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
			gameObject.GetOrAddComponent<Rigidbody>().useGravity = false;
			gameObject.GetOrAddComponent<Rigidbody>().isKinematic = true;
			_mirror = gameObject;
		}
	}
}
