using CitraClient.Modules.Base;
using CitraClient.Utils;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;

namespace CitraClient.Modules.ESP
{
	internal class TrustLevelESP : ModuleBase
	{
		private static HighlightsFXStandalone _friendsHighlights;

		private static HighlightsFXStandalone _veteranHighlights;

		private static HighlightsFXStandalone _trustedHighlights;

		private static HighlightsFXStandalone _knownHighlights;

		private static HighlightsFXStandalone _userHighlights;

		private static HighlightsFXStandalone _newUserHighlights;

		private static HighlightsFXStandalone _visitorHighlights;

		private static HighlightsFXStandalone _monkeyHighlights;

		private static HighlightsFXStandalone _questHighlights;

		private static readonly Color FriendsColor = Color.yellow;

		private static readonly Color VeteranColor = Color.yellow;

		private static readonly Color TrustedColor = Color.magenta;

		private static readonly Color KnownColor = new Color(1f, 0.5f, 0f, 1f);

		private static readonly Color UserColor = Color.blue;

		private static readonly Color NewUserColor = Color.green;

		private static readonly Color VisitorColor = Color.grey;

		private static readonly Color MonkeyColor = Color.black;

		private static readonly Color QuestColor = Color.red;

		public override void QMLoaded()
		{
			HighlightsFX field_Private_Static_HighlightsFX_ = HighlightsFX.field_Private_Static_HighlightsFX_0;
			_friendsHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_friendsHighlights.highlightColor = FriendsColor;
			_veteranHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_veteranHighlights.highlightColor = VeteranColor;
			_trustedHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_trustedHighlights.highlightColor = TrustedColor;
			_knownHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_knownHighlights.highlightColor = KnownColor;
			_userHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_userHighlights.highlightColor = UserColor;
			_newUserHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_newUserHighlights.highlightColor = NewUserColor;
			_visitorHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_visitorHighlights.highlightColor = VisitorColor;
			_monkeyHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_monkeyHighlights.highlightColor = MonkeyColor;
			_questHighlights = field_Private_Static_HighlightsFX_.gameObject.AddComponent<HighlightsFXStandalone>();
			_questHighlights.highlightColor = QuestColor;
		}

		public static void ToggleESP(bool enabled)
		{
			PlayerManager field_Private_Static_PlayerManager_ = PlayerManager.field_Private_Static_PlayerManager_0;
			if (!(field_Private_Static_PlayerManager_ == null))
			{
				List<VRC.Player>.Enumerator enumerator = PlayerUtils.GetAllPlayersToList().GetEnumerator();
				while (enumerator.MoveNext())
				{
					VRC.Player current = enumerator.Current;
					HighlightPlayer(current, enabled);
				}
			}
		}

		private static void HighlightPlayer(VRC.Player player, bool highlighted)
		{
			if (!player.field_Private_APIUser_0.IsSelf)
			{
				Transform transform = player.transform.Find("SelectRegion");
				if (!(transform == null))
				{
					GetHighlightsFX(player.prop_APIUser_0).Method_Public_Void_Renderer_Boolean_0(transform.GetComponent<Renderer>(), highlighted);
				}
			}
		}

		public static void HighlightQuest(VRC.Player player, bool highlighted)
		{
			APIUser aPIUser = ((player != null) ? player.prop_APIUser_0 : null);
			if (aPIUser != null && aPIUser.IsOnMobile)
			{
				Transform transform = player.transform.Find("SelectRegion");
				GetHighlightsFX(aPIUser).Method_Public_Void_Renderer_Boolean_0(transform.GetComponent<Renderer>(), highlighted);
			}
		}

		public static void HighlightTarget(VRC.Player player, bool highlighted)
		{
			Transform transform = player.transform.Find("SelectRegion");
			GetHighlightsFX(player.prop_APIUser_0).Method_Public_Void_Renderer_Boolean_0(transform.GetComponent<Renderer>(), highlighted);
		}

		private static HighlightsFXStandalone GetHighlightsFX(APIUser apiUser)
		{
			if (APIUser.IsFriendsWith(apiUser.id))
			{
				return _friendsHighlights;
			}
			if (apiUser.hasLegendTrustLevel)
			{
				return _veteranHighlights;
			}
			if (apiUser.hasVeteranTrustLevel)
			{
				return _trustedHighlights;
			}
			if (apiUser.hasTrustedTrustLevel)
			{
				return _knownHighlights;
			}
			if (apiUser.hasKnownTrustLevel)
			{
				return _userHighlights;
			}
			if (apiUser.hasBasicTrustLevel)
			{
				return _newUserHighlights;
			}
			if (apiUser.hasNegativeTrustLevel)
			{
				return _visitorHighlights;
			}
			return apiUser.hasNegativeTrustLevel ? _monkeyHighlights : null;
		}
	}
}
