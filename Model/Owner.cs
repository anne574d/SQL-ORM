using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace SqlObject.Model
{
    class Owner
    {
        public Owner(){ }

        public Owner(int pk)
        {
            Console.WriteLine($"Created new owner with ID {pk}");
            id = pk;
            Read();
        }
        public void Read()
        {
            List<string> columns = new List<string>
            {
                "FirstName", "LastName", "Phone", "Email", "Address", "ZipCode"
            };
            List<string> keys = new List<string> { "ID" };
            ArrayList values = new ArrayList { id }; 

            DataTable table = SQL.Instance.Select(tableName, columns, keys, values);
            foreach(DataRow row in table.Rows)
            {
                firstName = row["FirstName"].ToString().Trim();
                lastName = row["LastName"].ToString().Trim();
                if (row["Phone"] != DBNull.Value)
                {
                    phone = row["Phone"].ToString().Trim();
                }
                if (row["Email"] != DBNull.Value)
                {
                    email = row["Email"].ToString().Trim();
                }
                address = row["Address"].ToString().Trim();
                zipCode = new ZipCode((int)row["ZipCode"]);
            }
        }

        public void Insert()
        {
            // ID is auto incremented, cannot be inserted manually
            List<string> keys = new List<string>
            {
                "FirstName", "LastName", "Address", "ZipCode"
            };
            ArrayList values = new ArrayList
            {
                firstName, lastName, address, zipCode.Number
            };

            // Nullable values
            if (!string.IsNullOrEmpty(phone))
            {
                keys.Add("Phone");
                values.Add(phone);
            }
            if (!string.IsNullOrEmpty(email))
            {
                keys.Add("Email");
                values.Add(email);
            }

            id = int.Parse(SQL.Instance.Insert(tableName, keys, values, "ID"));
            Debug.WriteLine($"New post added with ID = {id}");
        }

        public void Delete()
        {
            SQL.Instance.Delete(tableName, "ID", id.ToString());
        }

        public void Update()
        {
            List<string> keys = new List<string>
            {
                "FirstName", "LastName", "Address", "ZipCode"
            };
            ArrayList values = new ArrayList
            {
                firstName, lastName, address, zipCode.Number
            };

            // Nullable values
            if (!string.IsNullOrEmpty(phone))
            {
                keys.Add("Phone");
                values.Add(phone);
            }
            if (!string.IsNullOrEmpty(email))
            {
                keys.Add("Email");
                values.Add(email);
            }

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

        // Udpate a single column
        public void Update(string columnName)
        {
            List<string> keys = new List<string> { columnName };
            ArrayList values = new ArrayList();

            switch (columnName)
            {
                case "FirstName": values.Add(firstName); break;
                case "LastName": values.Add(lastName); break;
                case "Phone": values.Add(phone); break;
                case "Email": values.Add(email); break;
                case "Address": values.Add(address); break;
                case "ZipCode": values.Add(zipCode.Number); break;
                default: throw new Exception($"Failed to update: Invalid column name \"{columnName}\"");
            }

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

        public void Print()
        {
            string result = $"({id}) {firstName} {lastName}\n" +
                $"{address}\n" +
                $"{zipCode.Number} {zipCode.CityName}\n" +
                $"{phone}\n" +
                $"{email}";
            Console.WriteLine(result);
        }

        private string tableName = "Owners";

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
