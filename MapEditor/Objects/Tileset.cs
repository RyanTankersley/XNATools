using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.TileEngine.Collections;

namespace XNATools.MapEditor.Objects
{
    public class Tileset : ObjectDirectory
    {
        private int tileWidth = 0;
        private int tileHeight = 0;

        public int TileWidth { get { return this.tileWidth; } set { this.tileWidth = value; } }
        public int TileHeight { get { return this.tileHeight; } set { this.tileHeight = value; } }

        public Tileset()
            : base()
        {

        }

        public Tileset(string name, int tileWidth, int tileHeight)
            : base(name)
        {

        }

        /// <summary>
        /// Saves the Tileset to the database
        /// </summary>
        public void Save()
        {
            DAL.MapDAL.InsertUpdateTileset(this);
        }

        /// <summary>
        /// Deletes the tileset from the database
        /// </summary>
        public void Delete()
        {
            DAL.MapDAL.DeleteObjectDirectory(this.id);
        }

        /// <summary>
        /// Returns the name of the tileset on ToString()
        /// </summary>
        /// <returns>The name of the tileset</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
