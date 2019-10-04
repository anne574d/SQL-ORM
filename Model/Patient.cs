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
    class Patient
    {
        public Patient() { }
        public Patient(int pk)
        {
            id = pk;
            Read();
        }

        public void Read()
        {
            List<string> columns = new List<string>
            {
                "Name", "Species", "DateOfBirth", "DiedOn", "OwnerID"
            };
            List<string> keys = new List<string> { "ID" };
            ArrayList values = new ArrayList { id };

            DataTable table = SQL.Instance.Select(tableName, columns, keys, values);
            foreach (DataRow row in table.Rows)
            {
                name = row["Name"].ToString().Trim();
                species = new Species(row["Species"].ToString().Trim());
                dateOfBirth = (DateTime)row["DateOfBirth"];
                if (row["DiedOn"] != DBNull.Value)
                {
                    diedOn = (DateTime)row["DiedOn"];
                }
                owner = new Owner((int)row["OwnerID"]);
            }
        }

        public void Insert()
        {
            List<string> keys = new List<string>
            {
                "Name", "Species", "DateOfBirth", "OwnerID"
            };
            ArrayList values = new ArrayList()
            {
                name, species.Name, dateOfBirth, owner.ID
            };

            // Only include "diedOn" if value is not NULL
            if (diedOn != DateTime.MinValue)
            {
                keys.Add("DiedOn");
                values.Add(diedOn);
            }

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
                "Name", "Species", "DateOfBirth", "OwnerID"
            };

            ArrayList values = new ArrayList
            {
                name, species.Name, dateOfBirth, owner.ID
            };

            // Only include "diedOn" if value is not NULL
            if (diedOn != DateTime.MinValue)
            {
                keys.Add("DiedOn");
                values.Add(diedOn);
            }

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

        public void Print()
        {
            string result = $"" +
                $"({id}) {name} - {species.Name}\n" +
                $"{owner.FirstName} {owner.LastName}\n" +
                $"{dateOfBirth.ToString("dd-MM-yyyy")}";
            if (diedOn != DateTime.MinValue)
            {
                result += $" - {diedOn.ToString("dd-MM-yyyy")}";
            }
        }

        private string tableName = "Patients";

        private int id;
        private Owner owner;
        private string name;
        private Species species;
        private DateTime dateOfBirth;
        private DateTime diedOn;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Owner Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Species Species
        {
            get { return species; }
            set { species = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public DateTime DiedOn
        {
            get { return diedOn; }
            set { diedOn = value; }
        }
    }   
}
