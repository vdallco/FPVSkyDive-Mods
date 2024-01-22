extern alias harmony;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Orqa;
using RealDrone;

namespace FPVSkyDiveRealisticWhoopGravity
{
	public static class BuildInfo
	{
		public const string Name = "[FPV.SkyDive] Realistic Whoop Gravity"; // required
		public const string Description = "Makes the whoop fall faster and feel less floaty"; // optional
		public const string Author = "johnbrowndidnothingwrong"; // required
		public const string Company = null; // optional
		public const string Version = "0.0.1"; // required
		public const string DownloadLink = null; // optional
	}
	public class FPVSkyDiveRealisticWhoop : MelonMod
	{
		[harmony.HarmonyLib.HarmonyPatch(typeof(Orqa.SkyDive.Managers.SpawnDroneManager), "get_AvailableQuads")]
		class RealisticWhoopPatch
		{
			static void Prefix(List<Orqa.SkyDive.Misc.QuadData> ____availableQuads)
			{
				harmony.HarmonyLib.Harmony.DEBUG = true;
				MelonLogger.Msg("[Harmony patch prefix] RealisticWhoopPatch: trying to modify availableQuads field in SpawnDroneManager");
				// TODO: Modify the availableQuads fields
				int quadCount = ____availableQuads.Count;
				MelonLogger.Msg("[Harmony patch prefix] RealisticWhoopPatch: found " + quadCount.ToString() + " available quads");

				foreach (Orqa.SkyDive.Misc.QuadData quad in ____availableQuads) {
					MelonLogger.Msg("[Harmony patch prefix] RealisticWhoopPatch: found quad " + quad.QuadName);
					RealDrone.Profiles.QuadProfile qf = quad.QuadProfile;
					MelonLogger.Msg("[Harmony patch prefix] RealisticWhoopPatch: Phys profile: " + qf.PhysicsProfileName);

					if (quad.QuadName == "Main Menu/Whoop") {
						MelonLogger.Msg("[Harmony patch prefix] RealisticWhoopPatch: Found whoop. Current weight: " + quad.QuadWeight.ToString() + ". Current capacity: " + quad.QuadCapacity.ToString() + ". Current kv: " + quad.QuadKV.ToString() + ". Current max RPM: " + quad.QuadMaxRpm.ToString() + ". Attemping to update weight...");
						quad.QuadWeight = 250; // Make heavier
						quad.QuadKV = 2000; // Turn down kv
						quad.QuadMaxRpm = 3000; // Turn down RPM
						quad.QuadCapacity = 450; // Turn up mah
						quad.QuadProfile = ____availableQuads[0].QuadProfile; // Update the Whoop to use the regular Quad phys profile
											   //quad.
											   //quad.QuadName = "Main Menu/Modded Whoop";
					}
				}
			}
		}
	}
}
