# Custom Loading Screens Mod
The loading screen of Slime Rancher seems a bit generic and old, does it not? Well now, you customise it!

Using the fairly simple system that has been developed, this mod functions as a simple core lib mod for other mods to add in their own loading screens, while also encouraging the average user to also add in their own icons in their game folders!

## For Non Developers
- Run the mod once. This should create a new folder in your game installation called `Loading`
- Drop in your image into this folder. It has to be either jpg or png.
- - If the image name ends with `_Icon`, it gets added as the little wobbly icon in the loading screen.
- - If the image name ends with `_Splash`, it gets added as the background image of the loading screen.

## For Developers
- Add the following id to your `load_after` entry in your `modinfo.json` file: `"custom.loading"`
- Add the mod's dll to your list of references
- In your entry point class, add the following code:
```cs
public static void AddIconBypass(Sprite icon)
{
    try
    {
        CLS.AddToLoading.AddIcon(icon);
    } catch {}
}

public static void AddSplashBypass(Sprite splash)
{
    try
    {
        CLS.AddToLoading.AddSplash(splash);
    } catch {}
}
```
- In your mod's `Load()`, use the above code like this:
```cs
if (SRModLoader.IsModPresent("custom.loading"))
{
    Sprite iconOrSplash = GetOrCreateYourSprite();
    AddIconBypass(iconOrSplash); // If icon
    AddSplashBypass(iconOrSplash); // If splash
}
```
