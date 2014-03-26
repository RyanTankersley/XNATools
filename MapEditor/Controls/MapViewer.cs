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
        public EventHandler MouseClickFinished;

        private SpriteBatch spriteBatch = null;
        private Camera camera = null;
        private TileMap map = null;
        public TileMap Map { get { return this.map; } set { this.map = value; } }
        private MapCells selectedMapCells = null;
        public MapCells SelectedMapCells { get { return this.selectedMapCells; } set { this.selectedMapCells = value; } }

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
            selectedMapCells = new MapCells();

            camera = new Camera(Vector2.Zero, new Vector2(this.Width, this.Height), new Vector2(1f, 1f));

            this.MouseClick +=new MouseEventHandler(MapViewer_MouseClick);

            Application.Idle += delegate { Invalidate(); };
        }

        /// <summary>
        /// Creates the map using the given texture path and tile size.
        /// </summary>
        /// <param name="texturePath">The path of the texture to load into the map</param>
        /// <param name="tileSize">The size of the tiles on the texture</param>
        /// <param name="cameraSize">The size of the camera</param>
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
        /// Creates the map using the given texture and tile size
        /// </summary>
        /// <param name="texture">The texture to load into the map</param>
        /// <param name="cameraSize">The size of the camera</param>
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

        /// <summary>
        /// Empty
        /// </summary>
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

        /// <summary>
        /// Changes the camera between a fixed locatin to a moveable one.
        /// </summary>
        /// <param name="moveable">True to make camera moveable, false to make it fixed</param>
        public void ChangeCameraMoveable(bool moveable)
        {
            if (this.camera != null)
            {
                this.camera.Moveable = moveable;
            }
        }

        private void ClearSelection()
        {
            this.selectedMapCells = new MapCells();
        }

        private void MapViewer_MouseClick(object sender, MouseEventArgs e)
        {
            ClearSelection();
            this.selectedMapCells.Add(this.map.GetMapCellFromPosition((e.X + (int)this.camera.Location.X), (e.Y + (int)this.camera.Location.Y)));
            if (this.MouseClickFinished != null) this.MouseClickFinished(sender, new EventArgs());
        }
    }
}
