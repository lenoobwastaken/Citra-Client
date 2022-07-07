using System;
using System.Collections;
using System.Collections.Generic;
using CitraClient.Config;
using CitraClient.Utils;
using CitraClient.Utils.PhotonUtils;
using Harmony;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;

namespace CitraClient.CitraNamePlates
{
	public class CustomPlates
	{
		public static CustomPlates instance;

		public static HarmonyInstance harmony;

		private static Sprite nameplateBGBackup;

		private static AssetBundle bundle;

		private static Material npUIMaterial;

		private static Sprite nameplateOutline;

		public static string animatedTagDeveloper;

		public static Color PlateUser = new Color(ColorManager.UserR, ColorManager.UserG, ColorManager.UserB);

		public static Color PlateLegend = new Color(0.3f, 1f, 1f);

		public static Color PlateKnown = new Color(ColorManager.KnownR, ColorManager.KnownG, ColorManager.KnownB);

		public static Color PlateNegative = new Color(ColorManager.NuisanceR, ColorManager.NuisanceG, ColorManager.NuisanceB);

		public static Color PlateNewUser = new Color(ColorManager.NewUserR, ColorManager.NewUserG, ColorManager.NewUserB);

		public static Color PlateVisitor = new Color(ColorManager.VisitorsR, ColorManager.VisitorsG, ColorManager.VisitorsB);

		public static Color PlateTrusted = new Color(ColorManager.TrustedR, ColorManager.TrustedG, ColorManager.TrustedB);

		public static Color PlateFriend = new Color(ColorManager.FriendR, ColorManager.FriendG, 0f);

		public static Color PlateVeteran = new Color(0.9f, 0f, 0.75f);

		private static System.Collections.Generic.List<string> hiddenNameplateUserIDs = new System.Collections.Generic.List<string>();

		public static System.Collections.Generic.List<GameObject> AllNameplates = new System.Collections.Generic.List<GameObject>();

		public static System.Collections.Generic.List<(string, string)> CustomNameTags = new System.Collections.Generic.List<(string, string)>();

		public static void Initialize()
		{
			ClassInjector.RegisterTypeInIl2Cpp<Object>();
			Update().Start();
		}

		public static IEnumerator Update()
		{
			while (true)
			{
				if (!VRCPlayer.field_Internal_Static_VRCPlayer_0)
				{
					yield return null;
				}
				yield return new WaitForSeconds(0.15f);
				try
				{
					Il2CppSystem.Collections.Generic.List<Player>.Enumerator enumerator = PlayerUtils.GetAllPlayersToList().GetEnumerator();
					while (enumerator.MoveNext())
					{
						Player player = enumerator.Current;
						player.transform.FindChild("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats").gameObject.SetActive(value: false);
						Transform transform = player._vrcplayer.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents");
						Transform sTS = transform.Find("Quick Stats");
						string text = GetRank(player.prop_APIUser_0).ToLower();
						Color color2 = default(Color);
						if (false)
						{
							continue;
						}
						if (!APIUser.IsFriendsWith(player.field_Private_APIUser_0.id))
						{
							if (1 == 0)
							{
							}
							Color color3 = text switch
							{
								"user" => PlateUser, 
								"legend" => PlateLegend, 
								"known" => PlateKnown, 
								"negativetrust" => PlateNegative, 
								"new user" => PlateNewUser, 
								"verynegativetrust" => PlateNegative, 
								"visitor" => PlateVisitor, 
								"trusted" => PlateTrusted, 
								"veteran" => PlateVeteran, 
								_ => color2, 
							};
							if (1 == 0)
							{
							}
							color2 = color3;
						}
						else
						{
							color2 = PlateFriend;
						}
						int currentId = player.prop_Player_1.GetPhotonID();
						string text2 = $"Actor ID: {currentId} | [{GetRank(player.field_Private_APIUser_0)}] | F: {PlayerUtils.GetFPSColored(player._vrcplayer)} | P: {PlayerUtils.GetPingColored(player._vrcplayer)} ";
						if (player.field_Private_VRCPlayerApi_0.IsUserInVR())
						{
							text2 += "| <color=green>[VR]</color> ";
						}
						if (player.field_Private_APIUser_0.IsOnMobile)
						{
							text2 += "| <color=green>[Q]</color> ";
						}
						if (player.field_Private_APIUser_0.isSupporter)
						{
							text2 += "| <color=yellow>[VRC+]</color> ";
						}
						int pos = 33;
						if (player.field_Private_VRCPlayerApi_0.isMaster)
						{
							UpdateT(1, sTS, transform, Color.black, "<color=red>Master</color>", pos);
							pos += 33;
						}
						switch (player.prop_APIUser_0.id)
						{
						case "usr_cf45a4de-a115-4e8c-98eb-49d53004df7b":
							UpdateT(2, sTS, transform, Color.black, "<color=green>KOS</color>", pos);
							break;
						case "usr_7d310a5b-4a64-4a3e-8730-add841d24efd":
							UpdateT(3, sTS, transform, Color.black, "<color=green>Staff</color>", pos);
							break;
						case "usr_36854f8e-a1bf-43d7-adb5-ebdf02ea6b1e":
							UpdateT(4, sTS, transform, Color.black, "<color=green>Staff</color>", pos);
							break;
						}
						UpdateT(0, sTS, transform, color2, text2, 0f);
					}
				}
				catch
				{
				}
			}
		}

