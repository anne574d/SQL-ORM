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
            SQL.Instance.ConnectionInfo("localhost", "PetClinic2", "ormLogin", "123456");

            selectExamples();
            //insertExamples();
            //updateExamples();
            //deleteExamples();

            Console.WriteLine("Examples complete. Press any key to exit... ");
            Console.ReadLine();
        }

        public static void deleteExamples()
        {
            Console.Clear();
            Console.WriteLine("DELETE example\n");

            Patient patient = new Patient(11);
            int rowsAffected = patient.Delete();
            Console.WriteLine($"{rowsAffected} rows affected.\n");

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }

        public static void updateExamples()
        {
            Console.Clear();
            Console.WriteLine("UPDATE examples\n");

            Owner owner = new Owner(1);
            owner.Print();

            owner.Email = "newemailaddress@email.dk";
            owner.Update();
            
            Owner updatedOwner = new Owner(1);
            owner.Print();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }

        public static void insertExamples()
        {
            Console.Clear();
            Console.WriteLine("INSERT examples\n"); 

            Patient patient = new Patient();
            patient.Name = "Gecco";
            patient.Species = new Species("KRYBDYR");
            patient.DateOfBirth = new DateTime(2016, 1, 4);
            patient.Owner = new Owner() { ID = 10 };
            patient.Insert();
            Console.WriteLine($"Created new patient post with ID = {patient.ID}\n");

            Species krybdyr = new Species("HEST");
            krybdyr.Insert();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }

        public static void selectExamples()
        {
            Console.Clear();
            Console.WriteLine("SELECT examples\n");

            Owner owner1 = new Owner(1);
            owner1.Print();
            
            Owner owner2 = new Owner(5);
            owner2.Print();

            Patient p1 = new Patient(6);
            p1.Print();

            Patient p2 = new Patient(5);
            p2.Print();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
