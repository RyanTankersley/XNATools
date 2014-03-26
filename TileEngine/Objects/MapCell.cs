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

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public Tiles Tiles { get { return this.tiles; } set { this.tiles = value; } }

        public MapCell(Tile tile, int rowIndex, int columnIndex)
        {
            this.tiles = new Tiles();
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.tiles.Add(tile);
        }

        public MapCell(Tiles tiles, int rowIndex, int columnIndex)
        {
            this.tiles = tiles;
        }

        /// <summary>
        /// Draws all of the tiles on the cell
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used for drawing</param>
        /// <param name="texture">The texture the cell is on</param>
        /// <param name="screenPosition">The position to draw the cell on</param>
        /// <param name="tileSize">The size of the tiles</param>
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
