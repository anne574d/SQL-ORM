﻿using System;
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