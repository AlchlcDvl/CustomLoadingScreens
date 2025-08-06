using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;

namespace CLS;

[HarmonyPatch(typeof(LoadingUI), nameof(LoadingUI.Awake))]
internal static class AddImagesToLoadingUIPatch
{
    private static bool SpritesLoaded;
    private static Sprite[] Backgrounds;
    private static Sprite[] Icons;

    public static void Prefix(LoadingUI __instance)
    {
        var background = __instance.transform.Find("Background").GetComponent<Image>();

        if (!SpritesLoaded)
        {
            AddToLoading.AddSplash(background.sprite);
            AddToLoading.AddIcons(__instance.bouncyIcons);
            Backgrounds = [.. AddToLoading.Splashes];
            Icons = [.. AddToLoading.Icons];
            SpritesLoaded = true;
        }

        __instance.bouncyIcons = Icons;
        background.sprite = Randoms.SHARED.Pick(Backgrounds);

        var darkener = UnityEngine.Object.Instantiate(__instance.gameObject.FindChild("Shadow RightCorner", true), __instance.transform);
        darkener.name = "Shadow Bottom";
        var transform = darkener.transform;
        transform.localPosition = new(-1000f, -600f, 50f);
        transform.localScale = new(100f, -5f, 1f);
        transform.SetSiblingIndex(3);
    }
}