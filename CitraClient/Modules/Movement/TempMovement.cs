using System;
using CitraClient.Config;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using UnityEngine;
using UnityEngine.XR;
using VRC.Animation;
using VRC.SDKBase;

namespace CitraClient.Modules.Movement
{
	public class TempMovement : ModuleBase
	{
		private static VRCMotionState vrcMotionState;

		private static VRCPlayer _p = PlayerUtils.GetLocalPlayer();

		private static Quaternion _revertRotation;

		private static Vector3 _revertVector;

		public static float _delayBlinkTime;

		public static float _inputPressed;

		public static float _lastBlinkTime;

		public static float _boostAmount;

		private static bool IsBlinkAllowed => Time.time - _lastBlinkTime > _delayBlinkTime;

		public static void EnableFlight()
		{
			vrcMotionState = PlayerUtils.LocalPlayer().GetComponent<VRCMotionState>();
			PlayerUtils.LocalPlayer().GetComponent<CharacterController>().enabled = false;
		}

		public static void DisableFlight()
		{
			vrcMotionState.Method_Public_Void_0();
			PlayerUtils.LocalPlayer().GetComponent<CharacterController>().enabled = true;
		}

		public static void HandleFlight()
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			if (XRDevice.isPresent)
			{
				num = Input.GetAxis("Horizontal");
				num2 = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
				num3 = Input.GetAxis("Vertical");
			}
			else
			{
				if (Input.GetKey(KeyCode.W))
				{
					num3 += 1f;
				}
				if (Input.GetKey(KeyCode.S))
				{
					num3 -= 1f;
				}
				if (Input.GetKey(KeyCode.D))
				{
					num += 1f;
				}
				if (Input.GetKey(KeyCode.A))
				{
					num -= 1f;
				}
				if (Input.GetKey(KeyCode.E))
				{
					num2 += 1f;
				}
				if (Input.GetKey(KeyCode.Q))
				{
					num2 -= 1f;
				}
			}
			PlayerUtils.LocalPlayer().transform.position += PlayerUtils.LocalPlayer().transform.right * (Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * num * (float)((!Input.GetKey(KeyCode.LeftShift)) ? 1 : 8));
			PlayerUtils.LocalPlayer().transform.position += PlayerUtils.LocalPlayer().transform.forward * (Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * num3 * (float)((!Input.GetKey(KeyCode.LeftShift)) ? 1 : 8));
			PlayerUtils.LocalPlayer().transform.position += PlayerUtils.LocalPlayer().transform.up * (Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * num2 * (float)((!Input.GetKey(KeyCode.LeftShift)) ? 1 : 8));
			vrcMotionState.Reset();
		}

		public static void RaycastTeleportHandler()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0) && Camera.main != null)
			{
				Transform transform = Camera.main.transform;
				Transform transform2 = transform.transform;
				Ray ray = new Ray(transform2.position, transform2.forward);
				RaycastHit[] array = Physics.RaycastAll(ray);
				if (array.Length != 0)
				{
					RaycastHit raycastHit = array[0];
					VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = raycastHit.point;
				}
			}
		}

		public static void InfiniteJumpHandler()
		{
			VRCPlayerApi localPlayer = Networking.LocalPlayer;
			if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Boolean_0 && !localPlayer.IsPlayerGrounded())
			{
				Vector3 velocity = localPlayer.GetVelocity();
				velocity.y = localPlayer.GetJumpImpulse();
				localPlayer.SetVelocity(velocity);
			}
		}

		public static void JetPackHandler()
		{
			VRCPlayerApi localPlayer = Networking.LocalPlayer;
			if (Math.Abs(VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_0 - 1f) < 1f)
			{
				Vector3 velocity = localPlayer.GetVelocity();
				velocity.y = localPlayer.GetJumpImpulse();
				localPlayer.SetVelocity(velocity);
			}
		}

		public static void SpeedHandler()
		{
			VRCPlayerApi localPlayer = Networking.LocalPlayer;
			if (RuntimeConfig.isSpeed)
			{
				localPlayer.SetWalkSpeed(localPlayer.GetWalkSpeed() * 2f);
				localPlayer.SetRunSpeed(localPlayer.GetRunSpeed() * 2f);
				localPlayer.SetStrafeSpeed(localPlayer.GetStrafeSpeed() * 2f);
			}
			else
			{
				localPlayer.SetWalkSpeed(localPlayer.GetWalkSpeed() / 2f);
				localPlayer.SetRunSpeed(localPlayer.GetRunSpeed() / 2f);
				localPlayer.SetStrafeSpeed(localPlayer.GetStrafeSpeed() / 2f);
			}
		}

		public static void BeyBladeMode(bool state)
		{
			if (state)
			{
				_revertRotation = PlayerUtils.GetPlayerRotation();
				_revertVector = PlayerUtils.GetPlayerPosition();
				Transform transform = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
				transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
				Transform transform2 = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
				transform.position = transform2.position + new Vector3(0f, 2f, 0f);
			}
			else
			{
				PlayerUtils.SendToLocation(_revertVector, _revertRotation);
			}
		}

		public static void EnableDisableJumping(bool state)
		{
			GameObject gameObject = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject;
			if (state && gameObject.GetComponent<PlayerModComponentJump>() == null)
			{
				gameObject.AddComponent<PlayerModComponentJump>();
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 3f;
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 3f;
			}
			else
			{
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 3f;
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 3f;
			}
		}

		private static void Blink()
		{
			_lastBlinkTime = Time.time;
			Vector3 velocity = Networking.LocalPlayer.GetVelocity();
			Transform transform = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
			transform.position += transform.forward * _boostAmount;
			Networking.LocalPlayer.SetVelocity(velocity);
		}

		public static void OnUpdateBlink()
		{
			if (IsBlinkAllowed && Input.GetKeyDown(KeyCode.Space))
			{
				if (Time.time - _inputPressed < Configuration.GetConfig().dblTapSpacebar)
				{
					Blink();
				}
				_inputPressed = Time.time;
			}
		}

		public override void Update()
		{
			if (Application.isPlaying)
			{
				if (RuntimeConfig.basicfly)
				{
					HandleFlight();
				}
				if (RuntimeConfig.raycastTeleport)
				{
					RaycastTeleportHandler();
				}
				if (RuntimeConfig.infiniteJump)
				{
					InfiniteJumpHandler();
				}
				if (RuntimeConfig.jetPackJump)
				{
					JetPackHandler();
				}
			}
		}
	}
}
