using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using XNATools.MapEditor.Collections;
using XNATools.MapEditor.Objects;
using XNATools.ToolHelper;
using XNATools.TileEngine.Objects;

namespace XNATools.MapEditor.DAL
{
    public static class MapDAL
    {
        #region Connection Paths

        /// <summary>
        /// Gets the connection string for the DAL's database
        /// </summary>
        /// <returns>The connection string</returns>
        public static string GetConnectionString()
        {
            string path = Path.Combine(GetDataPath(), "Databases", "XNATools.mdf");
            return String.Format(Properties.Settings.Default.ConnectionString, path);
        }

        /// <summary>
        /// Gets the path of the tileset location
        /// </summary>
        /// <returns>The tile set location</returns>
        public static string GetTilesetPath()
        {
            return Path.Combine(GetDataPath(), "Tilesets");
        }

        /// <summary>
        /// Gets the path for the data folder which holds the databases and tilesets.
        /// </summary>
        /// <returns>The base path for the data folder</returns>
        private static string GetDataPath()
        {
            string path = System.Windows.Forms.Application.StartupPath;
            DirectoryInfo info = new DirectoryInfo(path);
            if (info != null && info.Parent != null && info.Parent.Parent != null &&
               info.Parent.Parent.Parent != null)
            {
                path = Path.Combine(info.Parent.Parent.Parent.FullName, "Data");
            }

            return path;
        }

        #endregion

        #region Map Project

        /// <summary>
        /// Gets all map projects in the database
        /// </summary>
        /// <returns>All map projects in the database</returns>
        public static MapProjects GetAllMapProjects()
        {
            MapProjects toReturn = new MapProjects();
            MapProject temp = null;
            DataTable dt = null;
            string query = "SELECT * FROM MapProject";
            dt = DatabaseHelper.ExecuteTextCommandQuery(GetConnectionString(), query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new MapProject();
                    temp.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);
                    temp.TileMaps = new TileEngine.Collections.TileMaps();
                    temp.Tileset = GetObjectDirectory(DatabaseHelper.GetValidValueFromObject(dr["Tileset"], (long)0));
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Inserts or Updates the given map project to the database
        /// </summary>
        /// <param name="o">The Map Project to insert or update</param>
        public static void InsertUpdateMapProject(MapProject o)
        {
            string query = string.Empty;
            SqlCommand cmd = null;

            if (o != null)
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                //Update
                if (o.ID > 0)
                {
                    query = "UPDATE [MapProject] SET Name = @Name, Tileset = @Tileset WHERE ID = @ID";
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = o.ID;
                }
                //Insert
                else
                {
                    query = "INSERT INTO [MapProject] (Name, Tileset) VALUES (@Name, @Tileset)";
                }

                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = o.Name;
                cmd.Parameters.Add("@Tileset", SqlDbType.BigInt).Value = o.Tileset.ID;

                cmd.CommandText = query;
                DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
            }
        }

        /// <summary>
        /// Deletes the map project with the given ID
        /// </summary>
        /// <param name="id">The ID of the map project to delete</param>
        public static void DeleteMapProject(int id)
        {
            SqlCommand cmd = new SqlCommand();
            string query = "DELETE FROM MapProject WHERE ID = @ID";
            
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
        }

        #endregion

        #region Tileset

        /// <summary>
        /// Gets all tilesets.
        /// </summary>
        /// <returns>All tilesets</returns>
        public static Tilesets GetTilesets()
        {
            Tilesets toReturn = new Tilesets();
            Tileset temp = null;
            DataTable dt = null;
            string query = "SELECT * FROM [Tileset] " + 
                           "JOIN ObjectDirectory ON Tileset.ID = ObjectDirectory.ID";

            dt = DatabaseHelper.ExecuteTextCommandQuery(GetConnectionString(), query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new Tileset();
                    temp.ID = DatabaseHelper.GetValidValueFromObject(dr["ID"], (long)0);
                    temp.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);
                    temp.TileHeight = DatabaseHelper.GetValidValueFromObject(dr["TileHeight"], (int)0);
                    temp.TileWidth = DatabaseHelper.GetValidValueFromObject(dr["TileWidth"], (int)0);

                    toReturn.Add(temp);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Deletes from the database any Tileset that is no longer in this list, then calls insertupdate on each object
        /// </summary>
        /// <param name="os">The list of tilesets to have in the database</param>
        public static void InsertUpdateTilesets(Tilesets os)
        {
            Tilesets curr = GetTilesets();

            if (curr != null && curr.Count > 0 && os != null)
            {
                foreach (Tileset o in curr)
                {
                    if (os.FindByID(o.ID) == null)
                    {
                        DeleteObjectDirectory(o.ID);
                    }
                }
            }

            foreach (Tileset o in os)
            {
                InsertUpdateTileset(o);
            }
        }

        /// <summary>
        /// Inserts or Updates a tileset
        /// </summary>
        /// <param name="o">The tileset to insert/update</param>
        /// <param name="parent">The parent of the tileset</param>
        public static void InsertUpdateTileset(Tileset o)
        {
            string query = string.Empty;
            SqlCommand cmd = null;
            long id = 0;

            if (o != null)
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                //Update
                if (o.ID > 0)
                {
                    query = "UPDATE [ObjectDirectory] SET Name = @Name, Parent = NULL WHERE ID = @ID; " +
                            "UPDATE [Tileset] Set TileHeight = @TileHeight, TileWidth = @TileWidth WHERE ID = @ID; " +
                            "SELECT SCOPE_IDENTITY();";
                    cmd.CommandText = query;

                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = o.ID;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = o.Name;
                    cmd.Parameters.Add("@TileHeight", SqlDbType.Int).Value = o.TileHeight;
                    cmd.Parameters.Add("@TileWidth", SqlDbType.Int).Value = o.TileWidth;
                    id = DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
                    if (id > 0)
                    {
                        id = o.ID;
                    }
                }
                //Insert
                else
                {
                    query = "INSERT INTO [ObjectDirectory] (Name, Parent) VALUES (@Name, NULL); " +
                            "SELECT SCOPE_IDENTITY();";
                    cmd.CommandText = query;

                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = o.Name;

                    id = DatabaseHelper.ExecuteCommandContainingGetIdentity(GetConnectionString(), cmd);

                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;

                    query = "INSERT INTO [Tileset] (ID, TileHeight, TileWidth) VALUES (@ID, @TileHeight, @TileWidth)";
                    cmd.CommandText = query;

                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;
                    cmd.Parameters.Add("@TileHeight", SqlDbType.Int).Value = o.TileHeight;
                    cmd.Parameters.Add("@TileWidth", SqlDbType.Int).Value = o.TileWidth;

                    DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
                }


                if (id > 0)
                {
                    InsertUpdateObjectDirectories(o.ObjectDirectories, id);
                    InsertUpdateMapObjects(o.MapObjects, id);
                }
            }
        }

