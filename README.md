# BoneLib
A BONELAB mod for making life easier for other mod creators.

BoneLib is fully open source so feel free to make a PR for new features and bug fixes. Just make sure the code is commented/easily understandable, and doesn't noticeably affect performance.

## Features
- Easy access to data related to the player (held items, player position, controllers, etc)
- Helper and extension methods for changing the RPM of guns, damaging enemies, getting clean object names

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

<br>

## Changelogs

#### v1.0.0:
- Initial release
