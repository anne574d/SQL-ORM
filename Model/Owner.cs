using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SqlObject.Model
{
    class Owner
    {
        public Owner(SqlConnection c)
        {
            conn = c;
        }

        public void Insert()
        {
            // ID is auto incremented, cannot be inserted manually
            List<string> keys = new List<string>
            {
                "FirstName", "LastName", "Phone", "Email", "Address", "ZipCodes"
            };
            ArrayList values = new ArrayList
            {
                firstName, lastName, phone, email, address, zipCode.Number
            };

            SQL.Insert(conn, "Owners", keys, values);

        }

        private SqlConnection conn;

        private string firstName, lastName, phone, email, address;
        private int id;
        ZipCode zipCode;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ZipCode ZipCodes { get; set; }
    }
}
