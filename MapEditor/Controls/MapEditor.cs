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
        public MapEditor()
        {
            InitializeComponent();
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
            LoadSpriteSheet(spriteSheet);
            this.ucMapEdit.LoadMap(spriteSheet, tileSize);
        }

        /// <summary>
        /// Loads the sprite sheet into both viewers
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet to load into the viewer</param>
        public void LoadSpriteSheet(string spriteSheet)
        {
            this.ucSpriteSheet.LoadMap(spriteSheet);
            this.ucSpriteSheet.Map.MapRows.Add(new MapRow());
            this.ucSpriteSheet.Map.MapRows[0].Cells.Add(new MapCell(new Tile("SpriteSheet", 0, new Vector2(0, 0))));
        }
    }
}
