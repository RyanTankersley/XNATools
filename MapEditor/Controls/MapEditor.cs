using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATools.TileEngine.Objects;
using XNATools.TileEngine.Collections;

namespace XNATools.MapEditor.Controls
{
    public class MapEditor : XNATools.Embedding.Controls.XNAControl
    {
        SpriteBatch spriteBatch = null;
        Camera camera = null;
        private TileMap map = null;
        public TileMap Map { get { return this.map; } set { this.map = value; } }

        public MapEditor()
        {
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            Random rand = new Random();
            //int val = 0;
            spriteBatch = new SpriteBatch(base.GraphicsDevice);
            //for (int i = 0; i < 100; i++)
            //{
            //    map.MapRows.Add(new MapRow());
            //    for (int j = 0; j < 100; j++)
            //    {
            //        val = (rand.Next() % 3);
            //        map.MapRows[i].Cells.Add(new MapCell(new Tile("Grass", (byte)val, new Vector2(val * 48, 0))));
            //    }
            //}

            camera = new Camera(Vector2.Zero, new Vector2(this.Width, this.Height), new Vector2(.6f, .6f));
            //LoadContent();
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        public void LoadMap(string texturePath, Vector2 tileSize)
        {
            if (File.Exists(texturePath))
            {
                try
                {
                    map = new TileMap(tileSize);
                    map.SpriteSheet = base.LoadTexture(texturePath, System.IO.Path.GetFileNameWithoutExtension(texturePath));
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

        protected void LoadContent()
        {
            //
        }

        protected void UpdateControl()
        {
            KeyboardState keyBoard = Keyboard.GetState();
            camera.Update(this.map.MapSize);
        }

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
