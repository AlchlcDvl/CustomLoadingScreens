using UnityEngine;

namespace CLS;

public static class AddToLoading
{
    internal static readonly HashSet<Sprite> Icons = [];
    internal static readonly HashSet<Sprite> Splashes = [];

    public static void AddIcon(Sprite sprite) => Icons.Add(sprite);

    public static void AddSplash(Sprite splash) => Splashes.Add(splash);

    public static void AddIcons(IEnumerable<Sprite> icons) => Icons.UnionWith(icons);

    public static void AddSplashes(IEnumerable<Sprite> splashes) => Splashes.UnionWith(splashes);

    internal static bool StartsWithAny(this string @string, params string[] parts) => parts.Any(@string.StartsWith);
}