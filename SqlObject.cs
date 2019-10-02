using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlObject
{
    class SqlObject
    {
        public SqlObject() // constructor
        {

        }
        public SqlObject(string serverAddr, string dbName, string username, string password) // constructor
        {
            this.ServerAddress = serverAddr;
            this.DatabaseName = dbName;
            this.Username = username;
            this.Password = password;
        }

        ~SqlObject() // destructor
        {
            conn.Close();
        }

        public void Connect()
        {

            Console.WriteLine(conn.ConnectionString);

        }



        public void Select()
        {
            string sql = "SELECT Firstname FROM Owners;";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }

        public int Insert()
        {
            int rowsAffected;

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("INSERT Employees (Name, Location) ");
            sb.Append("VALUES (@name, @location);");

            string sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@name", "Jake");
                command.Parameters.AddWithValue("@location", "United States");
                rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " row(s) inserted");
            }
            return rowsAffected;
        }
        


    // fields
    public string ServerAddress { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // privates
        private SqlConnection conn;

    }
}
