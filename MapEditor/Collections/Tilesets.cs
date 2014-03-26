using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.Collections
{
    public class Tilesets : List<Tileset>, IMapDirectoryContainer
    {
        public Tilesets()
        {

        }

        /// <summary>
        /// Gets the tileset with the matching ID
        /// </summary>
        /// <param name="id">The ID of the tileset to get</param>
        /// <returns>The tileset with the given ID</returns>
        public Tileset FindByID(long id)
        {
            Tileset toReturn = null;

            foreach (Tileset o in this)
            {
                if (o.ID == id)
                {
                    toReturn = o;
                    break;
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets all the tilesets on the database
        /// </summary>
        /// <returns>All tilesets on the database</returns>
        public static Tilesets GetTilesets()
        {
            return DAL.MapDAL.GetTilesets();
        }

        public void Save()
        {
            DAL.MapDAL.InsertUpdateTilesets(this);
        }

        public void Remove(IMapDirectoryObject toRemove)
        {
            if (toRemove is Tileset)
            {
                base.Remove(toRemove as Tileset);
            }
            else
            {
                throw new Exception("Invalid Remove.  Can only remove tilesets");
            }
        }

        public void Add(IMapDirectoryObject toAdd)
        {
            if (toAdd is Tileset)
            {
                base.Add(toAdd as Tileset);
            }
            else
            {
                throw new Exception("Invalid Add.  Can only remove tilesets");
            }
        }
    }
}
