using SRML.SR;
using UnityEngine;

namespace CLS;

/// <summary>
/// Interface class that serves to handle adding customisation details for loading screen.
/// </summary>
public static class AddToLoading
{
    internal static readonly HashSet<Sprite> Icons = [];
    internal static readonly HashSet<string> TipIds = [];
    internal static readonly HashSet<Sprite> Splashes = [];

    internal static int TipCount;

    internal const string Prefix = "m.loadingtip.";

    /// <summary>
    /// Adds an icon to be randomly selected for the bouncy icon.
    /// </summary>
    /// <param name="icon">The icon to be added.</param>
    public static void AddIcon(Sprite icon) => Icons.Add(icon);

    /// <summary>
    /// Adds an image to be randomly selected for the loading screen background.
    /// </summary>
    /// <param name="splash">The background to be added.</param>
    public static void AddSplash(Sprite splash) => Splashes.Add(splash);

    /// <summary>
    /// Adds a translation id to be randomly selected for the loading tip.
    /// </summary>
    /// <param name="tipId">The translated id of the text to be added.</param>
    public static void AddLocalTipText(string tipId) => TipIds.Add(tipId);

    /// <summary>
    /// Adds a string of text to be randomly selected for the loading tip.
    /// </summary>
    /// <param name="tipText">The text to be added.</param>
    public static void AddTipText(string tipText)
    {
        var id = Prefix + TipCount;
        TranslationPatcher.AddUITranslation(id, tipText);
        TipCount++;
        TipIds.Add(id);
    }

    /// <summary>
    /// Batch method to add multiple icons with one method call.
    /// </summary>
    /// <param name="icons">The icons to be added.</param>
    public static void AddIcons(IEnumerable<Sprite> icons) => Icons.UnionWith(icons);

    /// <summary>
    /// Batch method to add multiple translation id with one method call.
    /// </summary>
    /// <param name="tipIds">The texts to be added.</param>
    public static void AddLocalTipTexts(IEnumerable<string> tipIds) => TipIds.UnionWith(tipIds);

    /// <summary>
    /// Batch method to add multiple backgrounds with one method call.
    /// </summary>
    /// <param name="splashes">The backgrounds to be added.</param>
    public static void AddSplashes(IEnumerable<Sprite> splashes) => Splashes.UnionWith(splashes);

    /// <summary>
    /// Batch method to add multiple texts with one method call.
    /// </summary>
    /// <param name="tipTexts">The texts to be added.</param>
    public static void AddTipTexts(IEnumerable<string> tipTexts)
    {
        foreach (var tipText in tipTexts)
            AddTipText(tipText);
    }

    internal static bool EndsWithAny(this string @string, params string[] parts) => parts.Any(x => @string.EndsWith(x, StringComparison.Ordinal));

    internal static bool StartsWithAny(this string @string, params string[] parts) => parts.Any(x => @string.StartsWith(x, StringComparison.Ordinal));
}