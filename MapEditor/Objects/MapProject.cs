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
        private string name = string.Empty;
        private TileMaps tileMaps = null;
        public string Name { get { return this.name; } set { this.name = value; } }
        public TileMaps TileMaps { get { return this.tileMaps; } set { this.tileMaps = value; } }

        public MapProject()
        {
            tileMaps = new TileMaps();
        }

        public MapProject(string name)
        {
            this.name = name;
            tileMaps = new TileMaps();
        }
    }
}
