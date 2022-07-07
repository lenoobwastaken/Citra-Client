using System;
using CitraClient.Config;
using CitraClient.Utils;
using UnityEngine;
using UnityEngine.XR;
using VRC.SDKBase;

namespace CitraClient.Modules.Movement
{
	public class MovementComponent : MonoBehaviour
	{
		private const string THUMBSTICK_AXIS = "Oculus_CrossPlatform_SecondaryThumbstickVertical";

		private static VRCPlayer _local;

		private static Quaternion _revert;

		public CharacterController characterController;

		public Camera mainCamera;

		public bool inVR;

		public MovementComponent(IntPtr ptr)
			: base(ptr)
		{
		}

		private void Start()
		{
			inVR = XRDevice.isPresent;
			mainCamera = Camera.main;
			characterController = base.gameObject.GetComponent<CharacterController>();
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				if (RuntimeConfig.noclip && characterController.enabled && (RuntimeConfig.directionalFly || RuntimeConfig.basicfly))
				{
					characterController.enabled = false;
				}
				if (RuntimeConfig.basicfly)
				{
					Physics.gravity = Vector3.zero;
					BasicFlyHandler();
				}
				else if (RuntimeConfig.directionalFly)
				{
					Physics.gravity = Vector3.zero;
					DirectionalFlyHandler();
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

		public void BasicFlyHandler()
		{
			float num = Configuration.GetConfig().flySpeed;
			Networking.LocalPlayer.SetVelocity(Vector3.zero);
			if (Input.GetKey(KeyCode.LeftShift))
			{
				num *= 2f;
			}
			if (Input.GetAxis("Vertical") != 0f)
			{
				base.transform.position += base.transform.forward * (num * Time.deltaTime * Input.GetAxis("Vertical"));
			}
			if (Input.GetAxis("Horizontal") != 0f)
			{
				base.transform.position += base.transform.right * (num * Time.deltaTime * Input.GetAxis("Horizontal"));
			}
			if (Input.GetKey(KeyCode.E))
			{
				Transform transform = base.transform;
				transform.position += transform.up * (num * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.Q))
			{
				Transform transform2 = base.transform;
				transform2.position -= transform2.up * (num * Time.deltaTime);
			}
			if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") != 0f)
			{
				base.transform.position += base.transform.up * (num * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical"));
			}
		}

		public void DirectionalFlyHandler()
		{
			float num = Configuration.GetConfig().flySpeed;
			Networking.LocalPlayer.SetVelocity(Vector3.zero);
			if (Input.GetKey(KeyCode.LeftShift))
			{
				num *= 2f;
			}
			if (Input.GetAxis("Vertical") != 0f)
			{
				base.transform.position += mainCamera.transform.forward * (num * Time.deltaTime * Input.GetAxis("Vertical"));
			}
			if (Input.GetAxis("Horizontal") != 0f)
			{
				base.transform.position += mainCamera.transform.right * (num * Time.deltaTime * Input.GetAxis("Horizontal"));
			}
			if (Input.GetKey(KeyCode.E))
			{
				base.transform.position += mainCamera.transform.up * (num * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.Q))
			{
				base.transform.position -= mainCamera.transform.up * (num * Time.deltaTime);
			}
		}

		public void RaycastTeleportHandler()
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
			_local = PlayerUtils.GetLocalPlayer();
			if (state)
			{
				_revert = PlayerUtils.GetPlayerRotation();
				_local.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			}
			else
			{
				_local.transform.rotation = _revert;
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
			if (!state && gameObject.GetComponent<PlayerModComponentJump>() != null)
			{
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 0f;
				gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 0f;
				UnityEngine.Object.Destroy(gameObject.GetComponent<PlayerModComponentJump>());
			}
		}
	}
}
