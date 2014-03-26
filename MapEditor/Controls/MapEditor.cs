using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATools.TileEngine.Objects;
using XNATools.TileEngine.Collections;

namespace XNATools.MapEditor.Controls
{
    public partial class MapEditor : UserControl
    {
        private int SelectedLayer { get { return (int)this.nmLayer.Value; } }

        public MapEditor()
        {
            InitializeComponent();
            this.ucMapEdit.MouseClickFinished += new EventHandler(MapViewer_Click);
            this.ucSpriteSheet.MouseClickFinished += new EventHandler(MapViewer_Click);
        }

        /// <summary>
        /// Loads the map viewer and sprite sheet, setting up the map with the given value
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet to use to edit the map</param>
        /// <param name="map">The map to create things from</param>
        /// <param name="tileSize">The size of the tiles on the map</param>
        public void LoadViewers(string spriteSheet, TileMap map, Vector2 tileSize)
        {
            LoadViewers(spriteSheet, tileSize);
            this.ucMapEdit.Map = map;
        }

        /// <summary>
        /// Loads the map viewer and the sprite sheet, making the map empty.
        /// </summary>
        /// <param name="spriteSheet">The spriteSheet to load</param>
        /// <param name="tileSize">The size of each tile in the spritesheet</param>
        public void LoadViewers(string spriteSheet, Vector2 tileSize)
        {
            LoadSpriteSheet(spriteSheet, tileSize);
            this.ucMapEdit.LoadMap(spriteSheet, tileSize);
            if (this.ucMapEdit.Map != null)
            {
                this.ucMapEdit.Map.CreateMapFromDefault(200, 200);
            }
            this.ucMapEdit.ChangeCameraMoveable(false);
        }

        /// <summary>
        /// Loads the sprite sheet into both viewers
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet to load into the viewer</param>
        public void LoadSpriteSheet(string spriteSheet, Vector2 tileSize)
        {
            this.ucSpriteSheet.LoadMap(spriteSheet, tileSize);
            if (this.ucSpriteSheet.Map != null)
            {
                this.ucSpriteSheet.Map.CreateMapFromTileSheet();
            }

            this.ucSpriteSheet.ChangeCameraMoveable(true);
        }

        /// <summary>
        /// MapViewer_Click Event.  Sets the clicked viewer's camera to moveable,
        /// and the unclicked viewer's camera to unmoveable
        /// </summary>
        /// <param name="sender">The map viewer clicked on</param>
        /// <param name="e">Event Args</param>
        public void MapViewer_Click(object sender, EventArgs e)
        {
            MapViewer mv = sender as MapViewer;
            if (mv != null)
            {
                if (mv.Name == this.ucSpriteSheet.Name)
                {
                    HandleSpriteMapClick(mv);
                }
                else
                {
                    HandleTileMapClick(mv);
                }
            }
        }

        private void HandleSpriteMapClick(MapViewer mv)
        {
            mv.ChangeCameraMoveable(true);
            this.ucMapEdit.ChangeCameraMoveable(false);

        }

        private void HandleTileMapClick(MapViewer mv)
        {
            mv.ChangeCameraMoveable(true);
            this.ucSpriteSheet.ChangeCameraMoveable(false);
            MapCell ssCell = null;
            MapCell meCell = null;
            if (this.ucSpriteSheet.SelectedMapCells.Count > 0 && this.ucMapEdit.SelectedMapCells.Count > 0)
            {
                ssCell = this.ucSpriteSheet.SelectedMapCells[0];
                meCell = this.ucMapEdit.Map.MapRows[this.ucMapEdit.SelectedMapCells[0].RowIndex].Cells[this.ucMapEdit.SelectedMapCells[0].ColumnIndex];
                for (int i = meCell.Tiles.Count; i < SelectedLayer; i++)
                {
                    meCell.Tiles.Add(new Tile("Default", 0, Vector2.Zero));
                }

                meCell.Tiles[SelectedLayer - 1] = ssCell.Tiles[0];
            }
        }

        private void nmLayer_ValueChanged(object sender, EventArgs e)
        {
            this.ucMapEdit.Focus();
        }
    }
}
