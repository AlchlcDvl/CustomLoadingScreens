using UnityEngine;

namespace CLS;

public static class AddToLoading
{
    internal static readonly List<Sprite> Icons = [];
    internal static readonly List<Sprite> Splashes = [];

    public static void AddIcon(Sprite sprite) => Icons.Add(sprite);

    public static void AddSplash(Sprite splash) => Splashes.Add(splash);
}