# BoneLib
A BONELAB mod for making life easier for other mod creators.

BoneLib is fully open source so feel free to make a PR for new features and bug fixes. Just make sure the code is commented/easily understandable, and doesn't noticeably affect performance.

## Features
- A menu system for other mods to use
- Easy access to data related to the player (held items, position, controllers, current avatar, etc)
- Events for common actions, such as guns firing, grabbing items, changing avatars, NPCs dying, etc
- Helper and extension methods for changing the RPM of guns, damaging enemies, getting clean object names
- Extension methods to call functions with nullable parameters that would otherwise crash the game
- Spawn message boxes for the player
- Safely invoke actions with error handling
- Lots of other small features

Many more features will be added as we continue to develop the mod.

<br>

## User Preferences
You will have to run the game once with the mod installed before the preferences will show up.

Preferences are stored in `UserData\MelonPreferences.cfg`.

- LoggingMode: `NORMAL` or `DEBUG`. For most people normal is fine, debug will just show more info to help with development.

- SkipIntro: `true` to not play the intro at the start of the game and go straight to the menu.

<br>

## Auto Updater
By default this mod will automatically update to the latest release from github when you launch the game. If you want to disable this for any reason, set `OfflineMode` to `true` in the preferences file. If you haven't run the game yet and don't have that file, create it and add the following lines to it.
```
[BoneLibUpdater]
OfflineMode = true
```

<br>

## Development Setup
The VS project uses the system environment variable `BONELAB_DIR` for referencing assemblies and build output paths. Make sure you have this set on your computer to your BONELAB install location (where the .exe is, no trailing slash) for VS to be able to find the files. If this doesn't work right away, try deleting the `.vs` folder for this project and restarting VS.

<br>

## Credits

**Gnonme / Lvna / Adi / Adamdev:** Main developers

**Parzival:** Added enum with all game layers, improved documentation, made some extension method parameters optional

**Extraes:** Ported nullable extension methods (originally made by WNP78) from MTINM, lots of testing, added InvokeActionSafe methods, added animal image popup methods

**Adamdev:** Added events for NPC deaths and resurrections, and events for MarrowGame and MarrowScenes, added BoneMenu

**Lakatrazz:** BoneMenu improvements, fixed issues after game updates

**Trev:** LemonLoader support

**SoulWithMae:** SpawnCrate helper methods, added a class with commonly used barcodes, added a notification system, added helper methods for loading stuff from embedded resources

**WNP78:** Ported the mod to ML 0.6 and improved reference DLL handling

<br>

## Changelogs (BoneLib)

#### v3.0.0:
- Complete overhaul of BoneMenu which features dialogs, string elements, and more
- Audio class now wraps around the Marrow audio methods, and mixers are exposed
- Hooks for when the player spawner spawns the player

#### v2.5.0:
- Added `OnWarehouseReady` hook
- Ported to work with ML 0.6+
- Improved DLL reference handling

#### v2.4.0:
- Added notification system
- Added asset bundle helpers
- Added IsLoading helper method
- Added CheckIfAssemblyLoaded helper method

#### v2.3.0:
- Added Audio class with references to mixers
- Added CommonBarcodes for easy item spawning
- Improved error logging

#### v2.2.2:
- Added SpawnCrate helper methods

#### v2.2.1:
- Fixed compatibility with bonelab patch 3

#### v2.2.0:
- Added LemonLoader support for quest modding
- Added `IsAndroid` bool to HelperMethods
- Added sound and haptics to BoneMenu
- Bug fixes

#### v2.1.1:
- Fixed BoneMenu confirmation buttons

#### v2.1.0:
- Added starting value parameter to enum element methods
- Float elements are displayed rounded
- Menu elements can now be updated with the menu open

#### v2.0.1:
- Fixed startup crash

#### v2.0.0:
- Added BoneMenu
- Added extension methods for safe action invoking
- Changed level loading events in Hooking
- Note: this update is not guaranteed to be compatible with mods made for BoneLib v1.x.x

#### v1.4.0:
- Added a preference to skip the intro
- Added methods to spawn popup objects with random animal photos on them for fun
- Forced preferences to always save to file

#### v1.3.1:
- Fixed `Hooking.OnMarrowGameStarted` event

#### v1.3.0:
- Added `SafeActions` class for invoking actions with error handling
- Added MarrowGame and MarrowScene events in Hooking
- Added events for NPC deaths and resurrection in Hooking
- Added the controller rig to the Player class
- Added priority attribute to force MelonLoader to always load BoneLib first
- Made some parameters optional in nullable method extensions

#### v1.2.0:
- Added `Hooking.OnPlayerReferencesFound` since the melonloader level load/init functions are unreliable
- Added a way to spawn message boxes
- Fixed Nullables being in the wrong namespace
- Fixed Nullables crashing the game
- Removed extensions backwards compatibility for wrong namespace
- Code cleanup

#### v1.1.0:
- Added Hooking class with events for common actions
- Added `GetPhysicsRig()` and `GetCurrentAvatar()` to Player class
- Added GameLayers enum
- Added extension methods to allow using functions with nullable parameters
- Fixed `SetRpm()` and `DealDamage()` extension methods being in the wrong namespace
- Other small fixes and changes

#### v1.0.0:
- Initial release

<br/>

## Changelogs (BoneLib Updater)

#### v1.1.0:
- Added downloading xml documentation file with mod dll

#### v1.0.0:
- Initial release
