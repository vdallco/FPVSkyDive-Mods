extern alias harmony; 

using HarmonyLib;
using Orqa.SkyDive.Managers;
using MelonLoader;
using System;
//using LuaLoader;

namespace FPVSkyDiveInfiniteBounds
{
	public static class BuildInfo
	{
		public const string Name = "[FPV.SkyDive] Unlimited Map Bounds"; // required
		public const string Description = "Allows drones to fly higher and farther than the built-in limits"; // optional
		public const string Author = "johnbrowndidnothingwrong"; // required
		public const string Company = null; // optional
		public const string Version = "0.0.1"; // required
		public const string DownloadLink = null; // optional
	}
	public class FPVSkyDiveUnlimitedMapBounds : MelonMod
	{
		[harmony.HarmonyLib.HarmonyPatch(typeof(Orqa.SkyDive.Misc.MapBoundsController), "InitializeBoundsCollider")]
		class UnlimitedMapBoundsPatch
		{
			static bool Prefix()
			{
				harmony.HarmonyLib.Harmony.DEBUG = true;
				MelonLogger.Msg("[Harmony patch prefix] UnlimitedMapBoundsPatch: skipping InitializeBoundsCollider");
				return false; // skips FPV.SkyDives MapBoundsController.InitializeBoundsCollider() function
			}
		}
	}
}
