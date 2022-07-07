using System;
using System.Collections;
using System.ComponentModel;
using CitraClient.Modules.Movement;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace CitraClient.Modules.Player
{
	public static class AttachToPlayer
	{
		public enum BodyPart
		{
			NOT_ATTACHED,
			HEAD,
			LEFT_HAND,
			LEFT_FOOT,
			RIGHT_HAND,
			RIGHT_FOOT
		}

		public static bool _shouldAttach;

		public static void AttachToggle()
		{
			_shouldAttach = !_shouldAttach;
		}

		public static IEnumerator AttachTo(BodyPart attachLocation, VRC.Player target)
		{
			if (!Enum.IsDefined(typeof(BodyPart), attachLocation))
			{
				throw new InvalidEnumArgumentException("attachLocation", (int)attachLocation, typeof(BodyPart));
			}
			VRCPlayer p = VRCPlayer.field_Internal_Static_VRCPlayer_0;
			VRCPlayerApi t = target.field_Private_VRCPlayerApi_0;
			switch (attachLocation)
			{
			case BodyPart.NOT_ATTACHED:
				yield break;
			case BodyPart.HEAD:
				while (_shouldAttach)
				{
					if (target == null)
					{
						yield break;
					}
					if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
					{
						p.transform.position = t.GetBonePosition(HumanBodyBones.Head);
						TempMovement.EnableFlight();
						yield return null;
						yield return null;
						continue;
					}
					TempMovement.DisableFlight();
					yield break;
				}
				break;
			case BodyPart.LEFT_HAND:
				while (_shouldAttach)
				{
					if (target == null)
					{
						yield break;
					}
					if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
					{
						p.transform.position = t.GetBonePosition(HumanBodyBones.LeftHand);
						TempMovement.EnableFlight();
						yield return null;
						yield return null;
						continue;
					}
					TempMovement.DisableFlight();
					yield break;
				}
				break;
			case BodyPart.LEFT_FOOT:
				while (_shouldAttach)
				{
					if (target == null)
					{
						yield break;
					}
					if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
					{
						p.transform.position = t.GetBonePosition(HumanBodyBones.LeftFoot);
						TempMovement.EnableFlight();
						yield return null;
						yield return null;
						continue;
					}
					TempMovement.DisableFlight();
					yield break;
				}
				break;
			case BodyPart.RIGHT_HAND:
				while (_shouldAttach)
				{
					if (target == null)
					{
						yield break;
					}
					if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
					{
						p.transform.position = t.GetBonePosition(HumanBodyBones.RightHand);
						TempMovement.EnableFlight();
						yield return null;
						yield return null;
						continue;
					}
					TempMovement.DisableFlight();
					yield break;
				}
				break;
			case BodyPart.RIGHT_FOOT:
				while (_shouldAttach)
				{
					if (target == null)
					{
						yield break;
					}
					if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
					{
						p.transform.position = t.GetBonePosition(HumanBodyBones.RightFoot);
						TempMovement.EnableFlight();
						yield return null;
						yield return null;
						continue;
					}
					TempMovement.DisableFlight();
					yield break;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("attachLocation", attachLocation, null);
			}
			yield return null;
		}
	}
}
