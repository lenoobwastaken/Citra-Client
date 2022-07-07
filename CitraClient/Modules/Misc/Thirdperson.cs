using CitraClient.Modules.Base;
using UnityEngine;

namespace CitraClient.Modules.Misc
{
	public class Thirdperson : ModuleBase
	{
		public static int cameraMode;

		private static Camera fCamera;

		private static Camera bCamera;

		public override void QMLoaded()
		{
			fCamera = CreateCamera(1, 180f);
			bCamera = CreateCamera(2, 0f);
		}

		public override void Update()
		{
			HandleDistance();
		}

		public static Camera CreateCamera(int camMode, float rotation)
		{
			GameObject gameObject = new GameObject($"Citra-ThirdPersonCam-{camMode}");
			Transform transform = Camera.main.transform;
			gameObject.transform.parent = transform;
			gameObject.transform.rotation = transform.rotation;
			gameObject.transform.Rotate(new Vector3(0f, rotation, 0f));
			gameObject.transform.position = Camera.main.transform.position + -gameObject.transform.forward * 2f;
			Camera camera = gameObject.AddComponent<Camera>();
			camera.enabled = false;
			camera.fieldOfView = 90f;
			camera.nearClipPlane /= 4f;
			return camera;
		}

		public static void SetMode(int camMode)
		{
			cameraMode = camMode;
			switch (camMode)
			{
			case 0:
				fCamera.enabled = false;
				bCamera.enabled = false;
				break;
			case 1:
				fCamera.enabled = false;
				bCamera.enabled = true;
				break;
			case 2:
				fCamera.enabled = true;
				bCamera.enabled = false;
				break;
			}
		}

		public static void HandleDistance()
		{
			if (cameraMode != 0 && !(Camera.main == null))
			{
				float axis = Input.GetAxis("Mouse ScrollWheel");
				if (axis > 0f)
				{
					bCamera.transform.position -= fCamera.transform.forward * 0.1f;
					fCamera.transform.position += bCamera.transform.forward * 0.1f;
				}
				else if (axis < 0f)
				{
					bCamera.transform.position += fCamera.transform.forward * 0.1f;
					fCamera.transform.position -= bCamera.transform.forward * 0.1f;
				}
			}
		}
	}
}
