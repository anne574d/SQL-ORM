using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;


namespace SqlObject.Model
{
    class SQL
    {
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

            execute(cmd);
        }
        public static void Delete(SqlConnection conn, string tableName, string pKey, string pValue)
        {
            string query = $"DELETE FROM {tableName} WHERE {pKey} = @{pKey}";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue($"@{pKey}", pValue);

            execute(cmd);
        }

        public static void Update(SqlConnection conn, string tableName, List<string> keys, ArrayList values, string pKey, string pValue)
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

            execute(cmd);
        }


        private static void execute(SqlCommand cmd)
        {
            Console.WriteLine("Executing: " + cmd.CommandText);
            cmd.Connection.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            cmd.Connection.Close();
        }

    }
}
