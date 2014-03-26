using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace XNATools.ToolHelper
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Executes the command query
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="commandString">Command text to be executed</param>
        /// <returns>Results of the Query</returns>
        public static DataTable ExecuteQuery(string connString, SqlCommand cmd)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            DataTable dt = null;
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(connString);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt = new DataTable();
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }

                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return dt;
        }

        /// <summary>
        /// Executes a query with the query text without parameters.
        /// </summary>
        /// <param name="connString">The connection string for the database</param>
        /// <param name="query">The query to make to the database</param>
        /// <returns>The databable returned from the query</returns>
        public static DataTable ExecuteTextCommandQuery(string connString, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.Text;
            return ExecuteQuery(connString, cmd);
        }

        /// <summary>
        /// Executes a query with the query text.  parameters are optional.
        /// </summary>
        /// <param name="connString">The connection string for the database</param>
        /// <param name="query">The query to make to the database</param>
        /// <param name="parameters">The parameters of the query.  Optional</param>
        /// <returns>The databable returned from the query</returns>
        public static DataTable ExecuteTextCommandQuery(string connString, string query, SqlParameterCollection parameters)
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.Text;

            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return ExecuteQuery(connString, cmd);
        }

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="command">SQL command to be executed</param>
        /// <returns>Results of the execution</returns>
        public static int ExecuteStoredProcedure(string connString, SqlCommand command)
        {
            int result = 0;
            SqlConnection conn = null;

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(connString);
                command.Connection = conn;
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                result = command.ExecuteNonQuery();
            }
            //catch(Exception ex) { result = 0; }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="command">SQL command to be executed</param>
        /// <returns>Results of the execution</returns>
        public static int ExecuteCommand(string connString, SqlCommand command)
        {
            int result = 0;
            SqlConnection conn = null;

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(connString);
                command.Connection = conn;
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            //catch(Exception ex) { result = 0; }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Executes a command containing "SELECT SCOPE_IDENTITY();" at the end.  Returns the identity as a long
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="command">SQL command to be executed</param>
        /// <returns>Results of the execution</returns>
        public static long ExecuteCommandContainingGetIdentity(string connString, SqlCommand command)
        {
            object result = 0;
            SqlConnection conn = null;

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(connString);
                command.Connection = conn;
                conn.Open();
                result = command.ExecuteScalar();
            }
            //catch(Exception ex) { result = 0; }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return DatabaseHelper.GetValidValueFromObject(result, (long)0);
        }

        /// <summary>
        /// Gets a valid string from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a string from</param>
        /// <param name="defaultValue">The value to set to if invalid string</param>
        /// <returns>A valid string</returns>
        public static string GetValidValueFromObject(object o, string defaultValue)
        {
            return o != null ? o.ToString() : defaultValue;
        }

        /// <summary>
        /// Gets a valid byte from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a byte from</param>
        /// <param name="defaultValue">The value to set to if invalid byte</param>
        /// <returns>A valid byte</returns>
        public static byte GetValidValueFromObject(object o, byte defaultValue)
        {
            byte parser = 0;
            return o != null && byte.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid short from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a short from</param>
        /// <param name="defaultValue">The value to set to if invalid short</param>
        /// <returns>A valid short</returns>
        public static short GetValidValueFromObject(object o, short defaultValue)
        {
            short parser = 0;
            return o != null && short.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid int from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a int from</param>
        /// <param name="defaultValue">The value to set to if invalid int</param>
        /// <returns>A valid int</returns>
        public static int GetValidValueFromObject(object o, int defaultValue)
        {
            int parser = 0;
            return o != null && int.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid long from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a long from</param>
        /// <param name="defaultValue">The value to set to if invalid long</param>
        /// <returns>A valid long</returns>
        public static long GetValidValueFromObject(object o, long defaultValue)
        {
            long parser = 0;
            return o != null && long.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid float from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a float from</param>
        /// <param name="defaultValue">The value to set to if invalid float</param>
        /// <returns>A valid float</returns>
        public static float GetValidValueFromObject(object o, float defaultValue)
        {
            float parser = 0;
            return o != null && float.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid double from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a double from</param>
        /// <param name="defaultValue">The value to set to if invalid double</param>
        /// <returns>A valid double</returns>
        public static double GetValidValueFromObject(object o, double defaultValue)
        {
            double parser = 0;
            return o != null && double.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid bool from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a bool from</param>
        /// <param name="defaultValue">The value to set to if invalid bool</param>
        /// <returns>A valid bool</returns>
        public static bool GetValidValueFromObject(object o, bool defaultValue)
        {
            bool parser = false;
            return o != null && bool.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }

        /// <summary>
        /// Gets a valid DateTime from the object.  Returns defaultValue if invalid
        /// </summary>
        /// <param name="o">The object to get a DateTime from</param>
        /// <param name="defaultValue">The value to set to if invalid DateTime</param>
        /// <returns>A valid DateTime</returns>
        public static DateTime GetValidValueFromObject(object o, DateTime defaultValue)
        {
            DateTime parser = DateTime.Now;
            return o != null && DateTime.TryParse(o.ToString(), out parser) ? parser : defaultValue;
        }
    }
}
