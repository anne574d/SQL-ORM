using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlObject.Model
{
    class Consultation
    {
        public Consultation() { }
        public Consultation(int pk)
        {
            id = pk;
            Read();
        }

        public void Read()
        {
            List<string> columns = new List<string>
            {
                "TreatmentID", "PatientID", "Price", "ConsultationDate", "Invoice"
            };
            List<string> keys = new List<string> { "ID" };
            ArrayList values = new ArrayList { id };

            DataTable table = SQL.Instance.Select(tableName, columns, keys, values);
            foreach (DataRow row in table.Rows)
            {
                treatment = new Treatment((int)row["TreatmentID"]);
                patient = new Patient((int)row["PatientID"]);
                price = (decimal)row["Price"];
                consultationDate = (DateTime) row["ConsultationDate"];
                invoice = (bool)row["Invoice"];
            }
        }

        public void Insert()
        {
            // ID is auto incrementing and cannot be specified in an INSERT query
            List<string> keys = new List<string>
            {
                "TreatmentID", "PatientID", "Price"
            };
            ArrayList values = new ArrayList()
            {
                treatment.ID, patient.ID, price
            };

            // Default set values:
            if (consultationDate != DateTime.MinValue)
            {
                keys.Add("ConsultationDate");
                values.Add(consultationDate);
            }
            if (invoice)
            {
                keys.Add("Invoice");
                values.Add(invoice);
            }

            id = int.Parse(SQL.Instance.Insert(tableName, keys, values, "ID"));
        }
        public void Delete()
        {
            SQL.Instance.Delete(tableName, "ID", id.ToString());
        }

        // Update all columns
        public void Update()
        {
            List<string> keys = new List<string>
            {
                "TreatmentID", "PatientID", "Price"
            };

            ArrayList values = new ArrayList
            {
                treatment.ID, patient.ID, price
            };

            // Default set values:
            if (consultationDate != DateTime.MinValue)
            {
                keys.Add("ConsultationDate");
                values.Add(consultationDate);
            }
            if (invoice)
            {
                keys.Add("Invoice");
                values.Add(invoice);
            }

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

        // Update a single column
        public void Update(string columnName)
        {
            List<string> keys = new List<string> { columnName };
            ArrayList values = new ArrayList();

            switch (columnName)
            {
                case "TreatmentID": values.Add(treatment.ID); break;
                case "PatientID": values.Add(patient.ID); break;
                case "Price": values.Add(price); break;
                case "ConsultationDate": values.Add(consultationDate); break;
                case "Invoice": values.Add(invoice); break;
                default: throw new Exception($"Failed to update: Invalid column name \"{columnName}\"");
            }

            SQL.Instance.Update(tableName, keys, values, "ID", id.ToString());
        }

        private SqlConnection conn;
        private string tableName = "Consultations";

        private int id;
        private Treatment treatment;
        private Patient patient;
        private decimal price;
        private DateTime consultationDate;
        private bool invoice = false;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Treatment Treatment
        {
            get { return treatment; }
            set {
                    if (value.Species.Name != patient.Species.Name)
                    {
                        throw new Exception($"Treatment does not match patient's species. " +
                            $"({value.Species.Name} is not {patient.Species.Name})");
                    }
                    else
                    {
                        treatment = value;
                    }
                }
        }
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public DateTime ConsultationDate
        {
            get { return consultationDate; }
            set { consultationDate = value; }
        }
        public bool Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }
    }
}
