using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.Utils
{
	public static class ImageUtils
	{
		public static Sprite ConsoleSprite;

		public static Sprite CatSmileSprite;

		public static Sprite CitraLogoSprite;

		public static Sprite PlayerListSprite;

		public static Sprite QMBackgroundSprite;

		public static Sprite PopupSprite;

		public static Sprite CitraMenuBtnSprite;

		public static Sprite CitraWingSprite;

		public static Sprite FPSSprite;

		public static Sprite CitraButtonSprite;

		public static Sprite CatSmileSpriteTwo;

		private static Sprite _coolCatSprite;

		private static Sprite _happyCatSprite;

		private static Sprite _pizzaCatSprite;

		private static Sprite _hoodieCatSprite;

		private static Sprite _karenCatSprite;

		public static void SetRandomCatSprite()
		{
			System.Random random = new System.Random();
			Sprite[] array = new Sprite[6] { _coolCatSprite, _happyCatSprite, _pizzaCatSprite, _hoodieCatSprite, _karenCatSprite, CatSmileSprite };
			int num = random.Next(array.Length);
			Sprite sprite = array[num];
			Image componentInChildren = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
			RawImage componentInChildren2 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
			if (componentInChildren2.enabled && componentInChildren2 != null)
			{
				componentInChildren2.enabled = false;
			}
			if (!(componentInChildren == null))
			{
				componentInChildren.enabled = true;
				componentInChildren.sprite = sprite;
			}
		}

		public static void SetSprites()
		{
			int num = 0;
			ConsoleSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/citra-console.png"));
			if (ConsoleSprite != null)
			{
				num++;
			}
			CatSmileSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/CatSmile.png"));
			if (CatSmileSprite != null)
			{
				num++;
			}
			CitraLogoSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/Citra-Logo.png"));
			if (CitraLogoSprite != null)
			{
				num++;
			}
			PlayerListSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/PlayerlistSprite1.png"));
			if (PlayerListSprite != null)
			{
				num++;
			}
			QMBackgroundSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/QM_BG.png"));
			if (QMBackgroundSprite != null)
			{
				num++;
			}
			PopupSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/popup.png"));
			if (PopupSprite != null)
			{
				num++;
			}
			CitraMenuBtnSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/Menuicon3.png"));
			if (CitraMenuBtnSprite != null)
			{
				num++;
			}
			CitraWingSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/Wing_BG.png"));
			if (CitraWingSprite != null)
			{
				num++;
			}
			FPSSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/popup.png"));
			if (FPSSprite != null)
			{
				num++;
			}
			_coolCatSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/coolcat.png"));
			if (_coolCatSprite != null)
			{
				num++;
			}
			_happyCatSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/happycat.png"));
			if (_happyCatSprite != null)
			{
				num++;
			}
			_pizzaCatSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/pizzacat.png"));
			if (_pizzaCatSprite != null)
			{
				num++;
			}
			_hoodieCatSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/hoodiecat.png"));
			if (_hoodieCatSprite != null)
			{
				num++;
			}
			_karenCatSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/karencat.png"));
			if (_karenCatSprite != null)
			{
				num++;
			}
			CitraButtonSprite = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/Citra_QuickMenuButton.png"));
			if (CitraButtonSprite != null)
			{
				num++;
			}
			CatSmileSpriteTwo = LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://citra.cc/no/cattrollVRC.png"));
			if (CatSmileSpriteTwo != null)
			{
				num++;
			}
			if (num == 16)
			{
				ConsoleUtils.OnLogSuccess("Loaded Sprites Successfully.");
			}
			else
			{
				ConsoleUtils.OnLogError("Error Occurred while loading Sprites");
			}
		}

		private static Sprite LoadSpriteFromByteArray(byte[] array)
		{
			if (array == null || array.Length == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(512, 512);
			if (!Il2CppImageConversionManager.LoadImage(texture2D, array))
			{
				return null;
			}
			Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0u, SpriteMeshType.FullRect, default(Vector4), generateFallbackPhysicsShape: false);
			sprite.hideFlags += 32;
			return sprite;
		}

		internal static Sprite CreateSpriteFromTex(Texture2D tex)
		{
			Sprite sprite = Sprite.CreateSprite(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f, 0u, SpriteMeshType.FullRect, default(Vector4), generateFallbackPhysicsShape: false);
			sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			return sprite;
		}

		public static Sprite LoadSprite(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return null;
			}
			byte[] array = new WebClient().DownloadData(url);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(512, 512);
			if (!Il2CppImageConversionManager.LoadImage(texture2D, array))
			{
				return null;
			}
			Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0u, SpriteMeshType.FullRect, default(Vector4), generateFallbackPhysicsShape: false);
			sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			return sprite;
		}

		public static void ChangeCatSprite(int type)
		{
			switch (type)
			{
			case 0:
			{
				Image componentInChildren13 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren14 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren14.enabled && componentInChildren14 != null)
				{
					componentInChildren14.enabled = false;
				}
				if (componentInChildren13 != null)
				{
					componentInChildren13.enabled = true;
					componentInChildren13.sprite = _coolCatSprite;
				}
				break;
			}
			case 1:
			{
				Image componentInChildren5 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren6 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren6.enabled && componentInChildren6 != null)
				{
					componentInChildren6.enabled = false;
				}
				if (componentInChildren5 != null)
				{
					componentInChildren5.enabled = true;
					componentInChildren5.sprite = _hoodieCatSprite;
				}
				break;
			}
			case 2:
			{
				Image componentInChildren3 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren4 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren4.enabled && componentInChildren4 != null)
				{
					componentInChildren4.enabled = false;
				}
				if (componentInChildren3 != null)
				{
					componentInChildren3.enabled = true;
					componentInChildren3.sprite = _pizzaCatSprite;
				}
				break;
			}
			case 3:
			{
				Image componentInChildren7 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren8 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren8.enabled && componentInChildren8 != null)
				{
					componentInChildren8.enabled = false;
				}
				if (componentInChildren7 != null)
				{
					componentInChildren7.enabled = true;
					componentInChildren7.sprite = _hoodieCatSprite;
				}
				break;
			}
			case 4:
			{
				Image componentInChildren9 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren10 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren10.enabled && componentInChildren10 != null)
				{
					componentInChildren10.enabled = false;
				}
				if (componentInChildren9 != null)
				{
					componentInChildren9.enabled = true;
					componentInChildren9.sprite = _karenCatSprite;
				}
				break;
			}
			case 5:
			{
				Image componentInChildren11 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren12 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren12.enabled && componentInChildren12 != null)
				{
					componentInChildren12.enabled = false;
				}
				if (componentInChildren11 != null)
				{
					componentInChildren11.enabled = true;
					componentInChildren11.sprite = CatSmileSprite;
				}
				break;
			}
			case 6:
			{
				Image componentInChildren = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren2 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				if (componentInChildren2.enabled && componentInChildren2 != null)
				{
					componentInChildren2.enabled = false;
				}
				if (componentInChildren != null)
				{
					componentInChildren.enabled = true;
					componentInChildren.sprite = CatSmileSpriteTwo;
				}
				break;
			}
			}
		}
	}
}
