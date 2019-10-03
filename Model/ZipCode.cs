using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace SqlObject.Model
{
    class ZipCode
    {
        public ZipCode(SqlConnection c)
        {
            conn = c;
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

            SQL.Insert(conn, "ZipCodes", keys, values);
        }
        public void Delete()
        {
            SQL.Delete(conn, "ZipCodes", "ZipCode", zipcode.ToString());
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

            SQL.Update(conn, "ZipCodes", keys, values, "ZipCode", zipcode.ToString());
        }

        private SqlConnection conn;

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
