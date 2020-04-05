using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PeopleSearch
{
    public class PeopleSearch
    {
        private static string name;
        private static string address;
        private static int age;
        private static string[] interests;

        /// <summary>
        /// Gets the name of the Person
        /// </summary>
        /// <returns> name as a string </returns>
        private static string GetName()
        {
            Console.WriteLine("\nEnter full name: ");
            name = Console.ReadLine();
            name = ToTitleCase(name);
            Console.WriteLine(name);
            return name;
        }

        /// <summary>
        /// Gets the address of the person
        /// </summary>
        /// <returns> address as a string </returns>
        private static string GetAddress()
        {
            Console.WriteLine("\nEnter address: ");
            address = Console.ReadLine();
            address = ToTitleCase(address);
            return address;
        }

        /// <summary>
        /// Gets the age of the person
        /// </summary>
        /// <returns> name as an int </returns>
        private static int GetAge()
        {
            Console.WriteLine("\nEnter age: ");
            string ageAsString = Console.ReadLine();
            // ensure age entered is a valid number
            while(!(int.TryParse(ageAsString, out age)))
            {
                Console.WriteLine("Please enter a valid age:");
                ageAsString = Console.ReadLine();
            }
            return age;
        }

        /// <summary>
        /// Gets all interests of the person
        /// </summary>
        /// <returns> interests as string array </returns>
        private static string[] GetInterests()
        {
            Console.WriteLine("\nEnter {0}'s interests separated by a comma (must enter at least one interest)", name);
            string allInterests = Console.ReadLine();
            // prompt to get an interest until at least one is entered
            while (string.IsNullOrEmpty(allInterests))
            {
                Console.WriteLine("please enter at least one interest for {0}, if you enter more than one separate them by a comma:", name);
                allInterests = Console.ReadLine();               
            }
            // convert interest string to interest array
            interests = allInterests.Split(",");
            // trim whitespace off each element in interests
            for(int i = 0; i < interests.Length; i++)
            {
                interests[i] = interests[i].Trim();
            }
            return interests;
        }

        /// <summary>
        /// Gets name, address, age, and interests of a person
        /// </summary>
        private static void GetPersonInfo()
        {
            Console.WriteLine("To add a new person enter their information below");
            string name = GetName();
            string address = GetAddress();
            int age = GetAge();
            string[] interests = GetInterests();
        }

        /// <summary>
        /// Asks user for a string to search through the given dictionary keys. If the string is contained in any of the dictionary keys the values are 
        /// returned to the user.
        /// </summary>
        /// <param name="dictionaryOfPerson"> dictionary to search keys </param>
        private static void PersonSearch(Dictionary<string, Person> dictionaryOfPerson)
        {
            Console.WriteLine("Enter name to search for: ");
            string nameToSearch = Console.ReadLine();
            foreach (KeyValuePair<string, Person> k in dictionaryOfPerson)
            {
                if (k.Key.Contains(nameToSearch))
                {
                    Console.WriteLine(k.Value.ToString());
                }
                else
                {
                    Console.WriteLine(nameToSearch + " is not in the database.");
                }
            }

        }

        //helper methods
        /// <summary>
        /// Puts given string into to TitleCase. The first letter of each word is capitalized.
        /// </summary>
        /// <param name="str"> string to format with TitleCase </param>
        /// <returns> name in TitleCase </returns>
        private static string ToTitleCase(string str)
        {
            string nameWithTitleCase = str;
            if (!string.IsNullOrEmpty(str))
            {
                // separate words by a single space (" ") and put in string array.
                string[] words = str.Split(' ');
                // capitalize the first letter in each word
                for (int index = 0; index < words.Length; index++)
                {
                    string s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                // add all the words back together as a string
                nameWithTitleCase = string.Join(" ", words);
            }
            return nameWithTitleCase;
        }

        /// <summary>
        /// Prints three lines to the console asking to search for a person, add a person or exit the program
        /// </summary>
        private static string SearchOrAddPerson()
        {
            Console.WriteLine("Press 1 to search for a person");
            Console.WriteLine("Press 2 to add a new person");
            Console.WriteLine("Press 3 to Exit");
            string decision = Console.ReadLine();
            return decision;
        }

        public static void Main(string[] args)
        {
            // Ask user if they want to search for person, add a new person, or exit the program
            string userDecision = SearchOrAddPerson();
            Dictionary<string, Person> dictionaryOfPerson = new Dictionary<string, Person>();
            // continue asking SearchOrAddPerson until user exits the program
            while(!userDecision.Equals(""))
            {
                switch (userDecision)
                {
                    case "0":
                        userDecision = SearchOrAddPerson();
                        continue;
                    // search for a person
                    case "1":
                        if (dictionaryOfPerson.Keys.Count == 0)
                        {
                            Console.WriteLine("There are no people to search for, please add a person.");
                        }
                        PersonSearch(dictionaryOfPerson);
                        // change decision to "0" to call SearchOrAddPerson()
                        userDecision = "0";
                        continue;
                    // add a new person
                    case "2":
                        GetPersonInfo();
                        Person newPerson = new Person(name, address, age, interests);
                        dictionaryOfPerson.Add(newPerson.getName(), newPerson);
                        // change decision to "0" to call SearchOrAddPerson()
                        userDecision = "0";
                        continue;
                    // exit the program
                    case "3":
                        Environment.Exit(0);
                        break;
                    // invalid entry
                    default:
                        Console.WriteLine("Entry not valid");
                        userDecision = SearchOrAddPerson();
                        continue;
                }
            }
            
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
