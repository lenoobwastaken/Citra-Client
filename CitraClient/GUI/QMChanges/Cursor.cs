using System.Collections;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using UnityEngine;

namespace CitraClient.GUI.QMChanges
{
	public class Cursor : ModuleBase
	{
		private const float Speed = 1f;

		private readonly Color _startColor = Color.magenta;

		private readonly Color _endColor = Color.cyan;

		private Color _lerpedColor;

		private GameObject _helperObj;

		private float _startTime;

		public Color trailColor = new Color(1f, 0f, 0.38f);

		private readonly float distanceFromCamera = 5f;

		private readonly float startWidth = 0.05f;

		private readonly float endWidth = 0f;

		private readonly float trailTime = 0.24f;

		private GameObject _trailObj;

		private Transform _trailTransform;

		private Camera _mainCamera;

		private void StartTime()
		{
			_startTime = Time.time;
		}

		private void UpdateTrailRenderer()
		{
			MoveTrailToCursor(Input.mousePosition);
		}

		public void MoveTrailToCursor(Vector3 screenPosition)
		{
			_trailTransform.position = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
		}

		public void CreateCursorLineRenderer()
		{
			if (Camera.main != null)
			{
				_mainCamera = Camera.main;
			}
			if (_trailObj != null)
			{
				Object.Destroy(_trailObj.gameObject);
			}
			_trailObj = new GameObject("CITRA_MOUSE_TRAIL");
			_trailTransform = _trailObj.transform;
			TrailRenderer trailRenderer = _trailObj.AddComponent<TrailRenderer>();
			trailRenderer.time = -1f;
			MoveTrailToCursor(Input.mousePosition);
			trailRenderer.time = trailTime;
			trailRenderer.startWidth = startWidth;
			trailRenderer.endWidth = endWidth;
			trailRenderer.numCapVertices = 2;
			trailRenderer.sharedMaterial = new Material(Shader.Find("Unlit/Color"))
			{
				color = trailColor
			};
		}

		private void UpdateCursor()
		{
			float t = Mathf.Sin(Time.time - _startTime) * 1f;
			if (_helperObj != null)
			{
				Color color2 = (_lerpedColor = (_helperObj.GetComponent<SpriteRenderer>().color = Color.Lerp(_startColor, _endColor, t)));
			}
		}

		private IEnumerator FindCursor()
		{
			while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null || !WorldUtils.IsInWorld())
			{
				yield return null;
			}
			_helperObj = GameObject.Find("_Application/CursorManager/MouseArrow/VRCUICursorIcon");
			_helperObj.GetComponent<SpriteRenderer>().color = _lerpedColor;
		}

		public override void Start()
		{
			StartTime();
		}

		public override void Update()
		{
			if (!(_helperObj == null))
			{
				UpdateCursor();
				UpdateTrailRenderer();
			}
		}

		public override void OnSceneLoad(int buildIndex, string sceneName)
		{
		}
	}
}
