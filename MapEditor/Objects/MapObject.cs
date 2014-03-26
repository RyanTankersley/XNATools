using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.TileEngine.Objects;

namespace XNATools.MapEditor.Objects
{
    public class MapObject : IMapDirectoryObject
    {
        private long id;
        private string name = string.Empty;
        private TileMap tileMap;

        public long ID { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public TileMap TileMap { get { return this.tileMap; } set { this.tileMap = value; } }

        public MapObject()
        {

        }

        public MapObject(string name, TileMap tileMap)
        {
            this.id = -1;
            this.name = name;
            this.tileMap = tileMap;
        }

        /// <summary>
        /// Gets a map object by ID
        /// </summary>
        /// <param name="id">The ID of the map object to get</param>
        /// <returns>The map object of the given ID.  Null if none</returns>
        public static MapObject GetMapObject(long id)
        {
            return DAL.MapDAL.GetMapObject(id);
        }
    }
}
