using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.Collections
{
    public class ObjectDirectories : List<ObjectDirectory>, IMapDirectoryContainer
    {
        public ObjectDirectories()
        {
            
        }

        /// <summary>
        /// Finds the object direcotry in this list with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object direcotry to find</param>
        /// <returns>The object direcotry with the given ID.  Null if there is none.</returns>
        public ObjectDirectory FindByID(long id)
        {
            ObjectDirectory toReturn = null;

            foreach (ObjectDirectory o in this)
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
        /// Returns all the object directories with the given parent.
        /// </summary>
        /// <param name="parent">The parent of the object directories to get</param>
        /// <returns>The object directories with the given parent</returns>
        public static ObjectDirectories GetObjectDirectories(long parent)
        {
            return DAL.MapDAL.GetObjectDirectories(parent);
        }

        public void Remove(IMapDirectoryObject toRemove)
        {
            if (toRemove is ObjectDirectory)
            {
                base.Remove(toRemove as ObjectDirectory);
            }
            else
            {
                throw new Exception("Invalid Remove.  Can only remove tilesets");
            }
        }

        public void Add(IMapDirectoryObject toAdd)
        {
            if (toAdd is ObjectDirectory)
            {
                base.Add(toAdd as ObjectDirectory);
            }
            else
            {
                throw new Exception("Invalid Add.  Can only remove tilesets");
            }
        }
    }
}
