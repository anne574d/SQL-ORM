using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace SqlObject.Model
{
    public class SQL
    {
        // Make SQL a singleton, which saves the given connection info
        // which can then be used anywhere in the program
        private static SQL instance;
        private SQL() { }
        public static SQL Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SQL();
                }
                return instance;
            }
        }

        // Add/change the parameters of the SqlConnection
        private SqlConnection conn;
        public void ConnectionInfo(string serverAddr, string databaseName, string username, string password)
        {
            string connString = new SqlConnectionStringBuilder()
            {
                DataSource = serverAddr,
                InitialCatalog = databaseName,
                UserID = username,
                Password = password
            }.ConnectionString;

            conn = new SqlConnection(connString);
        }

        // ######################################### SQL functions #########################################

        // Returns given columns for all posts in table 
        public DataTable SelectAll(string tableName, List<string> columns)
        {
            string query = $"SELECT {string.Join(", ", columns)} FROM {tableName}";
            SqlCommand cmd = new SqlCommand(query, conn);

            // Open connection, execute query
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            SqlDataReader reader = cmd.ExecuteReader();

            // Load reader data over to datatable object before closing connection
            DataTable result = new DataTable();
            result.Load(reader);
            conn.Close();

            return result;
        }

        // Returns given columns for the posts in table which fulfills conditions
        public DataTable Select(string tableName, List<string> columns, List<string> keys, ArrayList values)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            List<string> conditions = new List<string>();
            for (int i = 0; i < keys.Count; ++i)
            {
                conditions.Add($"{keys[i]} = @{keys[i]}");
            }

            string query = $"SELECT {string.Join(", ", columns)} FROM {tableName} WHERE {string.Join(" AND ",conditions)}";
            SqlCommand cmd = new SqlCommand(query, conn);
            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"{keys[i]}", values[i]);
            }

            // Open connection, execute query
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            SqlDataReader reader = cmd.ExecuteReader();

            // Load reader data over to datatable object before closing connection
            DataTable result = new DataTable();
            result.Load(reader);
            conn.Close();

            return result;
        }

        // Inserts post and returns the value of the "output"-column
        public string Insert(string tableName, List<string> keys, ArrayList values, string output)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            string query = $"INSERT INTO {tableName} ({string.Join(", ", keys)}) OUTPUT INSERTED.{output} VALUES (@{string.Join(", @", keys)})";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{keys[i]}", values[i]);
            }

            // Open connection, execute query...
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            string result = cmd.ExecuteScalar().ToString();
            cmd.Connection.Close();

            return result;
        }

        // Inserts without returning an output
        public void Insert(string tableName, List<string> keys, ArrayList values)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            string query = $"INSERT INTO {tableName} ({string.Join(", ", keys)}) VALUES (@{string.Join(", @", keys)})";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{keys[i]}", values[i]);
            }

            // Open connection, execute query, close connection
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            cmd.ExecuteScalar();
            cmd.Connection.Close();
        }

        // Delete post with a set of conditions
        public int Delete(string tableName, List<string> keys, ArrayList values)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            List<string> conditions = new List<string>();
            for (int i = 0; i < keys.Count; ++i)
            {
                conditions.Add($"{keys[i]} = @{keys[i]}");
            }

            string query = $"DELETE FROM {tableName} WHERE {string.Join(" AND ", conditions)}";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{keys[i]}", values[i]);
            }

            // Open connection, execute query, return rows affected
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            int rows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rows} rows affected");
            return rows;
        }

        // Update all columns for post with given primary key/value
        public int Update(string tableName, List<string> keys, ArrayList values, string pKey, string pValue)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            List<string> updates = new List<string>();
            for (int i = 0; i < keys.Count; ++i)
            {
                updates.Add($"{keys[i]} = @{keys[i]}");
            }

            string query = $"UPDATE {tableName} SET {string.Join(", ", updates)} WHERE {pKey} = @{pKey}";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{keys[i]}", values[i]);
            }
            cmd.Parameters.AddWithValue($"@{pKey}", pValue);

            // Open connection, execute query, return rows affected
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            int rows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rows} rows affected");
            return rows;
        }

        // Execute functions commented out because they probably 
        // ought to be in a related class/a class of their own
        // instead of the generic CRUD

        /*
        // Execute SP and return rows affected
        public int Execute(string procedureName, List<string> parameters, ArrayList values)
        {
            if (parameters.Count != values.Count)
            {
                throw new Exception("Mismatch between length of parameters and values lists");
            }

            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < parameters.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{parameters[i]}", values[i]);
            }

            // Open connection, execute query, return rows affected
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rows} rows affected");
            return rows;
        }

        // Execute SP and return Datatable 
        public DataTable Execute(string procedureName, List<string> parameters, ArrayList values)
        {
            if (parameters.Count != values.Count)
            {
                throw new Exception("Mismatch between length of parameters and values lists");
            }

            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < parameters.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{parameters[i]}", values[i]);
            }

            // Open connection, execute query
            conn.Open();
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            SqlDataReader reader = cmd.ExecuteReader();

            // Load reader data over to datatable object before closing connection
            DataTable result = new DataTable();
            result.Load(reader);
            conn.Close();
        }*/

    }
}