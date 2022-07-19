using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.Utils
{
	public static class ImageUtils
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000035D4 File Offset: 0x000017D4
		public static void SetRandomCatSprite()
		{
			System.Random random = new System.Random();
			Sprite[] array = new Sprite[]
			{
				ImageUtils._coolCatSprite,
				ImageUtils._happyCatSprite,
				ImageUtils._pizzaCatSprite,
				ImageUtils._hoodieCatSprite,
				ImageUtils._karenCatSprite,
				ImageUtils.CatSmileSprite,
				ImageUtils.CatSmileSpriteTwo
			};
			int num = random.Next(array.Length);
			Sprite sprite = array[num];
			Image componentInChildren = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
			RawImage componentInChildren2 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
			bool flag = componentInChildren2.enabled && componentInChildren2 != null;
			if (flag)
			{
				componentInChildren2.enabled = false;
			}
			bool flag2 = componentInChildren == null;
			if (!flag2)
			{
				componentInChildren.enabled = true;
				componentInChildren.sprite = sprite;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000369C File Offset: 0x0000189C
		public static void SetSprites()
		{
			int num = 0;
			ImageUtils.ConsoleSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/citra-console.png"));
			bool flag = ImageUtils.ConsoleSprite != null;
			if (flag)
			{
				num++;
			}
			ImageUtils.CatSmileSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/CatSmile.png"));
			bool flag2 = ImageUtils.CatSmileSprite != null;
			if (flag2)
			{
				num++;
			}
			ImageUtils.CitraLogoSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/Citra-Logo.png"));
			bool flag3 = ImageUtils.CitraLogoSprite != null;
			if (flag3)
			{
				num++;
			}
			ImageUtils.PlayerListSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/PlayerlistSprite1.png"));
			bool flag4 = ImageUtils.PlayerListSprite != null;
			if (flag4)
			{
				num++;
			}
			ImageUtils.QMBackgroundSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/QM_BG.png"));
			bool flag5 = ImageUtils.QMBackgroundSprite != null;
			if (flag5)
			{
				num++;
			}
			ImageUtils.PopupSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/popup.png"));
			bool flag6 = ImageUtils.PopupSprite != null;
			if (flag6)
			{
				num++;
			}
			ImageUtils.CitraMenuBtnSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/Menuicon3.png"));
			bool flag7 = ImageUtils.CitraMenuBtnSprite != null;
			if (flag7)
			{
				num++;
			}
			ImageUtils.CitraWingSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/Wing_BG.png"));
			bool flag8 = ImageUtils.CitraWingSprite != null;
			if (flag8)
			{
				num++;
			}
			ImageUtils.FPSSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/popup.png"));
			bool flag9 = ImageUtils.FPSSprite != null;
			if (flag9)
			{
				num++;
			}
			ImageUtils._coolCatSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/coolcat.png"));
			bool flag10 = ImageUtils._coolCatSprite != null;
			if (flag10)
			{
				num++;
			}
			ImageUtils._happyCatSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/happycat.png"));
			bool flag11 = ImageUtils._happyCatSprite != null;
			if (flag11)
			{
				num++;
			}
			ImageUtils._pizzaCatSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/pizzacat.png"));
			bool flag12 = ImageUtils._pizzaCatSprite != null;
			if (flag12)
			{
				num++;
			}
			ImageUtils._hoodieCatSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/hoodiecat.png"));
			bool flag13 = ImageUtils._hoodieCatSprite != null;
			if (flag13)
			{
				num++;
			}
			ImageUtils._karenCatSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/karencat.png"));
			bool flag14 = ImageUtils._karenCatSprite != null;
			if (flag14)
			{
				num++;
			}
			ImageUtils.CitraButtonSprite = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/Citra_QuickMenuButton.png"));
			bool flag15 = ImageUtils.CitraButtonSprite != null;
			if (flag15)
			{
				num++;
			}
			ImageUtils.CatSmileSpriteTwo = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/cattrollVRC.png"));
			bool flag16 = ImageUtils.CatSmileSpriteTwo != null;
			if (flag16)
			{
				num++;
			}
			ImageUtils.DiscordLogo = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/DiscordLogo.png"));
			bool flag17 = ImageUtils.DiscordLogo != null;
			if (flag17)
			{
				num++;
			}
			ImageUtils.Preview_One = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/city.png"));
			bool flag18 = ImageUtils.Preview_One != null;
			if (flag18)
			{
				num++;
			}
			ImageUtils.Preview_Two = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/deepspace.png"));
			bool flag19 = ImageUtils.Preview_Two != null;
			if (flag19)
			{
				num++;
			}
			ImageUtils.Preview_Three = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/earth.png"));
			bool flag20 = ImageUtils.Preview_Three != null;
			if (flag20)
			{
				num++;
			}
			ImageUtils.Preview_Four = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/fractal.png"));
			bool flag21 = ImageUtils.Preview_Four != null;
			if (flag21)
			{
				num++;
			}
			ImageUtils.Preview_Five = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/lights.png"));
			bool flag22 = ImageUtils.Preview_Five != null;
			if (flag22)
			{
				num++;
			}
			ImageUtils.Preview_Six = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/saturn.png"));
			bool flag23 = ImageUtils.Preview_Six != null;
			if (flag23)
			{
				num++;
			}
			ImageUtils.Preview_Seven = ImageUtils.LoadSpriteFromByteArray(WebUtils.ImageToByteArray("https://wtfzammu.xyz/no/trippy.png"));
			bool flag24 = ImageUtils.Preview_Seven != null;
			if (flag24)
			{
				num++;
			}
			ConsoleUtils.OnLogInfo((num == 24) ? "Loaded Sprites Successfully." : "An Error Occurred while loading Sprites");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003A98 File Offset: 0x00001C98
		private static Sprite LoadSpriteFromByteArray(byte[] array)
		{
			bool flag = array == null || array.Length == 0;
			Sprite result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Texture2D texture2D = new Texture2D(512, 512);
				bool flag2 = !Il2CppImageConversionManager.LoadImage(texture2D, array);
				if (flag2)
				{
					result = null;
				}
				else
				{
					Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect, default(Vector4), false);
					sprite.hideFlags += 32;
					result = sprite;
				}
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B40 File Offset: 0x00001D40
		internal static Sprite CreateSpriteFromTex(Texture2D tex)
		{
			Sprite sprite = Sprite.CreateSprite(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect, default(Vector4), false);
			sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			return sprite;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static Sprite LoadSprite(string url)
		{
			bool flag = string.IsNullOrEmpty(url);
			Sprite result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = new WebClient().DownloadData(url);
				bool flag2 = array == null || array.Length == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Texture2D texture2D = new Texture2D(512, 512);
					bool flag3 = !Il2CppImageConversionManager.LoadImage(texture2D, array);
					if (flag3)
					{
						result = null;
					}
					else
					{
						Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect, default(Vector4), false);
						sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
						result = sprite;
					}
				}
			}
			return result;
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

		// Token: 0x0600005A RID: 90 RVA: 0x00003C84 File Offset: 0x00001E84
		public static void ChangeCatSprites(Sprite sprite)
		{
			bool flag = sprite == null;
			if (!flag)
			{
				Image componentInChildren = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<Image>();
				RawImage componentInChildren2 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/ThankYouCharacter/Character").GetComponentInChildren<RawImage>();
				bool flag2 = componentInChildren2.enabled && componentInChildren2 != null;
				if (flag2)
				{
					componentInChildren2.enabled = false;
				}
				bool flag3 = componentInChildren != null;
				if (flag3)
				{
					componentInChildren.enabled = true;
					componentInChildren.sprite = sprite;
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CFC File Offset: 0x00001EFC
		public static IEnumerator ImageArrayGif(int imageUrls)
		{
			switch (imageUrls)
			{
				case 0:
					yield return null;
					break;
				case 1:
					yield return null;
					break;
				case 2:
					yield return null;
					break;
				case 3:
					yield return null;
					break;
				case 4:
					yield return null;
					break;
			}
			yield break;
		}

		// Token: 0x04000025 RID: 37
		public static Sprite ConsoleSprite;

		// Token: 0x04000026 RID: 38
		public static Sprite CatSmileSprite;

		// Token: 0x04000027 RID: 39
		public static Sprite CitraLogoSprite;

		// Token: 0x04000028 RID: 40
		public static Sprite PlayerListSprite;

		// Token: 0x04000029 RID: 41
		public static Sprite QMBackgroundSprite;

		// Token: 0x0400002A RID: 42
		public static Sprite PopupSprite;

		// Token: 0x0400002B RID: 43
		public static Sprite CitraMenuBtnSprite;

		// Token: 0x0400002C RID: 44
		public static Sprite CitraWingSprite;

		// Token: 0x0400002D RID: 45
		public static Sprite FPSSprite;

		// Token: 0x0400002E RID: 46
		public static Sprite CitraButtonSprite;

		// Token: 0x0400002F RID: 47
		public static Sprite CatSmileSpriteTwo;

		// Token: 0x04000030 RID: 48
		public static Sprite DiscordLogo;

		// Token: 0x04000031 RID: 49
		public static Sprite Preview_One;

		// Token: 0x04000032 RID: 50
		public static Sprite Preview_Two;

		// Token: 0x04000033 RID: 51
		public static Sprite Preview_Three;

		// Token: 0x04000034 RID: 52
		public static Sprite Preview_Four;

		// Token: 0x04000035 RID: 53
		public static Sprite Preview_Five;

		// Token: 0x04000036 RID: 54
		public static Sprite Preview_Six;

		// Token: 0x04000037 RID: 55
		public static Sprite Preview_Seven;

		// Token: 0x04000038 RID: 56
		public static Sprite Preview_Eight;

		// Token: 0x04000039 RID: 57
		private static Sprite _coolCatSprite;

		// Token: 0x0400003A RID: 58
		private static Sprite _happyCatSprite;

		// Token: 0x0400003B RID: 59
		private static Sprite _pizzaCatSprite;

		// Token: 0x0400003C RID: 60
		private static Sprite _hoodieCatSprite;

		// Token: 0x0400003D RID: 61
		private static Sprite _karenCatSprite;
	}

}
