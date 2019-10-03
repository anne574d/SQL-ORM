using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using SqlObject.Model;

namespace SqlObject
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnection conn = getConnection("localhost", "PetClinic2", "ormLogin", "123456");

            Patient patient = new Patient(conn);
            patient.Name = "Willow";
            patient.Species = new Species(conn) { Name = "FUGL" };
            patient.DateOfBirth = new DateTime(2012,6,1);
            //patient.DiedOn = new DateTime(2017, 5, 19);
            patient.Owner = new Owner(conn) { ID = 6 };

            patient.Insert();

            Console.WriteLine("Completed");
            Console.ReadLine();

        }

        static SqlConnection getConnection(string server, string dbname, string username, string password)
        {
            string connString = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = dbname,
                UserID = username,
                Password = password
            }.ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return conn;
        }

        static SqlConnection getConnection()
        {
            string connString = new SqlConnectionStringBuilder()
            {
                DataSource = "127.0.0.1",
                InitialCatalog = "PetClinic",
                UserID = "sa",
                Password = "sQLpass050919"
            }.ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }

    }
}

/*
Klasse med connection

    dependency injection, ormen skal ikke selv oprette forbindelse
  

    i modellen lav en 1-1 relation imellem tabeller og metoder

    lav privates til all kolonner samt :

    private int patientID
    public PatientID{ get ... ; set ... ; }


    Foreign keys skal refereceres med deres egen klasse
 */
