using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace SqlObject.Model
{
    class Treatment
    {
        public Treatment() { }
        public Treatment(int pk)
        {
            id = pk;
            Read();
        }

        public void Read()
        {
            List<string> columns = new List<string>
            {
                "Description", "Price", "SpeciesType"
            };
            List<string> keys = new List<string> { "ID" };
            ArrayList values = new ArrayList { id };

            DataTable table = SQL.Instance.Select(tableName, columns, keys, values);
            foreach (DataRow row in table.Rows)
            {
                description = row["Description"].ToString().Trim();
                price = (decimal)row["Price"];
                species = new Species(row["SpeciesType"].ToString().Trim());
            }
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

            id = int.Parse(SQL.Instance.Insert(tableName, keys, values, "ID"));
        }
        public void Delete()
        {
            SQL.Instance.Delete(tableName, "ID", id.ToString());
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

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

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
