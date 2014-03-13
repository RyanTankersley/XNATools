using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNATools.TileEngine.Collections;

namespace XNATools.TileEngine.Objects
{
    public class MapRow
    {
        private MapCells cells = null;

        public MapCells Cells { get { return this.cells; } set { this.cells = value; } }

        public MapRow()
        {
            this.cells = new MapCells();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, int index)
        {
        }
    }
}
