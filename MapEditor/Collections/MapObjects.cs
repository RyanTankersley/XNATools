using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.Collections
{
    public class MapObjects : List<MapObject>, IMapDirectoryContainer
    {
        public MapObjects()
        {

        }

        /// <summary>
        /// Finds the map object in this list with the given ID.
        /// </summary>
        /// <param name="id">The ID of the map object to find</param>
        /// <returns>The map object with the given ID.  Null if there is none.</returns>
        public MapObject FindByID(long id)
        {
            MapObject toReturn = null;

            foreach (MapObject o in this)
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
        /// Gets all the map objects with the given parent
        /// </summary>
        /// <param name="parent">The ID of the parent to get all the map objects for</param>
        /// <returns>The map objects with the given parent</returns>
        public static MapObjects GetMapObjects(long parent)
        {
            return DAL.MapDAL.GetMapObjects(parent);
        }

        public void Remove(IMapDirectoryObject toRemove)
        {
            if (toRemove is MapObject)
            {
                base.Remove(toRemove as MapObject);
            }
            else
            {
                throw new Exception("Invalid Remove.  Can only remove tilesets");
            }
        }

        public void Add(IMapDirectoryObject toAdd)
        {
            if (toAdd is MapObject)
            {
                base.Add(toAdd as MapObject);
            }
            else
            {
                throw new Exception("Invalid Add.  Can only remove tilesets");
            }
        }
    }
}
