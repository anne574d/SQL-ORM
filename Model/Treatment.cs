using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace SqlObject.Model
{
    class Treatment
    {
        public Treatment(SqlConnection c)
        {
            conn = c;
        }

        public void Insert()
        {
            // ID is auto incrementing and cannot be specified in an INSERT query
            List<string> keys = new List<string>
            {
                "Description", "Price", "SpeciesType"
            };

            ArrayList values = new ArrayList()
            {
                description, price, species.Name
            };

            id = SQL.Insert(conn, tableName, keys, values, "ID");
        }
        public void Delete()
        {
            SQL.Delete(conn, tableName, "ID", id.ToString());
        }
        public void Update()
        {
            List<string> keys = new List<string>
            {
                "Description", "Price", "SpeciesType"
            };

            ArrayList values = new ArrayList
            {
                description, price, species.Name
            };

            SQL.Update(conn, tableName, keys, values, "ID", id.ToString());
        }

        private SqlConnection conn;
        private string tableName = "Treatments";

        private int id;
        private string description;
        private decimal price;
        private Species species;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public Species Species
        {
            get { return species; }
            set { species = value; }
        }

    }
}
