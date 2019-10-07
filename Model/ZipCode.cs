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
    class ZipCode
    {
        public ZipCode() { }
        public ZipCode(int primaryKey)
        {
            zipcode = primaryKey;
            Read();
        }

        public void Read()
        {
            List<string> columns = new List<string> { "CityName" };
            List<string> keys = new List<string> { "ZipCode" };
            ArrayList values = new ArrayList { zipcode };

            DataTable table = SQL.Instance.Select(tableName, columns, keys, values);
            foreach (DataRow row in table.Rows)
            {
                cityName = row["CityName"].ToString().Trim();
            }
        }

        public void Insert()
        {
            List<string> keys = new List<string>
            {
                "ZipCode", "CityName"
            };
            ArrayList values = new ArrayList
            {
                zipcode, cityName
            };

            SQL.Instance.Insert(tableName, keys, values);
        }
        public int Delete()
        {
            List<string> keys = new List<string> { "ZipCode" };
            ArrayList values = new ArrayList { zipcode };
            int rows = SQL.Instance.Delete(tableName, keys, values);
            return rows;
        }
        public void Update()
        {
            List<string> keys = new List<string>
            {
                "CityName"
            };
            ArrayList values = new ArrayList
            {
                cityName
            };

            SQL.Instance.Update(tableName, keys, values, "ZipCode", zipcode.ToString());
        }

        private string tableName = "ZipCodes";
        
        private int zipcode;
        private string cityName;
        public int Number
        {
            get { return zipcode; }
            set { zipcode = value; }
        }
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
    }
}
