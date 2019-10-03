using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SqlObject.Model;

namespace SqlObject
{
    class UI
    {
        public UI(SqlConnection c)
        {
            conn = c;
        }

        public void MainMenu()
        {
            string choice = userInput("What table would you like to access? ").Trim().ToLower();
            switch (choice)
            {
                case "owners": addOwner(); break;
                case "patients": addPatient(); break;
                default: Console.WriteLine("Not yet implemented"); break;
            }

            clickToContinue();
        }

        private void addOwner()
        {
            Console.WriteLine("CREATE NEW OWNER");
            string firstname = userInput("First name: \t");
            string lastname = userInput("Last name: \t");
            string phone = userInput("Phone number: \t");
            string email = userInput("Email address: \t");
            string address = userInput("Address: \t");
            string zip = userInput("Zip code: \t");

            Owner newOwner = new Owner(conn)
            {
                FirstName = firstname,
                LastName = lastname,
                Phone = phone,
                Email = email,
                Address = address,
                ZipCodes = new ZipCode(conn) { Number = int.Parse(zip) }
            };

            if (saysYes(userInput("Is the infomation above correct? (y/N)")))
            {
                newOwner.Insert();
            }
            else if (saysYes(userInput("Try again? (y/N)")))
            {
                addOwner();
            }
        }

        private void addPatient()
        {
            throw new NotImplementedException();

            Console.WriteLine("CREATE NEW PATIENT");
            string name = userInput("Patient name: \t");

            Patient newPatient = new Patient(conn)
            {
                Name = name,
            };

            if (saysYes(userInput("Is the infomation above correct? (y/N)")))
            {
                newPatient.Insert();
            }
            else if (saysYes(userInput("Try again? (y/N)")))
            {
                addPatient();
            }
        }

        private string userInput(string msg)
        {
            Console.Write(msg);
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.Write(msg);
            }
            return input;
        }

        private bool saysYes(string input)
        {
            List<string> acceptableAnswers = new List<string>
            {
                "y", "yes", "yep"
            };

            return (acceptableAnswers.Contains(input.ToLower()));
        }

        private void clickToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        SqlConnection conn;
    }
}
