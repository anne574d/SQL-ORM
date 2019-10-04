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
            /*
            Patient patient = new Patient();
            patient.Name = "Nemo";
            patient.Species = new Species() { Name = "FISK" };
            patient.DateOfBirth = new DateTime(2017,12,12);
            patient.Owner = new Owner() { ID = 10 };

            patient.Insert();
            */

            //Patient p = new Patient(6);

            Owner o1 = new Owner(1);
            o1.Print();
            Owner o2 = new Owner(5);
            o2.Print();
            Patient p1 = new Patient(6);
            //p1.Print();
            Patient p2 = new Patient(1);

            Console.WriteLine(p2.Owner.LastName);
            //p2.Print();

            /*
            Owner own = new Owner(1);
            Console.WriteLine($"" +
                $"{own.FirstName} {own.LastName}, {own.Email} \n" +
                $"{own.Address}\n" +
                $"{own.ZipCodes.Number} {own.ZipCodes.CityName}");
                */
            Console.WriteLine("Completed");
            Console.ReadLine();

        }
    }
}
