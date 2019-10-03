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

        public void Delete()
        {
            SQL.Delete(conn, "Owners", "ID", id.ToString());
        }

        public void Update()
        {
            List<string> keys = new List<string>
            {
                "FirstName", "LastName", "Phone", "Email", "Address", "ZipCode"
            };
            ArrayList values = new ArrayList
            {
                firstName, lastName, phone, email, address, zipCode.Number
            };

            SQL.Update(conn, "Owners", keys, values, "ID", id.ToString());
        }

        private SqlConnection conn;

        private string firstName, lastName, phone, email, address;
        private int id;
        ZipCode zipCode;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public ZipCode ZipCodes
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
    }
}
