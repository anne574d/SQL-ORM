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
            patient.Name = "Freja";
            patient.ID = 5;
            patient.Species = new Species() { Name = "KANIN" };
            patient.DateOfBirth = DateTime.Now;
            patient.DiedOn = DateTime.Now;
            patient.Owner = new Owner(conn) { ID = 6 };

            patient.Insert();
            patient.Delete();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
