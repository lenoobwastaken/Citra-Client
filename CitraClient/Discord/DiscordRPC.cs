using System;
using System.Linq;
using System.Threading;
using System.Timers;
using CitraClient.Config;
using CitraClient.Utils;
using VRC.Core;

namespace CitraClient.Discord
{
	public static class DiscordRPC
	{
		private static DiscordRPCSetup.RichPresence presence;

		private static DiscordRPCSetup.EventHandlers eventHandlers;

		public static void Start()
		{
			eventHandlers = default(DiscordRPCSetup.EventHandlers);
			presence.details = "Starting VRChat";
			presence.state = "Loading...";
			presence.largeImageKey = "logo";
			presence.largeImageText = "Using Citra v1.0.1";
			presence.partySize = 0;
			presence.partyMax = 0;
			presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			DiscordRPCSetup.Initialize("975447052366512138", ref eventHandlers, autoRegister: true, "");
			DiscordRPCSetup.UpdatePresence(ref presence);
			new Thread((ThreadStart)delegate
			{
				System.Timers.Timer timer = new System.Timers.Timer(5000.0);
				timer.Elapsed += Update;
				timer.AutoReset = true;
				timer.Enabled = true;
			}).Start();
		}

		public static void Update(object sender, ElapsedEventArgs args)
		{
			if (PlayerUtils.GetLocalPlayer() != null)
			{
				presence.details = "Logged in as " + (Configuration.GetConfig().hideName ? "[Hidden]" : PlayerUtils.GetLocalAPIUser().displayName);
				presence.state = "in a " + RoomManager.field_Internal_Static_ApiWorldInstance_0.type.ToString() + " " + RoomManager.field_Internal_Static_ApiWorldInstance_0.world.name;
				switch (RoomManager.field_Internal_Static_ApiWorldInstance_0.region)
				{
				case NetworkRegion.US_West:
					presence.smallImageKey = "us";
					presence.smallImageText = "usw";
					break;
				case NetworkRegion.US_East:
					presence.smallImageKey = "us";
					presence.smallImageText = "use";
					break;
				case NetworkRegion.Europe:
					presence.smallImageKey = "eu";
					presence.smallImageText = "eu";
					break;
				case NetworkRegion.Japan:
					presence.smallImageKey = "jp";
					presence.smallImageText = "jp";
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				presence.partyId = RoomManager.field_Internal_Static_ApiWorldInstance_0.worldId;
				presence.partySize = PlayerUtils.GetPlayerManager().GetAllPlayers().Count();
			}
			else if (APIUser.CurrentUser != null)
			{
				presence.details = "Logged in as " + (Configuration.GetConfig().hideName ? "[Hidden]" : PlayerUtils.GetLocalAPIUser().displayName);
				presence.state = "Loading into a world";
				presence.smallImageKey = "small";
				presence.smallImageText = "";
			}
			DiscordRPCSetup.UpdatePresence(ref presence);
		}
	}
}
