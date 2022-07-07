using CitraClient.Modules.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace CitraClient.GUI.QMChanges
{
	public class VideoBG : ModuleBase
	{
		private const string SpaceUrl = "https://files.catbox.moe/f20yh6.mp4";

		private const string citraURL = "https://files.catbox.moe/2cba3i.mp4";

		private const string citraURL2 = "https://files.catbox.moe/bu4ujg.mp4";

		private const string colorfulURL = "https://files.catbox.moe/j5yh1e.mp4";

		private CanvasRenderer _canvasRenderer;

		private RawImage _rawImage;

		private RectTransform _rectTransform;

		private RenderTexture _renderTexture;

		private VideoPlayer _videoPlayer;

		private GameObject _vidUiObj;

		public void CreateVideo()
		{
			_renderTexture = new RenderTexture(512, 512, 16);
			if (_vidUiObj != null)
			{
				Object.Destroy(_vidUiObj.gameObject);
			}
			_vidUiObj = new GameObject("CITRA_VID_UI");
			_videoPlayer = _vidUiObj.AddComponent<VideoPlayer>();
			_rectTransform = _vidUiObj.AddComponent<RectTransform>();
			_canvasRenderer = _vidUiObj.AddComponent<CanvasRenderer>();
			_rawImage = _vidUiObj.AddComponent<RawImage>();
			_videoPlayer.url = "https://files.catbox.moe/f20yh6.mp4";
			_videoPlayer.playOnAwake = true;
			_videoPlayer.targetTexture = _renderTexture;
			_videoPlayer.aspectRatio = VideoAspectRatio.FitOutside;
			_videoPlayer.isLooping = true;
			_videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			_vidUiObj.GetComponent<RawImage>().texture = _renderTexture;
			Object.Destroy(GameObject.Find("UserInterface/MenuContent/Backdrop/Backdrop/Background"));
			Transform transform = GameObject.Find("UserInterface/MenuContent/Backdrop/Backdrop").transform;
			_vidUiObj.transform.position = transform.position;
			_vidUiObj.transform.parent = transform;
			_vidUiObj.transform.localScale = new Vector3(15.4f, 10f, 10f);
		}
	}
}
