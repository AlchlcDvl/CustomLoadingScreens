using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;

namespace CLS;

[HarmonyPatch(typeof(LoadingUI), nameof(LoadingUI.Awake))]
internal static class AddImagesToLoadingUIPatch
{
    private static bool BackgroundAdded;
    private static Sprite[] Backgrounds;
    private static Sprite[] Icons;

    public static void Prefix(LoadingUI __instance)
    {
        var background = __instance.transform.Find("Background").GetComponent<Image>();

        if (!BackgroundAdded)
        {
            AddToLoading.AddSplash(background.sprite);
            Backgrounds = AddToLoading.Splashes.ToArray();
            Icons = __instance.bouncyIcons.Concat(AddToLoading.Icons).ToArray();
            BackgroundAdded = true;
        }

        __instance.bouncyIcons = Icons;
        background.sprite = Randoms.SHARED.Pick(Backgrounds);
    }
}