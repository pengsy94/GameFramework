using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Core;

public static class FontsManager
{
    private static readonly Dictionary<string, FontSystem> Cache = new();
    private static GraphicsDevice _graphicsDevice = null!;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public static FontSystem GetFontSystem(string assetName)
    {
        if (Cache.TryGetValue(assetName, out var fontSystem))
            return fontSystem;

        fontSystem = new FontSystem();
        fontSystem.AddFont(File.ReadAllBytes(assetName));
        Cache[assetName] = fontSystem;

        return fontSystem;
    }

    public static DynamicSpriteFont GetFont(string assetName, int fontSize = 24)
    {
        var fontSystem = GetFontSystem(assetName);
        return fontSystem.GetFont(fontSize);
    }

    public static void Unload()
    {
        Cache.Clear();
    }
}