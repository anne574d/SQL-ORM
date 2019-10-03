using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;


namespace SqlObject.Model
{
    class SQL
    {
        // insert and return the primary key (parsed to an int)
        public static int Insert(SqlConnection conn, string tableName, List<string> keys, ArrayList values, string pKey)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception("Mismatch between length of keys and values lists");
            }

            string query = $"INSERT INTO {tableName} ({string.Join(", ", keys)}) OUTPUT INSERTED.{pKey} VALUES (@{string.Join(", @", keys)})";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < keys.Count; ++i)
            {
                cmd.Parameters.AddWithValue($"@{keys[i]}", values[i]);
            }

            // Open connection, execute query, return primary key of new post
            Debug.WriteLine($"Executing: {cmd.CommandText}");
            conn.Open();
            int newPK = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();

            return newPK;
        }

        // insert without pk return
        public static void Insert(SqlConnection conn, string tableName, List<string> keys, ArrayList values)
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

        public static int Delete(SqlConnection conn, string tableName, string pKey, string pValue)
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

        public static int Update(SqlConnection conn, string tableName, List<string> keys, ArrayList values, string pKey, string pValue)
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

        public static void Execute(string procedureName, List<string> parameters, ArrayList values)
        {
            if (parameters.Count != values.Count)
            {
                throw new Exception("Mismatch between length of parameters and values lists");
            }

            
            /* TODO
             * 
            List<string> updates = new List<string>();
            for (int i = 0; i < keys.Count; ++i)
            {
                updates.Add($"{keys[i]} = @{keys[i]}");
            }


            string query = $"EXEC {procedureName} {}"


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

            */
        }
    }
}
