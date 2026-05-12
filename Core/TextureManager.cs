using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Core;

public static class TextureManager
{
    private static ContentManager _content = null!;

    private static readonly Dictionary<string, Texture2D> Cache = new();

    /// <summary>
    /// 在 CoreGame.LoadContent() 中调用一次。
    /// </summary>
    public static void Initialize(ContentManager content)
    {
        _content = content;
    }

    /// <summary>
    /// 加载并缓存纹理。
    ///
    /// 示例：
    /// Get("Textures/player")
    /// 对应 Content/Textures/player.png
    /// </summary>
    public static Texture2D Get(string assetName)
    {
        if (Cache.TryGetValue(assetName, out var texture))
            return texture;

        texture = _content.Load<Texture2D>(assetName);
        Cache[assetName] = texture;

        return texture;
    }

    /// <summary>
    /// 清空缓存（通常在切换场景或退出时调用）。
    /// </summary>
    public static void Unload()
    {
        Cache.Clear();
    }
}