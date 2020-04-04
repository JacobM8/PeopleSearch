using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch
{
    public class Person
    {
        // each person needs to contain a name, address, age, interests, and a picture
        public string PersonName;
        public string getName()
        {
            return PersonName;
        }
        public string PersonAddress;
        public string[] PersonInterests;
        public int PersonAge;

        public Person(string name, string address, int age, string[] interests)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(message: "name can not be empty", nameof(name));
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException(message: "address can not be empty", nameof(address));
            }

            PersonName = name;
            PersonAddress = address;
            PersonAge = age;
            PersonInterests = interests ?? throw new ArgumentNullException(nameof(interests));
        }
        public override string ToString()
        {
            string s = "Name: " + PersonName + ", Address: " + PersonAddress + ", Age: " + PersonAge + ", Interests: ";
            for (int i = 0; i < PersonInterests.Length - 1; i++)
            {
                s += PersonInterests[i] + ", ";
            }
            s += PersonInterests[PersonInterests.Length - 1];
            return s;
        }
    }
}
