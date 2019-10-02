using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SqlObject.Model
{
    class Owner
    {
        public Owner(SqlConnection c)
        {
            conn = c;
        }

        private SqlConnection conn;

        private string firstName, lastName, phone, email, address;
        private int id, zipCode;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ZipCodes { get; set; }
    }
}
