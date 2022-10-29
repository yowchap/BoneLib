# BoneLib
A BONELAB mod for making life easier for other mod creators.

BoneLib is fully open source so feel free to make a PR for new features and bug fixes. Just make sure the code is commented/easily understandable, and doesn't noticeably affect performance.

## Features
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

- LoggingMode: "NORMAL" or "DEBUG". For most people normal is fine, debug will just show more info to help with development.

<br>

## Auto Updater
By default this mod will automatically update to the latest release from github when you launch the game. If you want to disable this for any reason, set `OfflineMode` to `true` in the preferences file. If you haven't run the game yet and don't have that file, create it and add the following lines to it.
```
[BoneLibUpdater]
OfflineMode = true
```

<br>

## Credits

**Gnonme / L4rs / Adidasaurus:** Main developers

**Parzival:** Added enum with all game layers, improved documentation, made some extension method parameters optional

**Extraes:** Ported nullable extension methods (originally made by WNP78) from MTINM, lots of testing, added InvokeActionSafe methods

**Adamdev:** Added events for NPC deaths and resurrections, and events for MarrowGame and MarrowScenes

<br>

## Changelogs (BoneLib)

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