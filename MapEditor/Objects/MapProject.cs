using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Objects;
using XNATools.TileEngine.Objects;
using XNATools.TileEngine.Collections;

namespace XNATools.MapEditor.Objects
{
    public class MapProject
    {
        private int id = 0;
        private string name = string.Empty;
        private TileMaps tileMaps = null;
        private ObjectDirectory tileset;

        public int ID { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public TileMaps TileMaps { get { return this.tileMaps; } set { this.tileMaps = value; } }
        public ObjectDirectory Tileset { get { return this.tileset; } set { this.tileset = value; } }

        public MapProject()
        {
            tileMaps = new TileMaps();
        }

        public MapProject(string name)
        {
            this.name = name;
            tileMaps = new TileMaps();
        }

        /// <summary>
        /// Saves the Map Project
        /// </summary>
        public void Save()
        {
            if (tileset != null)
            {
                DAL.MapDAL.InsertUpdateMapProject(this);
            }
        }

        /// <summary>
        /// Deletes the map project
        /// </summary>
        public void Delete()
        {
            DAL.MapDAL.DeleteMapProject(this.id);
        }
    }
}
