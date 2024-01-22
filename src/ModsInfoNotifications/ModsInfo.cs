extern alias harmony;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace FPVSkyDiveModsInfo
{ 
	public static class BuildInfo
	{
		public const string Name = "[FPV.SkyDive] Mods Info"; // required
		public const string Description = "Displays information about creating and using mods on main menu"; // optional
		public const string Author = "johnbrowndidnothingwrong"; // required
		public const string Company = null; // optional
		public const string Version = "0.0.1"; // required
		public const string DownloadLink = null; // optional
	}

	public class ModsInfo : MelonMod
    {
		[harmony.HarmonyLib.HarmonyPatch(typeof(Orqa.SkyDive.UI.Controllers.MainMenuButtonListener), "Start")]
		class ModsInfoPatch
		{
			static void Postfix(UnityEngine.GameObject ____prefabNotificationTile, UnityEngine.Transform ____transformNotificationArea)
			{
				harmony.HarmonyLib.Harmony.DEBUG = true;
				MelonLogger.Msg("[Harmony patch prefix] LoadTitleNewsPatch: trying to modify titleNewsItem");
				PlayFab.ClientModels.TitleNewsItem tni = new PlayFab.ClientModels.TitleNewsItem();
				tni.Body = "{\"Title\":\"Mods are here!\",\"NotificationDescription\":\"Learn more to find out how to create and use mods\",\"NotificationUrl\":\"https://github.com/vdallco/FPVSkyDive-Mods\"}";
				tni.Title = "Mods are here!";
				tni.NewsId = "12345";
				tni.Timestamp = DateTime.Now;

				PlayFab.ClientModels.TitleNewsItem tni2 = new PlayFab.ClientModels.TitleNewsItem();
				tni2.Body = "{\"Title\":\"Mods\",\"NotificationDescription\":\"New drones, maps, UI's, gamemodes, etc are all possible with mods\",\"NotificationUrl\":\"https://github.com/vdallco/FPVSkyDive-Mods\"}";
				tni2.Title = "Customize FPV.SkyDive";
				tni2.NewsId = "12346";
				tni2.Timestamp = DateTime.Now;

				UnityEngine.Object.Instantiate<UnityEngine.GameObject>(____prefabNotificationTile, ____transformNotificationArea).GetComponent<Orqa.SkyDive.NotificationTile>().UpdateTile(tni);
				UnityEngine.Object.Instantiate<UnityEngine.GameObject>(____prefabNotificationTile, ____transformNotificationArea).GetComponent<Orqa.SkyDive.NotificationTile>().UpdateTile(tni2);
			}
		}
	}
}
