using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATools.TileEngine.Objects;
using XNATools.TileEngine.Collections;

namespace XNATools.MapEditor.Controls
{
    public partial class MapViewer : XNATools.Embedding.Controls.XNAControl
    {
        SpriteBatch spriteBatch = null;
        Camera camera = null;
        private TileMap map = null;
        public TileMap Map { get { return this.map; } set { this.map = value; } }

        public MapViewer()
        {
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            Random rand = new Random();
            spriteBatch = new SpriteBatch(base.GraphicsDevice);

            camera = new Camera(Vector2.Zero, new Vector2(this.Width, this.Height), new Vector2(1f, 1f));
            Application.Idle += delegate { Invalidate(); };
        }

        /// <summary>
        /// Creates the map using the given texture path and tile size.
        /// </summary>
        /// <param name="texturePath">The path of the texture to load into the map</param>
        /// <param name="tileSize">The size of the tiles on the texture</param>
        public void LoadMap(string texturePath, Vector2 tileSize)
        {
            Texture2D texture = null;
            if (File.Exists(texturePath))
            {
                try
                {
                    texture = base.LoadTexture(texturePath, System.IO.Path.GetFileNameWithoutExtension(texturePath));
                    LoadMap(texture, tileSize);
                }
                catch
                {
                    MessageBox.Show("Error Loading " + Path.GetFileName(texturePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File Path Does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates the map using the given texture path and tile size.  Loads the sprite sheet width/height as tile size.
        /// </summary>
        /// <param name="texturePath">The path of the texture to load into the map</param>
        public void LoadMap(string texturePath)
        {
            Texture2D texture = null;
            if (File.Exists(texturePath))
            {
                try
                {
                    texture = base.LoadTexture(texturePath, System.IO.Path.GetFileNameWithoutExtension(texturePath));
                    LoadMap(texture, new Vector2(texture.Width, texture.Height));
                }
                catch
                {
                    MessageBox.Show("Error Loading " + Path.GetFileName(texturePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File Path Does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates the map using the given texture and tile size
        /// </summary>
        /// <param name="texture">The texture to load into the map</param>
        /// <param name="tileSize">The size of the tiles on the texture</param>
        public void LoadMap(Texture2D texture, Vector2 tileSize)
        {
            if (texture != null && tileSize != null)
            {
                map = new TileMap(tileSize);
                map.SpriteSheet = texture;
            }
            else
            {
                MessageBox.Show("Error Loading texture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void LoadContent()
        {
            //
        }


        /// <summary>
        /// Updates the position of all the items on the map
        /// </summary>
        protected void UpdateControl()
        {
            camera.Update(this.map.MapSize);
        }

        /// <summary>
        /// Draws all the items on the map and calls update to update their position
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (map != null && map.SpriteSheet != null)
            {
                spriteBatch.Begin();
                map.Draw(spriteBatch, camera);
                spriteBatch.End();

                UpdateControl();
            }
        }
    }
}
