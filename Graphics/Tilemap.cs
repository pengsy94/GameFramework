using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Graphics;

public class Tilemap
{
    private readonly Tileset _tileset;
    private readonly int[] _tiles;

    public int Rows { get; }

    public int Columns { get; }

    public int Count { get; }

    public Vector2 Scale { get; set; }

    public float TileWidth => _tileset.TileWidth * Scale.X;

    public float TileHeight => _tileset.TileHeight * Scale.Y;

    public Tilemap(Tileset tileset, int columns, int rows)
    {
        _tileset = tileset;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        Scale = Vector2.One;
        _tiles = new int[Count];
    }

    public void SetTile(int index, int tilesetID)
    {
        _tiles[index] = tilesetID;
    }

    public void SetTile(int column, int row, int tilesetID)
    {
        int index = row * Columns + column;
        SetTile(index, tilesetID);
    }

    public TextureRegion GetTile(int index)
    {
        return _tileset.GetTile(_tiles[index]);
    }

    public TextureRegion GetTile(int column, int row)
    {
        var index = row * Columns + column;
        return GetTile(index);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (var i = 0; i < Count; i++)
        {
            var tilesetIndex = _tiles[i];
            var tile = _tileset.GetTile(tilesetIndex);

            var x = i % Columns;
            var y = i / Columns;

            var position = new Vector2(x * TileWidth, y * TileHeight);
            tile.Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
        }
    }

    public static Tilemap FromFile(ContentManager content, string filename)
    {
        var filePath = Path.Combine(content.RootDirectory, filename);

        using (var stream = TitleContainer.OpenStream(filePath))
        {
            using (var reader = XmlReader.Create(stream))
            {
                var doc = XDocument.Load(reader);
                var root = doc.Root;

                var tilesetElement = root.Element("Tileset");

                var regionAttribute = tilesetElement.Attribute("region").Value;
                var split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var x = int.Parse(split[0]);
                var y = int.Parse(split[1]);
                var width = int.Parse(split[2]);
                var height = int.Parse(split[3]);

                var tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                var tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                var contentPath = tilesetElement.Value;

                var texture = content.Load<Texture2D>(contentPath);
                var textureRegion = new TextureRegion(texture, x, y, width, height);
                var tileset = new Tileset(textureRegion, tileWidth, tileHeight);

                var tilesElement = root.Element("Tiles");
                var rows = tilesElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var columnCount = rows[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
                var tilemap = new Tilemap(tileset, columnCount, rows.Length);
                for (var row = 0; row < rows.Length; row++)
                {
                    var columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (var column = 0; column < columnCount; column++)
                    {
                        var tilesetIndex = int.Parse(columns[column]);
                        tilemap.SetTile(column, row, tilesetIndex);
                    }
                }

                return tilemap;
            }
        }
    }
}