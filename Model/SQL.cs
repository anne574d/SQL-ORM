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
        // Make SQL a singleton, which is initialized once with connection info
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

        // Add/change SqlConnection object's parameters
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
        public DataTable SelectAll(string tableName, List<string> columns)
        {
            string query = $"SELECT {string.Join(", ", columns)} FROM {tableName}";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(reader);
            conn.Close();
            return result;
        }

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

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(reader);
            conn.Close();
            return result;
        }

        // insert post and return the value of the "output"-column
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

            // Open connection, execute query, return primary key of new post
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            string result = cmd.ExecuteScalar().ToString();
            cmd.Connection.Close();

            return result;
        }

        // insert without returning an output
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

            // Open connection, execute query
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            cmd.ExecuteScalar();
            cmd.Connection.Close();
        }

        public int Delete(string tableName, string pKey, string pValue)
        {
            string query = $"DELETE FROM {tableName} WHERE {pKey} = @{pKey}";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue($"@{pKey}", pValue);

            // Open connection, execute query, return rows affected
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rowsAffected} rows affected");
            return rowsAffected;
        }

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
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rowsAffected} rows affected");
            return rowsAffected;
        }

        public void Execute(string procedureName, List<string> parameters, ArrayList values)
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
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Debug.WriteLine($"{rowsAffected} rows affected");
            //return rowsAffected;
        }
    }
}
