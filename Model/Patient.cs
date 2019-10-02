using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace SqlObject.Model
{
    class Patient
    {
        public Patient(SqlConnection c)
        {
            conn = c;
        }

        public void Read()
        {
            //
        }

        public void Insert()
        {
            List<string> keys = new List<string>
            {
                "OwnerID", "Name", "Species", "DateOfBirth", "DiedOn"
            };

            ArrayList values = new ArrayList()
            {
                owner.ID, name, species.Name, dateOfBirth, diedOn
            };

            SQL.Insert(conn, "Patients", keys, values);
        }

        public void Delete()
        {
            List<string> keys = new List<string>
            {
                "PatientID", "OwnerID", "Name", "Species", "DateOfBirth", "DiedOn"
            };

            ArrayList values = new ArrayList
            {
                id, owner.ID, name, species, dateOfBirth, diedOn
            };

            SQL.Delete(conn, "Patients", keys, values);
        }


        public void Update()
        {
            //    
        }

        private SqlConnection conn;

        private int id;
        private Owner owner;
        private string name;
        private Species species;
        private DateTime dateOfBirth, diedOn;

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
