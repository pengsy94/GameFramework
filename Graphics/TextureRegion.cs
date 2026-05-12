using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Graphics;

public class TextureRegion
{
    public Texture2D Texture { get; }
    public Rectangle SourceRectangle { get; }

    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    /// <summary>
    /// 创建一个新的纹理区域
    /// </summary>
    public TextureRegion()
    {
    }

    /// <summary>
    /// 创建 源纹理指定区域新的纹理
    /// </summary>
    /// <param name="texture">源纹理</param>
    /// <param name="x">新纹理开始坐标</param>
    /// <param name="y">新纹理结束坐标</param>
    /// <param name="width">新纹理宽</param>
    /// <param name="height">新纹理高.</param>
    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }

    public TextureRegion(Texture2D texture, Rectangle sourceRectangle)
    {
        Texture = texture;
        SourceRectangle = sourceRectangle;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        Draw(spriteBatch, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin,
        float scale, SpriteEffects effects, float layerDepth)
    {
        Draw(
            spriteBatch,
            position,
            color,
            rotation,
            origin,
            new Vector2(scale, scale),
            effects,
            layerDepth
        );
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin,
        Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        spriteBatch.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            rotation,
            origin,
            scale,
            effects,
            layerDepth
        );
    }
}