        #endregion

        #region Object Directory

        /// <summary>
        /// Gets all object directores with the given parent
        /// </summary>
        /// <param name="parent">The parent id of the object directories to get</param>
        /// <returns>All object directories with the given parent</returns>
        public static ObjectDirectories GetObjectDirectories(long parent)
        {
            ObjectDirectories toReturn = new ObjectDirectories();
            SqlCommand cmd = new SqlCommand();
            ObjectDirectory temp = null;
            DataTable dt = null;
            string query = "SELECT * FROM [ObjectDirectory] WHERE Parent = @Parent";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.Add(new SqlParameter("@Parent", SqlDbType.BigInt)).Value = parent;
            dt = DatabaseHelper.ExecuteQuery(GetConnectionString(), cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new ObjectDirectory();
                    temp.ID = DatabaseHelper.GetValidValueFromObject(dr["ID"], (long)0);
                    temp.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);

                    toReturn.Add(temp);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets the object directory with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object directory to get.</param>
        /// <returns>The object directory with the given ID</returns>
        public static ObjectDirectory GetObjectDirectory(long id)
        {
            ObjectDirectory toReturn = null;
            DataTable dt = null;
            SqlCommand cmd = new SqlCommand();
            string query = "SELECT * FROM [ObjectDirectory] WHERE ID = @ID";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;
            dt = DatabaseHelper.ExecuteQuery(GetConnectionString(), cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    toReturn = new ObjectDirectory();
                    toReturn.ID = DatabaseHelper.GetValidValueFromObject(dr["ID"], (long)0);
                    toReturn.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Deletes from the database any Object Directory that is no longer in this list, then calls insertupdate on each object
        /// </summary>
        /// <param name="os">The list of object directories to have in the database</param>
        /// <param name="parent">The parent of the object directories</param>
        private static void InsertUpdateObjectDirectories(ObjectDirectories os, long parent)
        {
            ObjectDirectories curr = GetObjectDirectories(parent);

            if (curr != null && curr.Count > 0 && os != null)
            {
                foreach (ObjectDirectory o in curr)
                {
                    if (os.FindByID(o.ID) == null)
                    {
                        DeleteObjectDirectory(o.ID);
                    }
                }
            }

            foreach (ObjectDirectory o in os)
            {
                InsertUpdateObjectDirectory(o, parent);
            }
        }

        /// <summary>
        /// Inserts or Updates an object directory, based on the given object directory's ID
        /// </summary>
        /// <param name="o">The object directory to insert/update</param>
        /// <param name="parent">The parent of the object directory</param>
        private static void InsertUpdateObjectDirectory(ObjectDirectory o, long parent)
        {
            string query = string.Empty;
            SqlCommand cmd = null;
            long id = 0;

            if (o != null)
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                //Update
                if (o.ID > 0)
                {
                    query = "UPDATE [ObjectDirectory] SET Name = @Name, Parent = @Parent WHERE ID = @ID; SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = o.ID;
                }
                //Insert
                else
                {
                    query = "INSERT INTO [ObjectDirectory] (Name, Parent) VALUES (@Name, @Parent); SELECT SCOPE_IDENTITY();";
                }

                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = o.Name;
                cmd.Parameters.Add("@Parent", SqlDbType.BigInt).Value = parent;

                cmd.CommandText = query;
                id = DatabaseHelper.ExecuteCommandContainingGetIdentity(GetConnectionString(), cmd);

                if (id > 0)
                {
                    InsertUpdateObjectDirectories(o.ObjectDirectories, id);
                    InsertUpdateMapObjects(o.MapObjects, id);
                }
            }
        }

        /// <summary>
        /// Deletes an object directory by id
        /// </summary>
        /// <param name="id">ID</param>
        public static void DeleteObjectDirectory(long id)
        {
            SqlCommand cmd = new SqlCommand();
            string query = "DELETE FROM ObjectDirectory WHERE ID = @ID";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;

            DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
        }

        #endregion

        #region Map Object

        /// <summary>
        /// Gets a collection of map objects that have the given parent
        /// </summary>
        /// <param name="parent">The parent to find map objects for</param>
        /// <returns>A collection of map objects with the given parent</returns>
        public static MapObjects GetMapObjects(long parent)
        {
            MapObjects toReturn = new MapObjects();
            MapObject temp = null;
            DataTable dt = null;
            SqlCommand cmd = new SqlCommand();
            string query = "SELECT * FROM [MapObject] WHERE Parent = @Parent";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.Add(new SqlParameter("@Parent", SqlDbType.BigInt)).Value = parent;
            dt = DatabaseHelper.ExecuteQuery(GetConnectionString(), cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new MapObject();
                    temp.ID = DatabaseHelper.GetValidValueFromObject(dr["ID"], (long)0);
                    temp.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);
                    if (dr["Structure"] != DBNull.Value)
                    {
                        temp.TileMap = TileMap.CreateTileMapFromByteArray((byte[])dr["Structure"]);
                    }
                    else
                    {
                        temp.TileMap = null;
                    }

                    toReturn.Add(temp);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets a map object by ID
        /// </summary>
        /// <param name="id">The ID of the map object to get</param>
        /// <returns>Returns the map object with the given ID.  Null if none exists.</returns>
        public static MapObject GetMapObject(long id)
        {
            MapObject toReturn = null;
            DataTable dt = null;
            SqlCommand cmd = new SqlCommand();
            string query = "SELECT * FROM [MapObject] WHERE ID = @ID";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.Add("@Parent", SqlDbType.BigInt).Value = id;
            dt = DatabaseHelper.ExecuteQuery(GetConnectionString(), cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    toReturn = new MapObject();
                    toReturn.ID = DatabaseHelper.GetValidValueFromObject(dr["ID"], (long)0);
                    toReturn.Name = DatabaseHelper.GetValidValueFromObject(dr["Name"], string.Empty);
                    if (dr["Structure"] != null)
                    {
                        toReturn.TileMap = TileMap.CreateTileMapFromByteArray((byte[])dr["Structure"]);
                    }
                    else
                    {
                        toReturn.TileMap = null;
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Deletes from the database any Map Object that is no longer in this list, then calls insertupdate on each object
        /// </summary>
        /// <param name="os">The list of map objects to have in the database</param>
        /// <param name="parent">The parent of the map objects</param>
        private static void InsertUpdateMapObjects(MapObjects os, long parent)
        {
            MapObjects curr = GetMapObjects(parent);

            if (curr != null && curr.Count > 0 && os != null)
            {
                foreach (MapObject o in curr)
                {
                    if (os.FindByID(o.ID) == null)
                    {
                        DeleteMapObject(o.ID);
                    }
                }
            }

            foreach (MapObject o in os)
            {
                InsertUpdateMapObject(o, parent);
            }
        }

        /// <summary>
        /// Inserts or Updates a map object, based on the given map object's ID
        /// </summary>
        /// <param name="o">The map object to insert/update</param>
        /// <param name="parent">The parent of the map object</param>
        private static void InsertUpdateMapObject(MapObject o, long parent)
        {
            SqlCommand cmd = new SqlCommand();
            string query = string.Empty;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;


            if (o != null)
            {
                //Update
                if (o.ID > 0)
                {
                    query = "UPDATE [MapObject] SET Name = @Name, Structure = @Structure, Parent = @Parent WHERE ID = @ID";
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = o.ID;
                }
                //Insert
                else
                {
                    query = "INSERT INTO [MapObject] (Name, Structure, Parent) VALUES (@Name, @Structure, @Parent)";
                }

                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = o.Name;
                cmd.Parameters.Add("@Structure", SqlDbType.VarBinary).Value = o.TileMap != null ? o.TileMap.GetBytesFromTileMap() : (object)DBNull.Value;
                cmd.Parameters.Add("@Parent", SqlDbType.BigInt).Value = parent;

                cmd.CommandText = query;
                DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
            }
        }

        /// <summary>
        /// Deletes a map object with the given ID
        /// </summary>
        /// <param name="id">The ID of the map object to delete</param>
        private static void DeleteMapObject(long id)
        {
            SqlCommand cmd = new SqlCommand();
            string query = "DELETE FROM MapObject WHERE ID = @ID";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;

            DatabaseHelper.ExecuteCommand(GetConnectionString(), cmd);
        }

        #endregion
    }
}
