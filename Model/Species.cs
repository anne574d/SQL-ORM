using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlObject.Model
{
    class Species
    {
        public Species() { }
        public Species(string primaryKey)
        {
            name = primaryKey;
        }

        public void Insert()
        {
            List<string> keys = new List<string>
            {
                "SpeciesName"
            };
            ArrayList values = new ArrayList
            {
                name
            };

            SQL.Instance.Insert(tableName, keys, values);
        }

        // Species is a single-columned table which acts as foreign key in a lot of different tables.
        // Therefore we do not wish to update or delete anything from this table. 
        // It is only possible to add new Species.

        private string tableName = "Species";
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
