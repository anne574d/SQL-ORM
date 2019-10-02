using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            // PatientID is auto incrementing, so it cannot be specified
            string query = $"INSERT INTO Patients " +
                $"(OwnerID, Name, Species, Birthday) " +
                $"VALUES " +
                $"(@ownerID, @name, @species, @birthday)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ownerID", owner.ID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@species", species.Name);
            cmd.Parameters.AddWithValue("@birthday", birthday);

            execute(cmd);
        }

        public void Delete()
        {
            string query = "DELETE FROM Patients WHERE " +
                "PatientID = @patientID AND " +
                "OwnerID = @ownerID AND " +
                "Name = @name AND " +
                "Species = @species AND " +
                "Birthday = @birthday";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", id);
            cmd.Parameters.AddWithValue("@ownerID", owner.ID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@species", species.Name);
            cmd.Parameters.AddWithValue("@birthday", birthday);

            execute(cmd);

        }
        public void Update()
        {
            //    
        }

        public void execute(SqlCommand cmd)
        {
            Console.WriteLine("Executing: " + cmd.CommandText);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            conn.Close();
        }

        private SqlConnection conn;

        private int id;
        private Owner owner;
        private string name;
        private Species species;
        private DateTime birthday;

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
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

    }
   
}
