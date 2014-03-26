using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.TileEngine.Objects
{
    public class Tile
    {
        private string name;
        private byte id;
        private Vector2 spriteSheetPosition = Vector2.Zero;

        public string Name { get { return this.name; } }
        public byte ID { get { return this.id; } }
        public Vector2 SpriteSheetPosition { get { return this.spriteSheetPosition; } }

        public Tile(string name, byte id, Vector2 position)
        {
            this.name = name;
            this.id = id;
            this.spriteSheetPosition = position;

        }

        /// <summary>
        /// Draws the Tile
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used to draw</param>
        /// <param name="texture">The texture the tile is on</param>
        /// <param name="screenPosition">The position on screen</param>
        /// <param name="size">The size of the tile</param>
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 screenPosition, Vector2 size)
        {
            spriteBatch.Draw(texture, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, (int)size.X, (int)size.Y),
                             new Rectangle((int)spriteSheetPosition.X, (int)spriteSheetPosition.Y, (int)size.X, (int)size.Y), Color.White);
        }
    }
}