		public static void Update(VRCPlayer Player)
		{
			Player._player._vrcplayer.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents");
			if (!(Player == null) && Player.isActiveAndEnabled && !(Player.field_Internal_Animator_0 == null) && !(Player.field_Internal_GameObject_0 == null))
			{
				int num = Player.field_Internal_GameObject_0.name.IndexOf("Avatar_Utility_Base_", StringComparison.Ordinal);
			}
			Object @object = Player.field_Public_PlayerNameplate_0.GetComponent<Object>();
			if (@object == null)
			{
				@object = Player.field_Public_PlayerNameplate_0.gameObject.AddComponent<Object>();
				@object.SN(Player.field_Public_PlayerNameplate_0);
				@object.Content = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents").gameObject;
				@object.IconBackground = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon/Background").GetComponent<Image>();
				@object.Icon = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon/User Image").GetComponent<RawImage>();
				@object.IconContainer = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon").gameObject;
				@object.NameBackground = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Main/Background").GetComponent<ImageThreeSlice>();
				@object.QuickStatsBackground = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Quick Stats").GetComponent<ImageThreeSlice>();
				@object.QuickStats = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Quick Stats").gameObject;
				@object.Name = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Main/Text Container/Name").GetComponent<TextMeshProUGUI>();
				@object.IconPulse = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon/Pulse").GetComponent<Image>();
				@object.NamePulse = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Main/Pulse").GetComponent<ImageThreeSlice>();
				@object.IconGlow = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon/Glow").GetComponent<Image>();
				@object.NameGlow = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Main/Glow").GetComponent<ImageThreeSlice>();
				@object.IconText = Player.field_Public_PlayerNameplate_0.gameObject.transform.Find("Contents/Icon/Initials").GetComponent<TextMeshProUGUI>();
			}
			Reset(Player.field_Public_PlayerNameplate_0);
			ImageThreeSlice nameBackground = @object.NameBackground;
			if (nameBackground != null)
			{
				if (nameplateBGBackup == null)
				{
					nameplateBGBackup = nameBackground._sprite;
				}
				nameBackground._sprite = nameplateOutline;
			}
			Color? color = null;
			Color? textColor = null;
			bool reset = false;
			Transform transform = @object.QuickStats.transform;
			int ID = 0;
			ST(ref ID, transform, @object.Content.transform, Color.white, "F: " + PlayerUtils.GetFPSColored(Player) + " P: " + PlayerUtils.GetPingColored(Player), 0f);
			string text = GetRank(Player._player.field_Private_APIUser_0).ToLower();
			if (!APIUser.IsFriendsWith(Player._player.field_Private_APIUser_0.id))
			{
				switch (text)
				{
				case "user":
					color = PlateUser;
					textColor = PlateUser;
					break;
				case "legend":
					color = PlateLegend;
					textColor = PlateLegend;
					break;
				case "known":
					color = PlateKnown;
					textColor = PlateKnown;
					break;
				case "negativetrust":
					color = PlateNegative;
					textColor = PlateNegative;
					break;
				case "new user":
					color = PlateNewUser;
					textColor = PlateNewUser;
					break;
				case "verynegativetrust":
					color = PlateNegative;
					textColor = PlateNegative;
					break;
				case "visitor":
					color = PlateVisitor;
					textColor = PlateVisitor;
					break;
				case "trusted":
					color = PlateTrusted;
					textColor = PlateTrusted;
					break;
				case "veteran":
					color = PlateVeteran;
					textColor = PlateVeteran;
					break;
				}
			}
			else
			{
				color = PlateFriend;
				textColor = PlateFriend;
			}
			SetColor(@object, color, color, textColor, null, reset);
		}

