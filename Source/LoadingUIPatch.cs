using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;

namespace CLS;

[HarmonyPatch(typeof(LoadingUI), nameof(LoadingUI.Awake))]
internal static class AddImagesToLoadingUIPatch
{
    private static Sprite[] Icons;
    private static Sprite[] Splashes;
    private static string[] TipTexts;
    private static bool SpritesLoaded;

    public static bool Prefix(LoadingUI __instance)
    {
        var background = __instance.transform.Find("Background").GetComponent<Image>();

        if (!SpritesLoaded)
        {
            var bundle = GameContext.Instance.MessageDirector.GetBundle("ui");
            var set = new HashSet<string>();

            for (var i = 0; bundle.Exists("m.loadingtip." + i); i++)
                set.Add(bundle.Xlate("m.loadingtip." + i));

            AddToLoading.AddIcons(__instance.bouncyIcons);
            AddToLoading.AddSplash(background.sprite);
            AddToLoading.AddTipTexts(set);

            Icons = [.. AddToLoading.Icons];
            TipTexts = [.. AddToLoading.TipTexts];
            Splashes = [.. AddToLoading.Splashes];

            SpritesLoaded = true;
        }

        __instance.bouncyIcons = Icons;

        __instance.bouncySlime.sprite = Randoms.SHARED.Pick(Icons);
        __instance.tipText.text = Randoms.SHARED.Pick(TipTexts);
        background.sprite = Randoms.SHARED.Pick(Splashes);

        var darkener = __instance.gameObject.FindChild("Shadow RightCorner", true).Instantiate(__instance.transform);
        darkener.name = "Shadow Bottom";

        var transform = darkener.transform;
        transform.localPosition = new(-1000f, -600f, 50f);
        transform.localScale = new(100f, -5f, 1f);
        transform.SetSiblingIndex(3);

        return false;
    }
}