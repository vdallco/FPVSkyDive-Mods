# FPVSkyDive-Mods
Mods for the drone simulator/game FPV.SkyDive using MelonLoader, Harmony patches, LuaLoader, and dnSpy

# How to create and use mods

# Harmony based mods

## Download and run MelonLoader Installer:

https://melonwiki.xyz/

- *Important*: You must use MelonLoader v0.5.7 (or any version of v0.5.x). The latest version v0.6.x is not compatible with LuaLoader.

Select the following options in MelonLoader Installer:
- Unity Game: C:\Program Files (x86)\Steam\steamapps\common\FPV.SkyDive\FPV.SkyDive.exe
- Version: uncheck latest and select v0.5.7
- Game Arch: x64 (auto-detect)

And proceed with the installation.

## Unlimited map bounds mod

Copy Mods/FPVSkyDiveInfiniteBounds.dll to C:\Program Files (x86)\Steam\steamapps\common\FPV.SkyDive\Mods

## More realistic whoop gravity (fall faster):


Copy Mods/FPVSkyDiveRealisticWhoopGravity.dll to C:\Program Files (x86)\Steam\steamapps\common\FPV.SkyDive\Mods


## Adding hooks to FPV.SkyDive with Harmony mod

To add FPV.SkyDive function hooks, create a new C# Class Project (.NET Framework).

In Properties/AssemblyInfo.cs, add the following lines:

```
[assembly: MelonInfo(typeof(FPVSkyDiveInfiniteBounds.FPVSkyDiveUnlimitedMapBounds), "FPVSkyDiveInfiniteBounds", "0.0.1", "AuthorName", "URL")]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]
```

And add this class to your mods namespace:

```
public static class BuildInfo
{
	public const string Name = Mod Name"; // required
	public const string Description = "Mod Description"; // optional
	public const string Author = "author"; // required
	public const string Company = null; // optional
	public const string Version = "0.0.1"; // required
	public const string DownloadLink = null; // optional
}
```

Finally, you can hook any public or private function from a MelonMod with Harmony. For example:

Function: private string FormatTime(float elapsed) in class Orqa.SkyDive.Misc.SkyDiveTimer could be hooked like this:

```
public class SkyDiveTimerMod : MelonMod
	{
		[harmony.HarmonyLib.HarmonyPatch(typeof(Orqa.SkyDive.Misc.SkyDiveTimer), "FormatTime")]
		class SkyDiveTimerPatch
		{
			static bool Prefix(float elapsed)
			{
				harmony.HarmonyLib.Harmony.DEBUG = true;
				MelonLogger.Msg("[Harmony patch prefix] FormatTime.elapsed = " + elapsed.ToString());
			}
		}
	}
```

Harmony mods can also read and write to public or private fields. Read more on Harmony here: https://harmony.pardeike.net/articles/patching-injections.html


# Lua based mods

## Install LuaLoader:

https://github.com/Fukashiro-Yukari/LuaLoader/releases/tag/1.1211

Copy DLL files and LuaLoader folder to C:\Program Files (x86)\Steam\steamapps\common\FPV.SkyDive\


## Use dnSpy to insert a line of code that bypasses stuck menu

If you try running FPV.SkyDive with MelonLoader and LuaLoader installed, the loading screen will be stuck on "Loading user data..." and you will see this error in your Player.log file:

```
InvalidOperationException: Texture has not yet finished downloading
  at (wrapper managed-to-native) UnityEngine.Networking.DownloadHandlerTexture.InternalGetTextureNative(UnityEngine.Networking.DownloadHandlerTexture)
  at UnityEngine.Networking.DownloadHandlerTexture.get_texture () [0x00001] in <5017bc0f384248a2bb17491c954649dd>:0 
  at Orqa.SkyDive.FirebaseManager.GetImage (System.String imageUrl) [0x000d0] in <8ffba1bbe565480f9d1c5c819fcfef2f>:0 
  at Orqa.SkyDive.FirebaseManager.LoadTextures () [0x0009b] in <8ffba1bbe565480f9d1c5c819fcfef2f>:0 
  at System.Runtime.CompilerServices.AsyncMethodBuilderCore+<>c.<ThrowAsync>b__7_0 (System.Object state) [0x00000] in <17d9ce77f27a4bd2afb5ba32c9bea976>:0 
  at UnityEngine.UnitySynchronizationContext+WorkRequest.Invoke () [0x00002] in <ab14d35a27c043688812ae199c64b5aa>:0 
  at UnityEngine.UnitySynchronizationContext.Exec () [0x0005d] in <ab14d35a27c043688812ae199c64b5aa>:0 
  at UnityEngine.UnitySynchronizationContext.ExecuteTasks () [0x00014] in <ab14d35a27c043688812ae199c64b5aa>:0 
```

To bypass this, 
- open FPV.SkyDive.exe in dnSpy (make sure FPV.SkyDive is not running)
_ Open Orqa.SkyDive -> FirebaseManager and find the GetImage function
- Replace the contents of GetImage() with this:

```
public Task<Texture> GetImage(string imageUrl)
{
	return Task.FromResult<Texture>(null);
}
```

- Click Compile
- File -> Save Module

Now the loading screen can get past the "Loading user data..." setp, but the main menu banner/ad is broken.


## Lua console

Now you can open the Lua console in-game by pressing F10.

## Create Lua mods

You can create Lua mods by adding scripts to the LuaLoader/autorun directory.

For example, this code will run when the game is launched:

```
hook.Add('OnApplicationStart','OnApplicationStart',function()
    print("### OnApplicationStart ###")
end)
```

## Forking LuaLoader

If you need to add new hooks to LuaLoader. First fork and clone the LuaLoader repo.

You will get an error if you try opening the sln or csproj due to a missing project reference. Open up the LuaLoader.csproj and remove the reference to LuaLoader.props.

Now you can open the solution and project in Visual Studio. Hooks should be added to LuaLoader/Main.cs


# dnSpy tricks

## Remove map bounds with dnSpy:

In dnSpy, Orqa.SkyDive.Misc -> MapBoundsController, comment out all lines in InitializeBoundsCollider() function.

## Adding new quads

...