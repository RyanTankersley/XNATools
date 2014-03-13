using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.TileEngine.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.TileEngine.Objects
{
    public class MapCell
    {
        private Tiles tiles;

        public Tiles Tiles { get { return this.tiles; } set { this.tiles = value; } }

        public MapCell(Tile tile)
        {
            this.tiles = new Tiles();
            this.tiles.Add(tile);
        }

        public MapCell(Tiles tiles)
        {
            this.tiles = tiles;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 screenPosition, Vector2 tileSize)
        {
            if (this.tiles != null && this.tiles.Count > 0)
            {
                foreach (Tile tile in this.tiles)
                {
                    if (tile != null)
                    {
                        tile.Draw(spriteBatch, texture, screenPosition, tileSize);
                    }
                }
            }
        }
    }
}