		internal static Transform MT(Transform STS, int I, float y)
		{
			Transform transform = UnityEngine.Object.Instantiate(STS, STS.parent, worldPositionStays: false);
			transform.name = $"Citra:{I}";
			transform.gameObject.active = true;
			Transform result = null;
			for (int num = transform.childCount; num > 0; num--)
			{
				Transform child = transform.GetChild(num - 1);
				if (child.name == "Trust Text")
				{
					result = child;
				}
				else
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
			float namePlateHeight = Configuration.GetConfig().namePlateHeight;
			transform.localPosition = new Vector3(0f, namePlateHeight + y, 0f);
			return result;
		}

		internal static void ST(ref int ID, Transform STS, Transform ObjectT, Color Color, string Text, float y)
		{
			Transform transform = ObjectT.Find($"Citra:{ID}");
			Transform transform2;
			if (transform == null)
			{
				transform2 = MT(STS, ID, y);
			}
			else
			{
				transform.gameObject.SetActive(value: true);
				transform2 = transform.Find("Trust Text");
			}
			TextMeshProUGUI component = transform2.GetComponent<TextMeshProUGUI>();
			component.color = Color;
			component.text = Text;
			ID++;
		}

		internal static void UpdateT(int ID, Transform STS, Transform ObjectT, Color Color, string Text, float y)
		{
			Transform transform = ObjectT.Find($"Citra:{ID}");
			Transform transform2;
			if (transform == null)
			{
				transform2 = MT(STS, ID, y);
			}
			else
			{
				transform.gameObject.SetActive(value: true);
				transform2 = transform.Find("Trust Text");
			}
			TextMeshProUGUI component = transform2.GetComponent<TextMeshProUGUI>();
			transform.GetComponent<ImageThreeSlice>().color = Color;
			component.color = Color;
			component.richText = true;
			component.text = Text;
		}

		private static void Reset(PlayerNameplate nameplate)
		{
			Object component = nameplate.gameObject.GetComponent<Object>();
			if (component != null)
			{
				component.Reset();
			}
			if (nameplateBGBackup != null && component != null)
			{
				ImageThreeSlice component2 = component.NameBackground.GetComponent<ImageThreeSlice>();
				if (component2 != null)
				{
					component2._sprite = nameplateBGBackup;
				}
			}
			SetColor(component, Color.white, Color.white, null, null, Reset: true);
		}

		public static void Refresh(PlayerNameplate nameplate)
		{
			Update(nameplate.field_Private_VRCPlayer_0);
			Object component = nameplate.gameObject.GetComponent<Object>();
			if (component != null)
			{
				component.Reset();
			}
		}

		private static void SetColor(Object Nameplate, Color? Color = null, Color? IconColor = null, Color? TextColor = null, Color? Lerp = null, bool Reset = false, bool IsDelusionUser = false)
		{
			if (!(Nameplate == null))
			{
				if (!Reset)
				{
					Nameplate.NameBackground.material = npUIMaterial;
					Nameplate.QuickStatsBackground.material = npUIMaterial;
					Nameplate.IconBackground.material = npUIMaterial;
				}
				else
				{
					Nameplate.NameBackground.material = null;
					Nameplate.QuickStatsBackground.material = null;
					Nameplate.IconBackground.material = null;
				}
				Color color = Nameplate.NameBackground.color;
				Color color2 = Nameplate.IconBackground.color;
				Color color3 = Nameplate.QuickStatsBackground.color;
				Color color4 = Nameplate.Name.faceColor;
				if (Color.HasValue)
				{
					Color value = Color.Value;
					Color value2 = Color.Value;
					value.a = color.a;
					value2.a = color3.a;
					Nameplate.NameBackground.color = value / 2f;
					Nameplate.QuickStatsBackground.color = value2;
				}
				if (IconColor.HasValue && Color.HasValue)
				{
					Color value3 = Color.Value;
					value3.a = color2.a;
					Nameplate.IconBackground.color = value3;
				}
				if (TextColor.HasValue && !Lerp.HasValue)
				{
					Color value4 = TextColor.Value;
					value4.a = color4.a;
					Nameplate.SNC(value4);
					Nameplate.ORbld();
				}
				if (TextColor.HasValue && Lerp.HasValue)
				{
					Color value5 = TextColor.Value;
					Color value6 = Lerp.Value;
					value5.a = color4.a;
					value6.a = color4.a;
				}
				if (IsDelusionUser)
				{
					Nameplate.IsDelusion = true;
					ColorUtility.TryParseHtmlString("#ff0062", out var color5);
					Nameplate.IconPulse.color = color5;
					Nameplate.IconGlow.color = UnityEngine.Color.black * 2f;
					Nameplate.NamePulse.color = color5;
					Nameplate.NameGlow.color = UnityEngine.Color.black * 2f;
				}
			}
		}

		private static void SetIcon(Texture texture, PlayerNameplate nameplate, Object helper, Player player)
		{
			helper.IconBackground.enabled = true;
			helper.Icon.enabled = true;
			helper.IconContainer.SetActive(value: true);
			helper.Icon.texture = texture;
		}

		public static IEnumerator AnimateDeveloperTag()
		{
			while (true)
			{
				animatedTagDeveloper = "D";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "De";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Dev";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Deve";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Devel";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develo";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develop";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develope";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Developer";
				yield return new WaitForSeconds(3f);
				animatedTagDeveloper = "Develope";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develop";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develo";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Devel";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Deve";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Dev";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "De";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "D";
				yield return new WaitForSeconds(1f);
			}
		}

		public static IEnumerator AnimateStaffTag()
		{
			while (true)
			{
				animatedTagDeveloper = "S";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "De";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Dev";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Deve";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Devel";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develo";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develop";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develope";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Developer";
				yield return new WaitForSeconds(3f);
				animatedTagDeveloper = "Develope";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develop";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Develo";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Devel";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Deve";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "Dev";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "De";
				yield return new WaitForSeconds(1f);
				animatedTagDeveloper = "D";
				yield return new WaitForSeconds(1f);
			}
		}

		public static string GetRank(APIUser instance)
		{
			return (instance.hasModerationPowers || instance.tags.Contains("admin_moderator")) ? "Moderation User" : ((instance.hasSuperPowers || instance.tags.Contains("admin_")) ? "Admin User" : ((instance.tags.Contains("system_legend") && instance.tags.Contains("system_trust_legend") && instance.tags.Contains("system_trust_trusted")) ? "Legend" : ((instance.hasLegendTrustLevel || (instance.tags.Contains("system_trust_legend") && instance.tags.Contains("system_trust_trusted"))) ? "Veteran" : (instance.hasVeteranTrustLevel ? "Trusted" : (instance.hasTrustedTrustLevel ? "Known" : (instance.hasKnownTrustLevel ? "User" : ((instance.hasBasicTrustLevel || instance.isNewUser) ? "New User" : (instance.hasNegativeTrustLevel ? "NegativeTrust" : (instance.hasVeryNegativeTrustLevel ? "VeryNegativeTrust" : "Visitor")))))))));
		}
	}
}
