using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlObject.Model
{
    class Treatment
    {
        public Treatment(SqlConnection c)
        {
            conn = c;
        }

        private SqlConnection conn;

        private int id;
        private string description;
        private decimal price;
        private Species species;

        public int ID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
