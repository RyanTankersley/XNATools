using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.TileEngine.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.TileEngine.Objects
{
    public class TileMap
    {
        private MapRows mapRows = null;
        private Texture2D spriteSheet;
        private Vector2 tileSize = Vector2.Zero;

        public MapRows MapRows { get { return this.mapRows; } set { this.mapRows = value; } }
        public Texture2D SpriteSheet { get { return this.spriteSheet; } set { this.spriteSheet = value; } }
        public Vector2 TileSize { get { return this.tileSize; } }
        public Vector2 MapSize
        {
            get
            {
                Vector2 mapSize = Vector2.Zero;
                mapSize.X = this.mapRows != null && mapRows.Count > 0 &&
                    mapRows[0].Cells != null ? mapRows[0].Cells.Count * tileSize.X :
                    0;
                mapSize.Y = this.mapRows != null ? mapRows.Count * tileSize.Y : 0;
                return mapSize;
            }
        }

        public TileMap(Vector2 tileSize)
        {
            mapRows = new Collections.MapRows();
            this.tileSize = tileSize;
        }

        /// <summary>
        /// Loads a sprite sheet file path into the spritesheet texture
        /// </summary>
        /// <param name="spriteSheet"></param>
        /// <param name="content"></param>
        public void LoadSpriteSheet(string spriteSheet, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.spriteSheet = content.Load<Texture2D>(spriteSheet);
        }

        /// <summary>
        /// Draws the tile map.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used for drawing</param>
        /// <param name="camera">The camera viewing the texture</param>
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 screenPosition = Vector2.Zero;
            int firstTileX = 0;
            int firstTileY = 0;
            int offsetX = 0;
            int offsetY = 0;
            int tilesToDrawAcross = 0;
            int tilesToDrawDown = 0;
            if (spriteBatch != null && camera != null)
            {
                firstTileX = (int)camera.Location.X / (int)tileSize.X;
                firstTileY = (int)camera.Location.Y / (int)tileSize.Y;
                offsetX = (int)camera.Location.X % (int)tileSize.X;
                offsetY = (int)camera.Location.Y % (int)tileSize.Y;
                tilesToDrawDown = (int)Math.Ceiling(camera.Size.Y / tileSize.Y) + 1;
                tilesToDrawAcross = (int)Math.Ceiling(camera.Size.X / tileSize.X) + 1;

                for (int i = 0; i < tilesToDrawDown && i + firstTileY < this.mapRows.Count; i++)
                {
                    for (int j = 0; j < tilesToDrawAcross && j + firstTileX < this.mapRows[i].Cells.Count; j++)
                    {
                        screenPosition.X = (j * tileSize.X) - offsetX;
                        screenPosition.Y = (i * tileSize.Y) - offsetY;
                        this.mapRows[i + firstTileY].Cells[j + firstTileX].Draw(spriteBatch, this.spriteSheet, screenPosition, tileSize);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the cell located at the x,y coordinate
        /// </summary>
        /// <param name="x">The position on the x axis</param>
        /// <param name="y">The position on the y axis</param>
        /// <returns>The map cell at x,y.  Null if there is none</returns>
        public MapCell GetMapCellFromPosition(int x, int y)
        {
            MapCell toReturn = null;
            int tileX = x / (int)tileSize.X;
            int tileY = y / (int)tileSize.Y;
            if (this.mapRows != null && this.mapRows.Count > tileY && this.mapRows[tileY].Cells.Count > tileX)
            {
                toReturn = this.mapRows[tileY].Cells[tileX];
            }

            return toReturn;
        }

        /// <summary>
        /// Creates a map that represents the given texture exactly
        /// </summary>
        public void CreateMapFromTileSheet()
        {
            int blocksAcross = 0;
            int blocksDown = 0;
            int x = 0;
            int y = 0;
            blocksAcross = this.SpriteSheet.Width / (int)this.tileSize.X;
            blocksDown = this.SpriteSheet.Height / (int)this.tileSize.Y;

            for (int i = 0; i < blocksDown; i++)
            {
                this.MapRows.Add(new MapRow());
                for (int j = 0; j < blocksAcross; j++)
                {
                    x = j * (int)this.tileSize.X;
                    y = i * (int)this.tileSize.Y;
                    this.MapRows[i].Cells.Add(new MapCell(new Tile("SpriteSheet", (byte)((i * 5) + j), new Vector2(x, y)), i, j));
                }
            }
        }

        public void CreateMapFromDefault(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                this.mapRows.Add(new MapRow());
                for (int j = 0; j < columns; j++)
                {
                    this.mapRows[i].Cells.Add(new MapCell(new Tile("Default", 0, Vector2.Zero), i, j));
                }
            }
        }

        public static TileMap CreateTileMapFromByteArray(byte[] bytes)
        {
            return null;
        }

        public byte[] GetBytesFromTileMap()
        {
            return new byte[] { 1 };
        }
    }
}
