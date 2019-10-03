using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlObject.Model
{
    class Consultation
    {
        public Consultation(SqlConnection c)
        {
            conn = c;
        }

        public void Insert()
        {
            // ID is auto incrementing and cannot be specified in an INSERT query
            List<string> keys = new List<string>
            {
                "TreatmentID", "PatientID", "Price", "ConsultationDate"
            };
            ArrayList values = new ArrayList()
            {
                //description, price, species.Name
            };

            SQL.Insert(conn, "Consultations", keys, values);
        }
        public void Delete()
        {
            SQL.Delete(conn, "Consultations", "ID", id.ToString());
        }
        public void Update()
        {
            List<string> keys = new List<string>
            {
                "TreatmentID", "PatientID", "Price", "ConsultationDate"
            };

            ArrayList values = new ArrayList
            {
                //description, price, species.Name
            };

            SQL.Update(conn, "Consultations", keys, values, "ID", id.ToString());
        }

        private SqlConnection conn;

        private int id;
        private Treatment treatment;
        private Patient patient;
        private decimal price;
        private DateTime consultationDate;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Treatment Treatment
        {
            get { return treatment; }
            set { treatment = value; }
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
