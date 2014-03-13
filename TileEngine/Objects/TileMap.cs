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

        public void LoadSpriteSheet(string spriteSheet, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.spriteSheet = content.Load<Texture2D>(spriteSheet);
        }

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

                for (int i = 0; i < tilesToDrawDown && i < this.mapRows.Count; i++)
                {
                    for (int j = 0; j < tilesToDrawAcross && j < this.mapRows[i].Cells.Count; j++)
                    {
                        screenPosition.X = (j * tileSize.X) - offsetX;
                        screenPosition.Y = (i * tileSize.Y) - offsetY;
                        this.mapRows[i + firstTileY].Cells[j + firstTileX].Draw(spriteBatch, this.spriteSheet, screenPosition, tileSize);
                    }
                }
            }
        }
    }
}
