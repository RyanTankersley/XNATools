using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.MapEditor.Objects;

namespace XNATools.MapEditor.Collections
{
    public class MapProjects : List<MapProject>
    {
        public MapProjects()
        {

        }

        /// <summary>
        /// Gets all the map projects in the database
        /// </summary>
        /// <returns>All the map projects in the database</returns>
        public static MapProjects GetMapProjects()
        {
            return DAL.MapDAL.GetAllMapProjects();
        }
    }
}
