using SRML;
using UnityEngine;

namespace CLS;

internal sealed class Main : ModEntryPoint
{
    private static readonly string Folder = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Loading");
    private static readonly string[] AlreadyPresent = ["Chick", "Hen", "Rooster", "Boom", "Gold", "Honey", "Hunter", "Phosphor", "Pink", "Puddle", "Rad", "Rock", "Tabby"];

    public override void PreLoad()
    {
        HarmonyInstance.PatchAll(typeof(Main).Assembly);

        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);
    }

    public override void Load()
    {
        foreach (var path in Directory.EnumerateFiles(Folder, "*.*", SearchOption.TopDirectoryOnly))
        {
            if (path.EndsWith(".txt"))
            {
                var text = File.ReadAllText(path);
                AddToLoading.AddTipTexts(text.Split('\n').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)));
                continue;
            }

            if (!path.EndsWithAny(".png", ".jpg"))
                continue;

            using var stream = File.OpenRead(path);
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var texture = new Texture2D(2, 2, TextureFormat.ARGB32, true)
            {
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };
            texture.LoadImage(ms.ToArray());
            var name = Path.GetFileNameWithoutExtension(path);
            texture.name = name;
            var sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(0.5f, 0.5f), 1f, 0, SpriteMeshType.Tight);
            sprite.name = name;

            if (name.EndsWith("_Icon"))
                AddToLoading.AddIcon(sprite);
            else if (name.EndsWith("_Splash"))
                AddToLoading.AddSplash(sprite);
            else
                ConsoleInstance.Log("Cannot register " + path + "!");
        }

        foreach (var sprite in Resources.FindObjectsOfTypeAll<Sprite>())
        {
            if (sprite.name.StartsWithAny("iconSlime", "iconBird") && !AlreadyPresent.Contains(sprite.name.Replace("iconSlime", "").Replace("iconBird", "")))
                AddToLoading.AddIcon(sprite);
        }
    }
}